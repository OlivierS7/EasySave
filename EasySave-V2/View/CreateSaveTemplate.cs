using System;
using System.Windows.Forms;
using NSController;
using EasySave_V2.Properties;

namespace NSView
{
    public partial class creaSaveTemplate : UserControl
    {
        public Controller controller;
        public creaSaveTemplate()
        {
            InitializeComponent();
        }

        /* Open folder navigation */
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                this.textBox2.Text = fdb.SelectedPath;
            }

        }

        /* Open folder navigation */
        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                this.textBox3.Text = fdb.SelectedPath;
            }
        }

        /* Create a save template with provided informations */
        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string srcDir = textBox2.Text;
            string destDir = textBox3.Text;
            int type = comboBox1.SelectedIndex + 1;
            GraphicalView.controller.CreateSaveTemplate(name, srcDir, destDir, type);
        }

        private void creaSaveTemplate_Load(object sender, EventArgs e)
        {
            
        }

        /* Allows the use of multiple languages */
        public void loadLang()
        {
            label1.Text = Resources.DestDir;
            label2.Text = Resources.SrcDir;
            label3.Text = Resources.SaveType;
            button2.Text = Resources.Confirm;
            SaveTemplateName.Text = Resources.Name;
        }
    }
}
