using Newtonsoft.Json.Linq;
using NSModel;
using RemoteClient.NSController;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RemoteClient.NSView
{
    public partial class ExecuteMenu : UserControl
    {
        public List<SaveTemplate> templates;

        /* Constructor */
        public ExecuteMenu()
        {
            InitializeComponent();
        }

        /* Method to refresh status of saves */
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

        /* Method to refresh progress of saves */
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

        /* Method to load saves */
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

        /* On load method */
        private void ExecuteMenu_Load(object sender, EventArgs e)
        {
            ChangeListView();
            RefreshProgress();
            RefreshStatus();
        }

        /* Method to execute one save */
        private void button1_Click(object sender, EventArgs e)
        {
            int index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1;
            JObject myObject = new JObject(new JProperty("title", "executeOneSave"), new JProperty("index", index));
            GraphicalView.controller.Send(myObject);
        }

        /* Method to execute all saves */
        private void button2_Click(object sender, EventArgs e)
        {
            JObject myObject = new JObject(new JProperty("title", "executeAllSave"));
            GraphicalView.controller.Send(myObject);
        }

        /* Method to abort one save */
        private void button3_Click(object sender, EventArgs e)
        {
            int index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1;
            JObject myObject = new JObject(new JProperty("title", "abortExecution"), new JProperty("index", index));
            GraphicalView.controller.Send(myObject);
        }

        /* Method to pause one save */
        private void button4_Click(object sender, EventArgs e)
        {
            int index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1;
            JObject myObject = new JObject(new JProperty("title", "pauseExecution"), new JProperty("index", index));
            GraphicalView.controller.Send(myObject);
        }

        /* Method to resume one save */
        private void button5_Click(object sender, EventArgs e)
        {
            int index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1;
            JObject myObject = new JObject(new JProperty("title", "resumeExecution"), new JProperty("index", index));
            GraphicalView.controller.Send(myObject);
        }
    }
}
