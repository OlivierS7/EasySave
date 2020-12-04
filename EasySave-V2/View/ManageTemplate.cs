using System;
using System.Windows.Forms;
using NSController;

namespace NSView
{
    public partial class ManageTemplate : UserControl
    {
        public IView View;
        public ManageTemplate()
        {
            InitializeComponent();
            creaSaveTemplate1.Hide();
            delSaveTemplate1.Hide();
            modifySaveTemplate1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HideAll();
            creaSaveTemplate1.controller = View.Controller;
            creaSaveTemplate1.Show();
            creaSaveTemplate1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HideAll();
            delSaveTemplate1.controller = View.Controller;
            delSaveTemplate1.templates = View.Controller.GetAllTemplates();
            delSaveTemplate1.ChangelistBox1();
            delSaveTemplate1.Show();
            delSaveTemplate1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HideAll();
            modifySaveTemplate1.controller = View.Controller;
            modifySaveTemplate1.templates = View.Controller.GetAllTemplates();
            modifySaveTemplate1.ChangelistBox1();
            modifySaveTemplate1.Show();
            modifySaveTemplate1.BringToFront();
        }    
        
        private void HideAll()
        {
            creaSaveTemplate1.Hide();
            delSaveTemplate1.Hide();
            modifySaveTemplate1.Hide();
        }
    }
}
