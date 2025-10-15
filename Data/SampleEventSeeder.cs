using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MunicipalServicesApp.Data;
using MunicipalServicesApp.Model;

namespace MunicipalServicesApp.Data
{
    public static class SampleEventSeeder
    {
        public static void Seed(EventManager manager)
        {
            if (manager == null)
                throw new ArgumentNullException(nameof(manager));

            var now = DateTime.Now.Date;

            manager.AddEvent("Community Clean-up", "Join neighbours for a street clean-up.","Community", now.AddDays(2).AddHours(9), "Greenpoint Park");
            manager.AddEvent("Movie Night", "Outdoor screening of a family-friendly film.", "Entertainment", now.AddDays(21).AddHours(19), "Numetro Canal Walk");
            manager.AddEvent("Fire Drill & Safety Training", "Participate in a community fire drill.", "Safety", DateTime.Now.AddDays(31).AddHours(14), "Cape Town Firestation");
            manager.AddEvent("Gardening Workshop", "Learn how to grow your own vegetables.", "Workshops", DateTime.Now.AddDays(29).AddHours(10), "Woodstock Gardening");
            manager.AddEvent("First Aid Training", "Learn basic first aid skills.", "Health", DateTime.Now.AddDays(19).AddHours(14), "Grooteshuur Hospital");
            manager.AddEvent("Tree Planting Day", "Plant trees with community and help the environment.", "Environment", DateTime.Now.AddDays(8).AddHours(9), "Newlands Park");
            manager.AddEvent("Blood Donation Drive", "Help save lives by donating blood.", "Health", DateTime.Now.AddDays(30).AddHours(9), "Tygerberg Medical  Campus");
            manager.AddEvent("Beach Clean-up Day", "Help keep our beaches clean and safe.", "Environment", DateTime.Now.AddDays(39).AddHours(8), "Clen Beach");
            manager.AddEvent("Farmers’ Market", "Buy fresh produce and handmade goods from local vendors.", "Community", DateTime.Now.AddDays(42).AddHours(8), "Access Park");
            manager.AddEvent("Tech Innovation Workshop", "Hands-on session for learning new tech skills.", "Education", DateTime.Now.AddDays(44).AddHours(14), "UWC Innovation Centre");
            manager.AddEvent("Recycling Drive", "Drop off recyclable items for proper disposal.", "Environment", DateTime.Now.AddDays(46).AddHours(9), "Civic Centre");
            manager.AddEvent("Cooking Class: Healthy Meals", "Learn to cook nutritious meals from scratch.", "Workshops", DateTime.Now.AddDays(60).AddHours(17), "Capsicum Culinary School");
            manager.AddEvent("Open Mic Night", "Showcase your talent or enjoy performances by local artists.", "Entertainment", DateTime.Now.AddDays(58).AddHours(19), "Baxter");
            manager.AddEvent("Charity Fun Run", "Participate in a 5k run to raise funds for local charities.", "Sports", DateTime.Now.AddDays(56).AddHours(8), "Seapoint");
            manager.AddEvent("Public Speaking Workshop", "Improve your speaking and presentation skills.", "Workshops", DateTime.Now.AddDays(20).AddHours(10), "Ca+laremont Library");

        }
    }
}