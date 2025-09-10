using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MunicipalServicesApp.Model;
using MunicipalServicesApp.Data;


namespace MunicipalServicesApp
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            var f = new ReportIssuesForm();
            f.FormClosed += (s, args) => Show();
            f.Show();
            Hide();
        }

        private void BtnEvents_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is coming in a later release.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnStatus_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is coming in a later release.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
