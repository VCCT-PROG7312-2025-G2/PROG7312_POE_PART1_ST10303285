using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MunicipalServicesApp.Model;
using MunicipalServicesApp.Data;

namespace MunicipalServicesApp.Forms
{
    public partial class ServiceRequestStatusForm : Form
    {

        // repo holds saved requests on disk
        private readonly ServiceRequestRepo repo;
        // data structures used in the form
        private AvlTree avl;
        private RequestPriorityHeap heap;
        private ServiceRouteNetwork graph;
       
        private Button btnCloseEmbedded;
        
        public ServiceRequestStatusForm()
        {
            InitialiseComponent();
            // create repository (it will load file)
            repo = new ServiceRequestRepo();
            SampleRequestSeedercs.SeedIfEmpty(repo);



            //Initialise AVL tree and heap, and load data into it
            avl = new AvlTree();
            heap = new RequestPriorityHeap();
            foreach (var r in repo.All)
            {
                avl.Insert(r);
                heap.Insert(r);
            }

            graph = new ServiceRouteNetwork(); // Build a simple graph from requests (used for route analysis)
            graph.BuildFromRequests(repo.All, threshold: 10.0);
            var rand = new Random();
            var locations = repo.All.Select(r => r.Location).Distinct().Take(6).ToList();
            for (int i = 0; i < locations.Count - 1; i++)
            {
                for (int j = i + 1; j < locations.Count; j++)
                {
                    int weight = rand.Next(1, 15); // random distance
                    graph.AddEdge(locations[i], locations[j], weight);
                }
            }

            CreateContentPanel();  // create a hidden panel we can show forms inside

            btnRefresh.Click += (s, e) => RefreshGrid();
            btnSearch.Click += (s, e) => SearchById();
            btnPrioritise.Click += (s, e) => ShowPriorityOrder();
            btnShowMST.Click += (s, e) => ShowGraphMST();
            btnBack.Click += (s, e) => this.Close();
            btnRecent.Click += (s, e) => ShowRecentRequests();

        // initial load
        RefreshGrid();
        }


        private void CreateContentPanel()  // create the panel where we will embed other small forms
        {
            contentPanel = new Panel
            {
                BackColor = ColorTranslator.FromHtml("#F9F9F9"),
                BorderStyle = BorderStyle.None,
                Visible = false
            };

            if (this.Controls.Contains(dataGridViewRequests))
            {
                contentPanel.Location = dataGridViewRequests.Location;
                contentPanel.Size = dataGridViewRequests.Size;
                contentPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
            else
            {
                contentPanel.Dock = DockStyle.Fill;
            }

            btnCloseEmbedded = new Button
            {
                Text = "Close",
                Size = new Size(80, 30),
                BackColor = ColorTranslator.FromHtml("#E6E6E6"),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(Math.Max(8, contentPanel.Width - 88), 8),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnCloseEmbedded.FlatAppearance.BorderSize = 0;
            btnCloseEmbedded.Click += (s, e) => CloseEmbedded();

            contentPanel.Resize += (s, e) =>
            {
                btnCloseEmbedded.Location = new Point(Math.Max(8, contentPanel.Width - btnCloseEmbedded.Width - 8), 8);
            };

            contentPanel.Controls.Add(btnCloseEmbedded);
            this.Controls.Add(contentPanel);
            contentPanel.BringToFront();
        }


        private bool IsEmbeddedVisible() // check if embedded panel currently shows something other than the close button
        {
            return contentPanel != null && contentPanel.Visible && contentPanel.Controls.Count > 1; // close button is always present
        }

        private void CloseEmbedded()
        {
            if (contentPanel == null) return;

            for (int i = contentPanel.Controls.Count - 1; i >= 0; i--)
            {
                var c = contentPanel.Controls[i];
                if (c == btnCloseEmbedded) continue;
                if (c is Form f)
                {
                    try { f.Close(); } catch { }
                    try { f.Dispose(); } catch { }
                }
                contentPanel.Controls.Remove(c);
            }

            contentPanel.Visible = false;

            if (dataGridViewRequests != null) dataGridViewRequests.Visible = true;
        }

        private void EmbedForm(Form toEmbed)
        {
            if (contentPanel == null) CreateContentPanel();
            CloseEmbedded();

            toEmbed.TopLevel = false;
            toEmbed.FormBorderStyle = FormBorderStyle.None;
            toEmbed.Dock = DockStyle.Fill;

            contentPanel.Controls.Add(toEmbed);
            contentPanel.Controls.SetChildIndex(btnCloseEmbedded, 0);

            contentPanel.Visible = true;

            if (dataGridViewRequests != null) dataGridViewRequests.Visible = false;

            toEmbed.Show();
        }


        // Load repository items into the grid
        private void RefreshGrid()
        {
            try
            {
                var list = repo.All
                    .OrderByDescending(x => x.DateReported)
                    .Select(x => new
                    {
                        Id = x.Id,
                        Category = x.Category,
                        Location = x.Location,
                        Priority = x.Priority,
                        Status = x.Status,
                        DateReported = x.DateReported
                    }).ToList();

                dataGridViewRequests.DataSource = null;
                dataGridViewRequests.DataSource = list;
                dataGridViewRequests.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load requests: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Search by GUID / ID string (copy ID from grid)
        private void SearchById()
        {
            var id = txtSearchId.Text?.Trim();
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Enter a Request ID to search (copy from the grid).", "Input required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Try AVL search first
            var req = avl?.SearchById(id);

            // Fall back to repo search if AVL not available or not found
            if (req == null)
                req = repo.GetById(id);

            if (req == null)
            {
                MessageBox.Show("No request found with that ID.", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // show a simple details dialog
            var details = $"ID: {req.Id}\nCategory: {req.Category}\nLocation: {req.Location}\nPriority: {req.Priority}\nStatus: {req.Status}\nDate: {req.DateReported}\n\nDescription:\n{req.Description}";
            MessageBox.Show(details, "Request Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowPriorityOrder() // show requests ordered by priority inside an embedded form
        {

            if (repo.All == null || repo.All.Count ==0)
            {
                MessageBox.Show("No requests available.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var ordered = repo.All
                .OrderBy(r => r.Priority)
                .ThenByDescending(r => r.DateReported)
                .ToList();

            var priorityForm = new RequestPriorityForm(ordered);
            EmbedForm(priorityForm);


        }

        private void ShowGraphMST() // get MST from the graph and show it in a form
        {
            if (graph == null)
            {
                MessageBox.Show("Graph not initialised.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var (edgesTuples, totalCost) = graph.GetMinimumSpanningTree();

            if (edgesTuples == null || edgesTuples.Count == 0)
            {
                MessageBox.Show("No MST could be generated (not enough requests).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // convert to MSTForm's edge type
            var edges = edgesTuples.Select(t =>
            {
                var fromReq = repo.GetById(t.from);
                var toReq = repo.GetById(t.to);

                string fromLabel = fromReq != null
                    ? $"{fromReq.Category} — {fromReq.Location}"
                    : t.from;

                string toLabel = toReq != null
                    ? $"{toReq.Category} — {toReq.Location}"
                    : t.to;

                return new MSTForm.Edge
                {
                    From = fromLabel,
                    To = toLabel,
                    Distance = t.weight,
                    Category = t.category
                };
            }).ToList();

            var mstForm = new MSTForm(edges, totalCost);
            EmbedForm(mstForm);
        }

        // show most recent requests using AVL helper
        private void ShowRecentRequests()
        {
            if (avl == null)
            {
                MessageBox.Show("No AVL tree available.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var recent10 = avl.GetMostRecent(10);
            if (recent10 == null || !recent10.Any())
            {
                MessageBox.Show("No requests found in AVL tree.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var recentForm = new RecentRequestsForm(recent10, 10);
            EmbedForm(recentForm);
        }





    }
}
