using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using MunicipalServicesApp.Model;
using System.Windows.Forms;

namespace MunicipalServicesApp.Data
{
    public class ServiceRequestRepo
    {
        private readonly string filePath;
        private List<ServiceRequest> items = new List<ServiceRequest>(); // In-memory list of requests.

        public ServiceRequestRepo(string fileName = "service_requests.xml") // Constructor: set file location and load existing data.
        {
            // store file in application base folder for easy access
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            Load();
        }

        public IReadOnlyList<ServiceRequest> All => items.AsReadOnly(); // Give a read-only view of all requests.

        // Add a new request. Save right away so changes persist.
        public void Add(ServiceRequest r)
        {
            items.Add(r);
            Save();
        }

        // Find a request by its id. Return null if not found.
        public ServiceRequest GetById(string id)
        {
            return items.Find(x => x.Id == id);
        }

        // Replace an existing request with an updated one.
        public void Update(ServiceRequest updated)
        {
            var idx = items.FindIndex(x => x.Id == updated.Id);
            if (idx >= 0)
            {
                items[idx] = updated;
                Save();
            }
        }

        // Save the list to an XML file.
        public void Save()
        {
            try
            {
                var ser = new XmlSerializer(typeof(List<ServiceRequest>));
                // create or overwrite file and write list
                using var stream = File.Create(filePath);
                ser.Serialize(stream, items);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving requests: " + ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load requests from the XML file into memory.
        public void Load()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    items = new List<ServiceRequest>();
                    return;
                }
                var ser = new XmlSerializer(typeof(List<ServiceRequest>));
                using var stream = File.OpenRead(filePath);
                items = (List<ServiceRequest>)ser.Deserialize(stream) ?? new List<ServiceRequest>();
            }
            catch
            {
                // if corrupt or fail, start fresh to avoid crashes
                items = new List<ServiceRequest>();
            }
        }
    }
}
