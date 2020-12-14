using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EasySave_V3.Properties;
using NSModel;

namespace NSView
{
    public partial class ExecuteSaveTemplate : UserControl
    {
        public List<SaveTemplate> templates;
        public delegate void StatusDelegate();
        public StatusDelegate refreshStatusDelegate;


        public void RefreshStatus()
        {
            GraphicalView.controller.model.refreshStatusDelegate += (name, status) =>
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    for (int i = 0; i < templates.Count; i++)
                    {
                        if (templates[i].backupName == name)
                            listView1.Items[i].SubItems[5].Text = status;
                    }
                }));
            };
        }

        public ExecuteSaveTemplate()
        {
            InitializeComponent();
        }

        private void executeSaveTemplate_Load(object sender, EventArgs e)
        {
            RefreshStatus();
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
            if (templates != null)
            {
                for (int i = 0; i < templates.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = (i+1).ToString();
                    item.SubItems.Add(templates[i].backupName);
                    item.SubItems.Add(templates[i].srcDirectory);
                    item.SubItems.Add(templates[i].destDirectory);
                    if(templates[i].backupType == 1)
                        item.SubItems.Add(Resources.FullSave);
                    else
                        item.SubItems.Add(Resources.DifferentialSave);
                    item.SubItems.Add(Resources.Paused);
                    item.SubItems.Add("0%");
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1 > 0)
            {
                int index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1;
                GraphicalView.controller.PauseOrResume(index, false);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1 > 0)
            {
                int index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1;
                GraphicalView.controller.PauseOrResume(index, true);
            }
        }
    }
}
