using System;   
using System.Windows.Forms;
using MunicipalServicesApp.Data;
using MunicipalServicesApp.Model;

namespace MunicipalServicesApp
{
    internal static class Program
    {
       
        public static SinglyLinkedList<Issue> Issues = new SinglyLinkedList<Issue>(); // Custom linked list to store issues

        [STAThread]
        static void Main()
        {
           
            ApplicationConfiguration.Initialize();
            Application.Run(new MainMenuForm()); // Start with MainMenuForm
        }
    }
}