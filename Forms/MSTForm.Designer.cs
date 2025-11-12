using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MunicipalServicesApp.Forms
{
    partial class MSTForm
    {
        private FlowLayoutPanel flowPanel;
        private Label lblHeader;
        private Label lblTotal;

        private void InitializeComponent()
        {
            this.Text = "Minimum Spanning Tree (Service Request Network)";
            this.BackColor = ColorTranslator.FromHtml("#F9F9F9");
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterParent;

            lblHeader = new Label
            {
                Text = "📊 Service Request Similarity Network",
                Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#333"),
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(12, 10, 0, 0)
            };

            lblTotal = new Label
            {
                Text = "Total Network Cost:",
                Font = new Font("Segoe UI Semibold", 11F),
                ForeColor = ColorTranslator.FromHtml("#333"),
                Dock = DockStyle.Bottom,
                Height = 40,
                Padding = new Padding(24,8,24,8)
            };

            flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(24),
                BackColor = ColorTranslator.FromHtml("#F9F9F9")
            };

            this.Controls.Add(flowPanel);
            this.Controls.Add(lblHeader);
            this.Controls.Add(lblTotal);
        }

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
