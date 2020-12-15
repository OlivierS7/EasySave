using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EasySave_V3.Properties;
using NSModel;

namespace NSView
{
    public partial class ModifySaveTemplate : UserControl
    {
        public List<SaveTemplate> templates;
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
            
        }

        /* Modify the selected save template */
        private void Confirm_Click(object sender, EventArgs e)
        {
            int index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1;
            if (index > 0)
            {
                GraphicalView.controller.ModifySaveTemplate(index, textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.SelectedIndex + 1);
                this.templates = GraphicalView.controller.GetAllTemplates();
                ChangelistBox1();
            }
        }

        /* Show all existing save templates */
        public void ChangelistBox1()
        {
            listView1.Items.Clear();
            if (templates != null)
            {
                for (int i = 0; i < templates.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = (i + 1).ToString();
                    item.SubItems.Add(templates[i].backupName);
                    item.SubItems.Add(templates[i].srcDirectory);
                    item.SubItems.Add(templates[i].destDirectory);
                    if (templates[i].backupType == 1)
                        item.SubItems.Add(Resources.FullSave);
                    else
                        item.SubItems.Add(Resources.DifferentialSave);
                    listView1.Items.Add(item);
                }
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            if(listView1.SelectedItems.Count - 1 >= 0)
            {
                index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index;
                textBox1.Text = listView1.Items[index].SubItems[1].Text;
                textBox2.Text = listView1.Items[index].SubItems[2].Text;
                textBox3.Text = listView1.Items[index].SubItems[3].Text;
                if (listView1.Items[index].SubItems[4].Text == "Full")
                    comboBox1.Text = "Full Save";
                else
                    comboBox1.Text = "Differential Save";
            }
        }
    }
}
