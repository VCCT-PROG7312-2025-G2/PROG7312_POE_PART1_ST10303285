using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MunicipalServicesApp.Forms
{
    partial class ServiceRequestStatusForm
    {
        private IContainer components = null;
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;

        private Panel contentPanel;
        private DataGridView dataGridViewRequests;
        private FlowLayoutPanel bottomButtonsPanel;

        private Button btnRefresh;
        private Label lblSearch;
        private TextBox txtSearchId;
        private Button btnSearch;
        private Button btnPrioritise;
        private Button btnShowMST;
        private Button btnRecent;
        private Button btnBack;

        private ToolTip mainToolTip;


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitialiseComponent()
        {
            components = new Container();
            // Form
            this.Text = "Service Request Status";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#F9F9F9");

            
            // ---------------- Header ----------------
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = ColorTranslator.FromHtml("#6CB2B2")
            };

            lblTitle = new Label
            {
                Text = "Service Request Status",
                Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(24, 18)
            };

            lblSubtitle = new Label
            {
                Text = "View and manage submitted service requests — search, prioritise and analyse routes.",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.WhiteSmoke,
                AutoSize = true,
                Location = new Point(26, 58)
            };

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSubtitle);

            //Content Panel
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24),
                BackColor = ColorTranslator.FromHtml("#F9F9F9")
            };

            // DataGridView 
            dataGridViewRequests = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                BackgroundColor = Color.White
            };

            // Bottom Buttons Panel
            bottomButtonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 96,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10, 14, 10, 14),
                BackColor = ColorTranslator.FromHtml("#F9F9F9"),
                WrapContents = false,
                AutoSize = false
            };

            // helper factory for styled buttons
            Button StyledButton(string text, string colorHex, bool whiteText = false)
            {
                var btn = new Button
                {
                    Text = text,
                    Size = new Size(140, 40),
                    BackColor = ColorTranslator.FromHtml(colorHex),
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = whiteText ? Color.White : Color.Black,
                    Font = new Font("Segoe UI", 9F),
                    Margin = new Padding(8, 0, 8, 0)
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Region = new Region(RoundedRect(new Rectangle(0, 0, btn.Width, btn.Height), 8));
                return btn;
            }

            // buttons + search controls 
            btnRefresh = StyledButton("Refresh", "#A8D8EA");
            lblSearch = new Label
            {
                Text = "Search by Request ID (copy-paste ID from grid):",
                Font = new Font("Segoe UI", 9F),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 10, 8, 0)
            };
            txtSearchId = new TextBox
            {
                Width = 360,
                Font = new Font("Segoe UI", 9F),
                Margin = new Padding(4, 10, 8, 0)
            };
            btnSearch = StyledButton("Search ID", "#A8D8EA");
            btnPrioritise = StyledButton("Show Priority", "#A8D8EA");
            btnShowMST = StyledButton("Analyse routes", "#6CB2B2", true);
            btnRecent = StyledButton("Recent Requests", "#E6E6E6");
            btnBack = StyledButton("Back", "#E6E6E6");

            // Add controls into bottom panel in order
            bottomButtonsPanel.Controls.Add(btnRefresh);
            bottomButtonsPanel.Controls.Add(lblSearch);
            bottomButtonsPanel.Controls.Add(txtSearchId);
            bottomButtonsPanel.Controls.Add(btnSearch);
            bottomButtonsPanel.Controls.Add(btnPrioritise);
            bottomButtonsPanel.Controls.Add(btnShowMST);
            bottomButtonsPanel.Controls.Add(btnRecent);
            bottomButtonsPanel.Controls.Add(btnBack);


            // tooltip for helpful hints
            mainToolTip = new ToolTip(components);
            mainToolTip.SetToolTip(btnShowMST, "Analyse request locations and suggest efficient route links.");
            mainToolTip.SetToolTip(btnRecent, "Show the most recent service requests.");

            contentPanel.Controls.Add(dataGridViewRequests);
            contentPanel.Controls.Add(bottomButtonsPanel);

            this.Controls.Add(contentPanel);
            this.Controls.Add(headerPanel);

            this.ClientSize = new Size(1366, 820);
        }

        // helper to draw rounded corners for buttons
        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }


    }
}