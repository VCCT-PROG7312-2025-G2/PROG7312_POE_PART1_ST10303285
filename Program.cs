using System;   
using System.Windows.Forms;
using MunicipalServicesApp.Data;
using MunicipalServicesApp.Model;

namespace MunicipalServicesApp
{
    internal static class Program
    {
       
        public static SinglyLinkedList<Issue> Issues = new SinglyLinkedList<Issue>(); // Custom linked list to store issues

        public static EventManager EventsManager = new EventManager();

        

        [STAThread]
        static void Main()
        {
           
            ApplicationConfiguration.Initialize();
            Data.SampleEventSeeder.Seed(EventsManager); // Seed sample events
            var repo = new ServiceRequestRepo();
            SampleRequestSeedercs.SeedIfEmpty(repo); // Seed sample requests if empty
            Application.Run(new MainMenuForm()); // Start with MainMenuForm
        }
    }
}