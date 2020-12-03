using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NSController;
using System.Diagnostics;

namespace EasySave_V2.View
{
    public partial class creaSaveTemplate : UserControl
    {
        public Controller controller;
        public creaSaveTemplate()
        {
            InitializeComponent();
        }

        private void SaveTemplateName_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                this.textBox2.Text = fdb.SelectedPath;
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                this.textBox3.Text = fdb.SelectedPath;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string srcDir = textBox2.Text;
            string destDir = textBox3.Text;
            int type = comboBox1.SelectedIndex + 1;
            this.controller.CreateSaveTemplate(name, srcDir, destDir, type);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
