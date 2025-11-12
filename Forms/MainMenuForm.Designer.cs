using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MunicipalServicesApp
{
    partial class MainMenuForm // Form
    {
        private Panel headerPanel; //Header panel
        private Label lblTitle; // Title label
        private Label lblSubtitle; // Subtitle label
        private FlowLayoutPanel cardsPanel; // Panel to hold cards

        private void InitialiseComponent() // Method to initialise components
        {
            this.headerPanel = new Panel();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.cardsPanel = new FlowLayoutPanel();

            // Main Form
            this.Text = "Municipal Services Portal";
            this.Size = new Size(880, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#F9F9F9"); // pastel background

            // Header Panel
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 110;
            this.headerPanel.BackColor = ColorTranslator.FromHtml("#6CB2B2"); // pastel teal

            // Title
            this.lblTitle.Text = "City Services Hub — Municipal Services";
            this.lblTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(24, 18);

            // Subtitle
            this.lblSubtitle.Text = "Report issues, view updates and stay in touch with your municipality.";
            this.lblSubtitle.Font = new Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = Color.WhiteSmoke;
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new Point(26, 58);

            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Controls.Add(this.lblSubtitle);

            // Cards Panel
            this.cardsPanel.Dock = DockStyle.Fill;
            this.cardsPanel.Padding = new Padding(24);
            this.cardsPanel.AutoScroll = true;
            this.cardsPanel.FlowDirection = FlowDirection.TopDown;
            this.cardsPanel.WrapContents = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Add cards
            this.cardsPanel.Controls.Add(CreateCard("Report Issues", "Report potholes, broken lights, sanitation problems and more.", true, BtnReport_Click, Properties.Resources.reportIcon));
            this.cardsPanel.Controls.Add(CreateCard("Local Events & Announcements", "View upcoming local events and official announcements.", true, BtnEvents_Click, Properties.Resources.eventsIcon));
            this.cardsPanel.Controls.Add(CreateCard("Service Request Status", "Check the status of previously submitted requests/issues.", true, BtnStatus_Click, Properties.Resources.statusIcon));

            this.Controls.Add(this.cardsPanel);
            this.Controls.Add(this.headerPanel);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private Panel CreateCard(string title, string desc, bool enabled, EventHandler onClick, Image icon) // Method to create a card
        {
            var panel = new Panel()
            {
                Size = new Size(1800, 200),
                BackColor = Color.White,
                Margin = new Padding(8),
               

            };

            // Rounded corners for panel
            panel.Region = new Region(RoundedRect(new Rectangle(0, 0, panel.Width, panel.Height), 12));
            panel.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.LightGray))
                {
                    g.DrawPath(pen, RoundedRect(new Rectangle(0, 0, panel.Width - 1, panel.Height - 1), 12));
                }
            };

            // Icon
            var iconBox = new PictureBox()
            {
                Image = icon,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(16, 25),
                Size = new Size(64, 64)
            };

            var titleLbl = new Label()
            {
                Text = title,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(96, 10),
                AutoSize = true
            };

            var descLbl = new Label()
            {
                Text = desc,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.DimGray,
                Location = new Point(96, 40),
                AutoSize = false,
                Size = new Size(480, 60)
            };

            var btn = new Button()
            {
                Text = enabled ? "Open" : "Coming Soon",
                Location = new Point(1600, 35),
                Size = new Size(150, 45),
                Enabled = enabled,
                BackColor = enabled ? ColorTranslator.FromHtml("#A8D8EA") : ColorTranslator.FromHtml("#D6D6D6"),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
            };

            btn.FlatAppearance.BorderSize = 0; 
            btn.Region = new Region(RoundedRect(new Rectangle(0, 0, btn.Width, btn.Height), 8));

            if (enabled) btn.Click += onClick;// attach event
            else btn.Click += (s, e) => MessageBox.Show("This feature is coming in a later release.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            panel.Controls.Add(iconBox);
            panel.Controls.Add(titleLbl);
            panel.Controls.Add(descLbl);
            panel.Controls.Add(btn);

            return panel;
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius) // Method to create rounded rectangle path
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
