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
            if (type == -1)
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (type == 1)
                MessageBox.Show(message, "Operation success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Connection_Click(object sender, EventArgs e)
        {
            string ip = textBox1.Text;
            int port;
            Int32.TryParse(textBox2.Text, out port);
            try
            {
                GraphicalView.controller.Connexion(ip, port);
                JObject myObject = new JObject(new JProperty("title", "getAllTemplates"));
                GraphicalView.controller.Send(myObject);
                PrintMessage("Connection successfull to the server !", 1);
            } catch
            {
                PrintMessage("Can't connect to this server please try again !", -1);
            }
           
            
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

        private void button3_Click(object sender, EventArgs e)
        {
            executeMenu1.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GraphicalView.controller.Disconnect();
            Environment.Exit(1);
        }

        private void executeMenu1_Load(object sender, EventArgs e)
        {

        }
    }
}
