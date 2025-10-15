using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MunicipalServicesApp.Forms
{
    partial class EventDetailsForm
    {
        private IContainer components = null;

        private Panel headerPanel;
        private Label lblTitle;
        private Label lblCategory;
        private Label lblDate;
        private Label lblLocation;
        private TextBox txtDescription;
        private Button btnClose;
        private Button btnEmail;
        private Button btnPhone;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Text = "Event Details";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.WindowState = FormWindowState.Maximized;

            // ---------------------------------------------------------------------------------------- Header Panel ------------------------------------------------------------------
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = ColorTranslator.FromHtml("#6CB2B2")
            };

            lblTitle = new Label
            {
                Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 0, 0, 0)
            };

            headerPanel.Controls.Add(lblTitle);

            // -------------------------------------------------------------------------------------- Event Info Labels --------------------------------------------------------------------
            lblCategory = new Label
            {
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.DimGray,
                AutoSize = true,
                Location = new Point(600, 100)
            };
            lblDate = new Label
            {
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.DimGray,
                AutoSize = true,
                Location = new Point(600, 130)
            };
            lblLocation = new Label
            {
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.DimGray,
                AutoSize = true,
                Location = new Point(600, 160)
            };

            // ------------------------------------------------------------------------------------------------- Description --------------------------------------------------------------
            txtDescription = new TextBox
            {
                Font = new Font("Segoe UI", 12F),
                Location = new Point(600, 200),
                Size = new Size(800, 300),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // ------------------------------------------------------------------------------------------------- Contact Buttons ------------------------------------------------------------
            btnEmail = new Button
            {
                Text = "Email Organizer",
                Size = new Size(160, 40),
                Location = new Point(600, 520),
                BackColor = ColorTranslator.FromHtml("#A8D8EA"),
                FlatStyle = FlatStyle.Flat
            };
            btnEmail.FlatAppearance.BorderSize = 0;
            btnEmail.Click += BtnEmail_Click;

            btnPhone = new Button
            {
                Text = "Call Organizer",
                Size = new Size(160, 40),
                Location = new Point(800, 520),
                BackColor = ColorTranslator.FromHtml("#A8D8EA"),
                FlatStyle = FlatStyle.Flat
            };
            btnPhone.FlatAppearance.BorderSize = 0;
            btnPhone.Click += BtnPhone_Click;

            // ---------------------------------------------------------------------------------------------------- Close Button ----------------------------------------------------------------------
            btnClose = new Button
            {
                Text = "Close",
                Size = new Size(120, 40),
                Location = new Point(1000, 520),
                BackColor = ColorTranslator.FromHtml("#F28C8C"),
                FlatStyle = FlatStyle.Flat
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += BtnClose_Click;

            // --------------------------------------------------------------------------------------------------- Add controls to form ---------------------------------------------------------------------------
            this.Controls.Add(headerPanel);
            this.Controls.Add(lblCategory);
            this.Controls.Add(lblDate);
            this.Controls.Add(lblLocation);
            this.Controls.Add(txtDescription);
            this.Controls.Add(btnEmail);
            this.Controls.Add(btnPhone);
            this.Controls.Add(btnClose);
        }
    }
}
