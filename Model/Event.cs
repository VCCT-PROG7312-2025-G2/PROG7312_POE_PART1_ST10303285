using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MunicipalServicesApp.Model
{
    public class Event
    {
        public int eventId { get; set; }
        public string eventName { get; set; }
        public string eventDescription { get; set; }
        public string eventCategory { get; set; }
        public DateTime eventDate { get; set; }
        public string eventLocation { get; set; }

        public string contactEmail { get; set; }
        public string contactPhone { get; set; }


        public Event() { }

        public Event(int id, string name, string description, string category, DateTime date, string location)
        {
            eventId = id;
            eventName = name;
            eventDescription = description;
            eventCategory = category;
            eventDate = date;
            eventLocation = location;
        }

        public override string ToString()
        {
            return $"{eventName} — {eventCategory} — {eventDate:yyyy-MM-dd} @ {eventLocation}";
        }


    }


}