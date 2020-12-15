using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteClient.NSView
{
    public partial class StartMenu : Form
    {
        public StartMenu()
        {
            InitializeComponent();
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
        }
    }
}
