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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            manageTemplate1.Show();
            manageTemplate1.BringToFront();
            manageTemplate1.controller = this.controller;
        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.controller.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void createSaveTemplate1_Load(object sender, EventArgs e)
        {

        }

        private void manageTemplate1_Load(object sender, EventArgs e)
        {

        }

        private void manageTemplate1_Load_1(object sender, EventArgs e)
        {

        }

        public void PrintMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
