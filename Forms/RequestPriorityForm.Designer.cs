using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp.Forms
{
    partial class RequestPriorityForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Header label and cards panel created by designer partial.
        /// </summary>
        private FlowLayoutPanel cardsPanel;
        private Label hdr;

        /// <summary>
        /// Clean up resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Designer initialization for RequestPriorityForm.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();

            // Form
            this.Text = "Priority Order";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = ColorTranslator.FromHtml("#F9F9F9");

            // Header label
            hdr = new Label
            {
                Text = "Priority Order — Requests sorted by priority then date",
                Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#333"),
                AutoSize = false,
                Height = 40,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(16, 8, 0, 0)
            };

            // Cards panel
            cardsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(16),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent
            };

            // Add controls
            this.Controls.Add(cardsPanel);
            this.Controls.Add(hdr);
        }
    }
}
