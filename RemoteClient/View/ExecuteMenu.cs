using Newtonsoft.Json.Linq;
using NSModel;
using RemoteClient.NSController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RemoteClient.NSView
{
    public partial class ExecuteMenu : UserControl
    {
        public List<SaveTemplate> templates;
        public ExecuteMenu()
        {
            InitializeComponent();
        }
        public void RefreshStatus()
        {
            Controller.refreshStatusDelegate += (name, status) =>
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

        public void RefreshProgress()
        {
            Controller.refreshProgressDelegate += (name, progression) =>
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    for (int i = 0; i < templates.Count; i++)
                    {
                        if (templates[i].backupName == name)
                            listView1.Items[i].SubItems[6].Text = progression.ToString() + "%";
                    }
                }));
            };
        }

        public void ChangeListView()
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
                        item.SubItems.Add("Full save");
                    else
                        item.SubItems.Add("Differential Save");
                    item.SubItems.Add("Ready");
                    item.SubItems.Add("0%");
                    listView1.Items.Add(item);
                }
            }
        }

        private void ExecuteMenu_Load(object sender, EventArgs e)
        {
            ChangeListView();
            RefreshProgress();
            RefreshStatus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1;
            JObject myObject = new JObject(new JProperty("title", "executeOneSave"), new JProperty("index", index));
            GraphicalView.controller.Send(myObject);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            JObject myObject = new JObject(new JProperty("title", "executeAllSave"));
            GraphicalView.controller.Send(myObject);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1;
            JObject myObject = new JObject(new JProperty("title", "abortExecution"), new JProperty("index", index));
            GraphicalView.controller.Send(myObject);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1;
            JObject myObject = new JObject(new JProperty("title", "pauseExecution"), new JProperty("index", index));
            GraphicalView.controller.Send(myObject);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1;
            JObject myObject = new JObject(new JProperty("title", "resumeExecution"), new JProperty("index", index));
            GraphicalView.controller.Send(myObject);
        }
    }
}
