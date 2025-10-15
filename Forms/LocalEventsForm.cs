using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MunicipalServicesApp.Data;
using MunicipalServicesApp.Model;

namespace MunicipalServicesApp.Forms
{
    // This form shows all local events.
    // Students can view, search, sort, and see recommended events.
    public partial class LocalEventsForm : Form
    {
        private EventManager eventManager; // manager for all events and searches

        public LocalEventsForm() : this(new EventManager()) { } //// Default constructor: creates a new EventManager if none provided

        public LocalEventsForm(EventManager manager) //constructor with event manager
        {
            // Save the event manager or throw error if null
            eventManager = manager ?? throw new ArgumentNullException(nameof(manager));
            InitializeComponent();
            //form setup
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Load categories and show all events when the form opens
            LoadCategories();
            RefreshEventCards(eventManager.GetAllEvents());
            RefreshRecommendationCards();
        }

        //----------------------------------------------------------------------------------------------------------------//
        private void LoadCategories() //Load event categories into the dropdown so users can filter by category
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All"); //option to show all events

            foreach (var c in eventManager.GetCategories()) //Add each category from event manager
                cmbCategory.Items.Add(c);

            cmbCategory.SelectedIndex = 0;
        }

        //----------------------------------------------------------------------------------------------------------------//
        private void DoSearch() //search/filter based on category, date range, or name
        {
            string cat = cmbCategory.SelectedItem?.ToString();
            if (cat == "All") cat = null;

            DateTime? from = dtFrom.Checked ? dtFrom.Value.Date : (DateTime?)null;
            DateTime? to = dtTo.Checked ? dtTo.Value.Date : (DateTime?)null;

            string searchText = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text.Trim();
            //get results from Event manager
            var results = eventManager.Search(cat, from, to, searchText);

            RefreshEventCards(results);
            RefreshRecommendationCards();
        }

        //----------------------------------------------------------------------------------------------------------------//
        private void RefreshEventCards(IEnumerable<Event> events) // Display events in the main events panel
        {
            eventsPanel.Controls.Clear(); //clear existing cards

            // Create a card for each event and add it to the panel
            foreach (var ev in events)
            {
                var card = CreateEventCard(ev);
                eventsPanel.Controls.Add(card); //add each event card
            }
        }
        //----------------------------------------------------------------------------------------------------------------//
        private Panel CreateEventCard(Event ev) // Create a single card to show an event
        {
            var card = new Panel
            {
                Size = new Size(eventsPanel.Width - 25, 120),
                BackColor = Color.White,
                Margin = new Padding(8),
                Cursor = Cursors.Hand
            };
            card.Region = new Region(RoundedRect(new Rectangle(0, 0, card.Width, card.Height), 12)); //round panel corners
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.LightGray))
                    e.Graphics.DrawPath(pen, RoundedRect(new Rectangle(0, 0, card.Width - 1, card.Height - 1), 12));
            };

            // Show event name
            var lblName = new Label
            {
                Text = ev.eventName,
                Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
                Location = new Point(12, 12),
                AutoSize = true
            };

            // Show Category label
            var lblCategory = new Label
            {
                Text = $"Category: {ev.eventCategory}",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(12, 40),
                AutoSize = true,
                ForeColor = Color.DimGray
            };

            // Show date label
            var lblDate = new Label
            {
                Text = $"Date: {ev.eventDate:yyyy-MM-dd HH:mm}",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(12, 66),
                AutoSize = true,
                ForeColor = Color.DimGray
            };

           
            //Add labels to card
            card.Controls.Add(lblName);
            card.Controls.Add(lblCategory);
            card.Controls.Add(lblDate);

            card.Tag = ev;
            card.Click += ShowEventDetailsFromRecommendation;

            return card;
        }

        //----------------------------------------------------------------------------------------------------------------//
        private void RefreshRecommendationCards() // Display recommended events in the side panel
        {
            lstRecommendations.Controls.Clear();

            int yOffset = 0;
            var recs = eventManager.Recommend(5); 
            foreach (var ev in recs)
            {
                var card = new Panel
                {
                    Size = new Size(lstRecommendations.Width - 12, 80),
                    BackColor = Color.White,
                    Margin = new Padding(4),
                    Cursor = Cursors.Hand
                };
                card.Region = new Region(RoundedRect(new Rectangle(0, 0, card.Width, card.Height), 10));
                card.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (var pen = new Pen(Color.LightGray))
                        e.Graphics.DrawPath(pen, RoundedRect(new Rectangle(0, 0, card.Width - 1, card.Height - 1), 10));
                };

                //Event Name
                var lblName = new Label
                {
                    Text = ev.eventName,
                    Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                    Location = new Point(8, 10),
                    AutoSize = true
                };

                //Event date
                var lblDate = new Label
                {
                    Text = ev.eventDate.ToString("yyyy-MM-dd"),
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.DimGray,
                    Location = new Point(8, 30),
                    AutoSize = true
                };

                card.Controls.Add(lblName);
                card.Controls.Add(lblDate);

                card.Tag = ev; //// Store the event object in the card's Tag property
                card.Click += ShowEventDetailsFromRecommendation;

                card.Location = new Point(0, yOffset);
                lstRecommendations.Controls.Add(card);
                yOffset += card.Height + 8;
            }
        }

        //----------------------------------------------------------------------------------------------------------------//
        private void ShowEventDetails(Event ev) // Open the EventDetailsForm for a specific event
        {
            eventManager.TrackEventClick(ev); // Track the click before showing
            var detailsForm = new EventDetailsForm(ev);
            detailsForm.ShowDialog(this);

            RefreshRecommendationCards();
        }

        //----------------------------------------------------------------------------------------------------------------//
        private void ApplySort()  // Apply sorting based on ComboBox selection
        {
            var idx = cmbSort.SelectedIndex;
            var currentEvents = eventManager.GetAllEvents();
            IEnumerable<Event> ordered = currentEvents;
            switch (idx)
            {
                case 0: ordered = currentEvents.OrderBy(e => e.eventDate); break; // Sort by date ascending
                case 1: ordered = currentEvents.OrderByDescending(e => e.eventDate); break; // Sort by date descending
                case 2: ordered = currentEvents.OrderBy(e => e.eventCategory); break; // Sort by category
                case 3: ordered = currentEvents.OrderBy(e => e.eventName); break; // Sort by name
            }
            RefreshEventCards(ordered);
        }

        //----------------------------------------------------------------------------------------------------------------//
        private void ShowEventDetailsFromRecommendation(object sender, EventArgs e) // Show details for event from recommendation panel
        {
            if (sender is Panel card && card.Tag is Event ev)
            {
                ShowEventDetails(ev);
                eventManager.MarkViewed(ev);
                RefreshRecommendationCards();
            }
        }
    }
}
