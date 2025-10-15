using System;
using System.Windows.Forms;
using MunicipalServicesApp.Model;

namespace MunicipalServicesApp.Forms
{

    // This form shows all the details for a single event.
    public partial class EventDetailsForm : Form
    { 
        private Event ev; // the event to display

        public EventDetailsForm(Event ev) // Constructor takes an Event object
        {

            // Save event or throw error if null
            this.ev = ev ?? throw new ArgumentNullException(nameof(ev));
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            // Load event data into labels and text box
            LoadEventDetails();
        }
        //-----------------------------------------------------------------------------------------------------------------//
        private void LoadEventDetails() // Fill the form controls with event information
        {
            lblTitle.Text = ev.eventName; // Event name
            lblCategory.Text = $"Category: {ev.eventCategory}"; // Event category
            lblDate.Text = $"Date: {ev.eventDate:yyyy-MM-dd HH:mm}"; // Event date
            lblLocation.Text = $"Location: {ev.eventLocation}"; // Event location
            txtDescription.Text = ev.eventDescription; // Event description


            btnEmail.Tag = ev.contactEmail ?? "mailto:info@example.com"; // fallback email
            btnPhone.Tag = ev.contactPhone ?? "tel:+1234567890"; // fallback phone number
        }

        //-----------------------------------------------------------------------------------------------------------------//
        private void BtnClose_Click(object sender, EventArgs e)  // Close the form when the user clicks Close button
        {
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------------------------//
        private void BtnEmail_Click(object sender, EventArgs e) // Open the user's email client with the event email
        {
            if (sender is Button btn && btn.Tag is string mailto)
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(mailto) 
                { 
                    UseShellExecute = true 
                });
            }
        }

        //-----------------------------------------------------------------------------------------------------------------//

        private void BtnPhone_Click(object sender, EventArgs e) // Open the user's phone dialer with the event phone number
        {
            if (sender is Button btn && btn.Tag is string tel)
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(tel) {
                    UseShellExecute = true 
                });
            }
        }

        //-----------------------------------------------------------------------------------------------------------------//
    }
}
