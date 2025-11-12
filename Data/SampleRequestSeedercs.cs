using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MunicipalServicesApp.Model;

namespace MunicipalServicesApp.Data
{
    public class SampleRequestSeedercs
    {

        public static void SeedIfEmpty(ServiceRequestRepo repo, int count = 18)
        {
            if (repo.All.Count > 0)
                return;

            var requests = new List<ServiceRequest>
            {
                new ServiceRequest { Location = "Claremont", Category = "Water Leak", Description = "Water leaking from pipe near Palmyra Junction", Priority = 4, Status = "Submitted" },
                new ServiceRequest { Location = "Mitchells Plain", Category = "Pothole", Description = "Large pothole causing traffic on AZ Berman Drive", Priority = 5, Status = "In Progress" },
                new ServiceRequest { Location = "Woodstock", Category = "Streetlight", Description = "Streetlight not working near Roodebloem Road", Priority = 2, Status = "Submitted" },
                new ServiceRequest { Location = "Gugulethu", Category = "Sanitation", Description = "Blocked drain near NY78 causing foul smell", Priority = 5, Status = "In Progress" },
                new ServiceRequest { Location = "Bellville", Category = "Illegal Dumping", Description = "Rubbish dumped near Tygervalley Road", Priority = 3, Status = "Submitted" },
                new ServiceRequest { Location = "Khayelitsha", Category = "Water Leak", Description = "Burst pipe flooding sidewalk in Site B", Priority = 5, Status = "In Progress" },
                new ServiceRequest { Location = "Rondebosch", Category = "Pothole", Description = "Pothole near Rondebosch Common entrance", Priority = 2, Status = "Submitted" },
                new ServiceRequest { Location = "Observatory", Category = "Streetlight", Description = "Streetlights flickering on Station Road", Priority = 3, Status = "In Progress" },
                new ServiceRequest { Location = "Athlone", Category = "Sanitation", Description = "Overflowing public bin near stadium entrance", Priority = 1, Status = "Submitted" },
                new ServiceRequest { Location = "Goodwood", Category = "Water Leak", Description = "Persistent leak near N1 City intersection", Priority = 4, Status = "Submitted" },
                new ServiceRequest { Location = "Grassy Park", Category = "Pothole", Description = "Deep pothole outside local clinic", Priority = 5, Status = "In Progress" },
                new ServiceRequest { Location = "Mowbray", Category = "Streetlight", Description = "Streetlight out near Liesbeek Parkway", Priority = 2, Status = "Submitted" },
                new ServiceRequest { Location = "Philippi", Category = "Sanitation", Description = "Blocked drain outside informal settlement", Priority = 5, Status = "In Progress" },
                new ServiceRequest { Location = "Sea Point", Category = "Water Leak", Description = "Leaking fire hydrant on Beach Road", Priority = 3, Status = "Submitted" },
                new ServiceRequest { Location = "Elsies River", Category = "Illegal Dumping", Description = "Illegal dumping behind local primary school", Priority = 4, Status = "In Progress" },
                new ServiceRequest { Location = "Wynberg", Category = "Pothole", Description = "Road damage after heavy rain near Maynard Mall", Priority = 3, Status = "Submitted" },
                new ServiceRequest { Location = "Delft", Category = "Streetlight", Description = "Broken streetlight near Delft South", Priority = 4, Status = "In Progress" },
                new ServiceRequest { Location = "Cape Town CBD", Category = "Sanitation", Description = "Overflowing drain near Greenmarket Square", Priority = 3, Status = "Submitted" },

            };


            var random = new Random();
            foreach (var req in requests)
            {
                req.Id = Guid.NewGuid().ToString();
                req.DateReported = DateTime.Now.AddDays(-random.Next(1, 30));
                repo.Add(req);
            }
        }

    }
}
