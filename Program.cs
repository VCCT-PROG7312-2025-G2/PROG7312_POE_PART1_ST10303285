using System;   
using System.Windows.Forms;
using MunicipalServicesApp.Data;
using MunicipalServicesApp.Model;

namespace MunicipalServicesApp
{
    internal static class Program
    {
       
        public static SinglyLinkedList<Issue> Issues = new SinglyLinkedList<Issue>();
        
        [STAThread]
        static void Main()
        {
           
            ApplicationConfiguration.Initialize();
            Application.Run(new MainMenuForm());
        }
    }
}