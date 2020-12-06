using System;
using System.Windows.Forms;
using NSController;
using EasySave_V2.Properties;

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
            if (!creaSaveTemplate1.Visible)
            {
                HideAll();
                creaSaveTemplate1.Show();
            }
            creaSaveTemplate1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!delSaveTemplate1.Visible)
            {
                HideAll();
            }
            delSaveTemplate1.templates = GraphicalView.controller.GetAllTemplates();
            delSaveTemplate1.ChangelistBox1();
            delSaveTemplate1.Show();
            delSaveTemplate1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!modifySaveTemplate1.Visible)
            {
                HideAll();
            }
            modifySaveTemplate1.templates = GraphicalView.controller.GetAllTemplates();
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

        private void modifySaveTemplate1_Load(object sender, EventArgs e)
        {

        }

        private void ManageTemplate_Load(object sender, EventArgs e)
        {
            
        }
        public void loadLang()
        {
            button1.Text = Resources.Create;
            button2.Text = Resources.Delete;
            button3.Text = Resources.Modif;
            creaSaveTemplate1.loadLang();
            modifySaveTemplate1.loadLang();
            delSaveTemplate1.loadLang();
        }
    }
}
