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

namespace MunicipalServicesApp
{
    public partial class ReportIssuesForm : Form // Form
    {
        private string selectedAttachmentPath = null; // Path of the selected attachment

        public ReportIssuesForm() // Constructor
        {
            InitialiseComponent();
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            UpdateEngagement();
        }

        private void AnyInputChanged(object sender, EventArgs e) => UpdateEngagement(); // Event handler for input changes

        private void UpdateEngagement() // Method to update engagement level
        {
            int score = 0;
            if (!string.IsNullOrWhiteSpace(txtLocation.Text)) score++;
            if (cmbCategory.SelectedIndex >= 0) score++;
            if (!string.IsNullOrWhiteSpace(rtbDescription.Text) && rtbDescription.Text.Trim().Length > 25) score++;
            if (!string.IsNullOrWhiteSpace(selectedAttachmentPath)) score++;

            int percent = score * 25;
            pbEngagement.Value = percent;
            lblEncouragement.Text = percent switch
            {
                0 => "Start by adding a location.",
                25 => "Good — Choose a category to describe your issue.",
                50 => "Nice! add a detailed description to improve actionability.",
                75 => "Almost complete — Consider attaching an image for faster resolution.",
                100 => "Excellent — your report is detailed and ready to submit. Thank you!",
                _ => "Complete fields to improve report quality."
            };
        }

        private void BtnAttach_Click(object sender, EventArgs e) // Event handler for attach button
        {
            using var dlg = new OpenFileDialog();
            dlg.Filter = "Images & Documents|*.jpg;*.jpeg;*.png;*.bmp;*.pdf;*.doc;*.docx;*.txt|All files|*.*";
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                selectedAttachmentPath = dlg.FileName;
                lblAttachment.Text = Path.GetFileName(selectedAttachmentPath);
                UpdateEngagement();
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e) // Event handler for submit button
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Please enter the location.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a category.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(rtbDescription.Text) || rtbDescription.Text.Trim().Length < 10)
            {
                MessageBox.Show("Please provide a description with at least 10 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var issue = new Issue
            {
                Location = txtLocation.Text.Trim(),
                Category = cmbCategory.SelectedItem?.ToString() ?? "Other",
                Description = rtbDescription.Text.Trim(),
                AttachmentPath = selectedAttachmentPath,
                EngagementLevel = pbEngagement?.Value.ToString() + "0"
            };

            // Add to custom linked list
            Program.Issues.AddLast(issue);

            try
            {
                var request = new Model.ServiceRequest
                {
                    Location = issue.Location,
                    Category = issue.Category,
                    Description = issue.Description,
                    AttachmentPath = issue.AttachmentPath,
                    EngagementLevel = issue.EngagementLevel,
                    DateReported = DateTime.Now,
                    Priority = 3,
                    Status = "Submitted"
                };

                var repo = new Data.ServiceRequestRepo();
                repo.Add(request);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Warning: request saved locally but failed to persist to file: " + ex.Message,
                "Persistence Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

                MessageBox.Show("Issue submitted. Reference ID: " + issue.Id,
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset form
            txtLocation.Text = "";
            cmbCategory.SelectedIndex = -1;
            rtbDescription.Text = "";
            selectedAttachmentPath = null;
            lblAttachment.Text = "No file attached";
            UpdateEngagement();
        }

        private void BtnBack_Click(object sender, EventArgs e) // Event handler for back button
        {
            Close();
        }

    }
}
