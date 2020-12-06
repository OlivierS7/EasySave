using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NSController;
using EasySave_V2.Properties;

namespace NSView
{
    public partial class ModifySaveTemplate : UserControl
    {
        public List<string> templates;
        public ModifySaveTemplate()
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
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                this.textBox3.Text = fdb.SelectedPath;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] splitString = listBox1.SelectedItem.ToString().Split(" | ");
            textBox1.Text = splitString[1].Split(" : ")[1];
            textBox2.Text = splitString[2].Split(" : ")[1];
            textBox3.Text = splitString[3].Split(" : ")[1];
            if (splitString[4].Split(" : ")[1] == "1")
                comboBox1.Text = "Full Save";
            else
                comboBox1.Text = "Differential Save";
        }

        /* Modify the selected save template */
        private void Confirm_Click(object sender, EventArgs e)
        {   
            if(listBox1.SelectedIndex + 1 > 0)
            {
                string[] splitString = listBox1.SelectedItem.ToString().Split(" | ");
                int id = Convert.ToInt32(splitString[0].Split(" : ")[1]);
                GraphicalView.controller.ModifySaveTemplate(id, textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.SelectedIndex + 1);
                this.templates = GraphicalView.controller.GetAllTemplates();
                ChangelistBox1();
            }
        }

        /* Show all existing save templates */
        public void ChangelistBox1()
        {
            listBox1.Items.Clear();
            int index = 1;
            if (templates != null)
            {
                for (int i = 1; i <= templates.Count; i += 4)
                {
                    listBox1.Items.Add("ID : " + index + " | " + Resources.Name + " : " + templates[i - 1] + " | " + Resources.SrcDir + " : " + templates[i] + " | " + Resources.DestDir + " : " + templates[i + 1] + " | " + Resources.SaveType + " : " + templates[i + 2]);
                    index++;
                };
            }
        }

        /* Allows the use of multiple languages */
        public void loadLang()
        {
            label1.Text = Resources.ModifTemp;
            Confirm.Text = Resources.Confirm;
            label2.Text = Resources.Name;
            label3.Text = Resources.SrcDir;
            label4.Text = Resources.DestDir;
            label5.Text = Resources.SaveType;
        }
    }
}
