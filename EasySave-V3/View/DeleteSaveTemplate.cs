using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EasySave_V3.Properties;
using NSModel;

namespace NSView
{
    public partial class DeleteSaveTemplate : UserControl
    {
        public List<SaveTemplate> templates;

        /* Constructor */
        public DeleteSaveTemplate()
        {
            InitializeComponent();
        }

        /* Delete a save template from provided informations */
        private void button1_Click(object sender, EventArgs e)
        {
            int index = listView1.SelectedItems[listView1.SelectedItems.Count - 1].Index + 1;
            if (listView1.SelectedItems.Count > 0 && index > 0)
            {
                GraphicalView.controller.DeleteSaveTemplate(index);
                this.templates = GraphicalView.controller.GetAllTemplates();
                ChangelistBox1();
            } 
        }

        /* Refrash the list */
        private void DelSaveTemplate_Load(object sender, EventArgs e)
        {
            ChangelistBox1();
        }

        /* Refresh the list */
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
            label1.Text = Resources.DelTemp;
            button1.Text = Resources.Confirm;
        }
    }
}
