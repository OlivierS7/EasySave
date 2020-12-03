using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NSController;

namespace NSView
{
    public partial class Form1 : Form
    {
        private Controller controller;
        public Form1(Controller controller)
        {
            this.controller = controller;
            InitializeComponent(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            manageTemplate1.Hide();
            editConfig1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            manageTemplate1.Show();
            manageTemplate1.controller = this.controller;
            manageTemplate1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.controller.OpenLogs();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.controller.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void PrintMessage(string message, int type)
        {
            if (type == -1)
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (type == 1)
                MessageBox.Show(message, "Operation success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void manageTemplate1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.hideAll();
            editConfig1.controller = this.controller;
            editConfig1.allowedProcesses = this.controller.getAllowedProcesses();
            editConfig1.extensionsToEncrypt = this.controller.getExtensionsToEncrypt();
            editConfig1.Show();
        }

        private void hideAll()
        {
            editConfig1.Hide();
            manageTemplate1.Hide();
        }
    }
}
