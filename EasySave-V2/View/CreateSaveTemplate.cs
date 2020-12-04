using System;
using System.Windows.Forms;
using NSController;

namespace NSView
{
    public partial class creaSaveTemplate : UserControl
    {
        public Controller controller;
        public creaSaveTemplate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                this.textBox2.Text = fdb.SelectedPath;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                this.textBox3.Text = fdb.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string srcDir = textBox2.Text;
            string destDir = textBox3.Text;
            int type = comboBox1.SelectedIndex + 1;
            GraphicalView.controller.CreateSaveTemplate(name, srcDir, destDir, type);
        }
    }
}
