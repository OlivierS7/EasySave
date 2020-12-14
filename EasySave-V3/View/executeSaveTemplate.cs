using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NSController;
using EasySave_V3.Properties;

namespace NSView
{
    public partial class ExecuteSaveTemplate : UserControl
    {
        public List<string> templates;
        public ExecuteSaveTemplate()
        {
            InitializeComponent();
        }

        private void executeSaveTemplate_Load(object sender, EventArgs e)
        {
            ChangelistBox();
            HideAll();
        }

        /* Execute the selected save */
        private void button1_Click(object sender, EventArgs e)
        { 
            if(listView1.SelectedItems.Count > 0 && listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1 > 0)
            {
                int index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1;
                GraphicalView.controller.ExecuteOneSave(index);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Show();
            button4.Show();
            label1.Show();
        }

        /* Execute all saves */
        private void button3_Click(object sender, EventArgs e)
        {
            GraphicalView.controller.ExecuteAllSave();
            HideAll();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HideAll();
        }

        private void HideAll()
        {
            button3.Hide();
            button4.Hide();
            label1.Hide();
        }

        /* Resresh the list */
        public void ChangelistBox()
        {
            listView1.Items.Clear();
            int index = 1;
            if (templates != null)
            {

                for (int i = 1; i <= templates.Count; i += 4)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = index.ToString();
                    item.SubItems.Add(templates[i - 1]);
                    item.SubItems.Add(templates[i]);
                    item.SubItems.Add(templates[i + 1]);
                    if(templates[i + 2].ToString() == "1")
                        item.SubItems.Add(Resources.FullSave);
                    else
                        item.SubItems.Add(Resources.DifferentialSave);
                    item.SubItems.Add("Not running");
                    item.SubItems.Add("0%");
                    index++;
                    listView1.Items.Add(item);
                }
            }
        }

        /* Allows the use of multiple languages */
        public void loadLang()
        {
            label1.Text = Resources.Sure;
            label2.Text = Resources.ExecTemp;
            button1.Text = Resources.ExecSelec;
            button2.Text = Resources.ExecAll;
            button3.Text = Resources.Yes;
            button4.Text = Resources.No;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1 > 0)
            {
                int index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1;
                GraphicalView.controller.StopThread(index);
            }
        }
    }
}
