using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EasySave_V2.View
{
    public partial class ManageTemplate : UserControl
    {
        public ManageTemplate()
        {
            InitializeComponent();
            creaSaveTemplate1.Hide();
            
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            creaSaveTemplate1.Show();
            creaSaveTemplate1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
        }
    }
}
