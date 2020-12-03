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
            executeSaveTemplate1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            executeSaveTemplate1.Hide();
            manageTemplate1.Show();
            manageTemplate1.controller = this.controller;
            manageTemplate1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            manageTemplate1.Hide();
            executeSaveTemplate1.Show();
            executeSaveTemplate1.controller = this.controller;
            executeSaveTemplate1.templates = this.controller.GetAllTemplates();
            executeSaveTemplate1.ChangelistBox();
            executeSaveTemplate1.BringToFront();

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
    }
}
