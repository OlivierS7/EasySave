using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NSController;

namespace EasySave_V2.View
{
    public partial class ModifySaveTemplate : UserControl
    {
        public Controller controller;
        public List<string> templates;
        public ModifySaveTemplate()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                this.textBox3.Text = fdb.SelectedPath;
            }
        }

        public void ChangelistBox1()
        {
            listBox1.Items.Clear();
            int index = 1;
            if (templates != null)
            {
                for (int i = 1; i <= templates.Count; i += 4)
                {
                    listBox1.Items.Add("ID : " + index + " | Name : " + templates[i - 1] + " | Source Directory : " + templates[i] + " | Destination Directory : " + templates[i + 1] + " | Backup Type : " + templates[i + 2]);
                    index++;
                };
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] splitString = listBox1.SelectedItem.ToString().Split(" | ");
            textBox1.Text = splitString[1].Split(" : ")[1];
            textBox2.Text = splitString[2].Split(" : ")[1];
            textBox3.Text = splitString[3].Split(" : ")[1];
            textBox4.Text = splitString[4].Split(" : ")[1];
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            string[] splitString = listBox1.SelectedItem.ToString().Split(" | ");
            int id = Convert.ToInt32(splitString[0].Split(" : ")[1]);
            this.controller.ModifySaveTemplate(id, textBox1.Text, textBox2.Text, textBox3.Text, Convert.ToInt32(textBox4.Text));
            this.templates = this.controller.GetAllTemplates();
            ChangelistBox1();
        }
    }
}
