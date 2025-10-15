using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    partial class ReportIssuesForm
    {
        private IContainer components = null;

        private Panel headerPanel;
        private Label lblHeader;
        private Label lblSubtitle;

        private Label lblLocation;
        private TextBox txtLocation;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblDescription;
        private RichTextBox rtbDescription;
        private Button btnAttach;
        private Label lblAttachment;
        private ProgressBar pbEngagement;
        private Label lblEncouragement;
        private Button btnSubmit;
        private Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();

            // ---------------------------------------------------------------------------------------- Form Setup -----------------------------------------------------------
            this.Text = "Report an Issue";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#F9F9F9");

            // ---------------------------------------------------------------------------------------- Header Panel ----------------------------------------------------------
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = ColorTranslator.FromHtml("#6CB2B2")
            };

            lblHeader = new Label
            {
                Text = "Report an Issue",
                Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(24, 18)
            };

            lblSubtitle = new Label
            {
                Text = "Help us improve your community by reporting problems in your area.",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.WhiteSmoke,
                AutoSize = true,
                Location = new Point(26, 58)
            };

            headerPanel.Controls.Add(lblHeader);
            headerPanel.Controls.Add(lblSubtitle);
            this.Controls.Add(headerPanel);

            int marginLeft = 50;
            int inputWidth = 700;
            int inputHeight = 30;
            int verticalSpacing = 50;
            int currentY = 130;

            // ------------------------------------------------------------------------------------------- Location ----------------------------------------------------------------
            lblLocation = new Label
            {
                Text = "Location (suburb / street):",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(marginLeft, currentY),
                AutoSize = true
            };
            this.Controls.Add(lblLocation);

            txtLocation = new TextBox
            {
                Location = new Point(marginLeft + 220, currentY - 3),
                Size = new Size(inputWidth, inputHeight),
                Font = new Font("Segoe UI", 10F)
            };
            txtLocation.TextChanged += AnyInputChanged;
            this.Controls.Add(txtLocation);

            currentY += verticalSpacing;

            // ------------------------------------------------------------------------------------------- Category --------------------------------------------------------------------
            lblCategory = new Label
            {
                Text = "Category:",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(marginLeft, currentY),
                AutoSize = true
            };
            this.Controls.Add(lblCategory);

            cmbCategory = new ComboBox
            {
                Location = new Point(marginLeft + 220, currentY - 3),
                Size = new Size(300, inputHeight),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cmbCategory.Items.AddRange(new object[] { "Sanitation", "Roads", "Utilities", "Street Lighting", "Public Safety", "Other" });
            cmbCategory.SelectedIndexChanged += AnyInputChanged;
            this.Controls.Add(cmbCategory);

            currentY += verticalSpacing;

            // ------------------------------------------------------------------------------------------- Description ----------------------------------------------------------------------
            lblDescription = new Label
            {
                Text = "Description:",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(marginLeft, currentY),
                AutoSize = true
            };
            this.Controls.Add(lblDescription);

            rtbDescription = new RichTextBox
            {
                Location = new Point(marginLeft + 220, currentY - 3),
                Size = new Size(inputWidth, 200),
                Font = new Font("Segoe UI", 10F)
            };
            rtbDescription.TextChanged += AnyInputChanged;
            this.Controls.Add(rtbDescription);

            currentY += 220 + 10;

            // ------------------------------------------------------------------------------------------ Attach File -------------------------------------------------------------------------
            btnAttach = new Button
            {
                Text = "Attach Image / Document",
                Location = new Point(marginLeft + 220, currentY),
                Size = new Size(220, 35),
                BackColor = ColorTranslator.FromHtml("#6CB2B2"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F)
            };
            btnAttach.FlatAppearance.BorderSize = 0;
            btnAttach.Region = new Region(RoundedRect(new Rectangle(0, 0, btnAttach.Width, btnAttach.Height), 8));
            btnAttach.Click += BtnAttach_Click;
            this.Controls.Add(btnAttach);

            lblAttachment = new Label
            {
                Text = "No file attached",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(marginLeft + 460, currentY + 5),
                AutoSize = true
            };
            this.Controls.Add(lblAttachment);

            currentY += 50;

            // ------------------------------------------------------------------------------------------ Engagement Bar --------------------------------------------------------------------------
            pbEngagement = new ProgressBar
            {
                Location = new Point(marginLeft + 220, currentY),
                Size = new Size(inputWidth, 20)
            };
            this.Controls.Add(pbEngagement);

            currentY += 30;

            lblEncouragement = new Label
            {
                Text = "Complete fields to improve report quality.",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(marginLeft + 220, currentY),
                Size = new Size(inputWidth, 30)
            };
            this.Controls.Add(lblEncouragement);

            currentY += 50;

            // ----------------------------------------------------------------------------------------- Buttons --------------------------------------------------------------------------------------
            btnSubmit = new Button
            {
                Text = "Submit Report",
                Size = new Size(180, 40),
                Location = new Point(marginLeft + 220, currentY),
                BackColor = ColorTranslator.FromHtml("#6CB2B2"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F)
            };
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Region = new Region(RoundedRect(new Rectangle(0, 0, btnSubmit.Width, btnSubmit.Height), 8));
            btnSubmit.Click += BtnSubmit_Click;
            this.Controls.Add(btnSubmit);

            btnBack = new Button
            {
                Text = "Back to Menu",
                Size = new Size(180, 40),
                Location = new Point(marginLeft + 420, currentY),
                BackColor = ColorTranslator.FromHtml("#A8D8EA"),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F)
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Region = new Region(RoundedRect(new Rectangle(0, 0, btnBack.Width, btnBack.Height), 8));
            btnBack.Click += BtnBack_Click;
            this.Controls.Add(btnBack);
        }

        // Utility for rounded buttons
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
