using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteClient.NSView
{
    public partial class StartMenu : Form
    {
        public StartMenu()
        {
            InitializeComponent();
            executeMenu1.Hide();
        }

        private void StartMenu_Load(object sender, EventArgs e)
        {

        }

        public void PrintMessage(string message, int type)
        {
            
        }

        private void Connection_Click(object sender, EventArgs e)
        {
            string ip = textBox1.Text;
            int port;
            Int32.TryParse(textBox2.Text, out port);
            GraphicalView.controller.Connexion(ip, port);
            JObject myObject = new JObject(new JProperty("title", "getAllTemplates"));
            GraphicalView.controller.Send(myObject);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!executeMenu1.Visible)
            {
                executeMenu1.Show();
            }
            executeMenu1.templates = GraphicalView.controller.GetTemplates();
            executeMenu1.ChangeListView();
            executeMenu1.BringToFront();
        }
    }
}
