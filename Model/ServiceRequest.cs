using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp.Model
{
    public class ServiceRequest
    {
        public string Id { get; set; }
        public DateTime DateReported { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
        public string EngagementLevel { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; } // e.g., Submitted, In Progress, Resolved

        public ServiceRequest()
        {
            Id = Guid.NewGuid().ToString();
            DateReported = DateTime.Now;
            Priority = 3;
            Status = "Submitted";
        }
    }
}
