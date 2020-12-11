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
            if(listBox1.SelectedIndex + 1 > 0) 
                GraphicalView.controller.ExecuteOneSave(listBox1.SelectedIndex + 1);
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
            listBox1.Items.Clear();
            int index = 1;
            if (templates != null)
            {

                for (int i = 1; i <= templates.Count; i += 4)
                {
                    listBox1.Items.Add("ID : " + index + " | Name : " + templates[i - 1] + " | Source Directory : " + templates[i] + " | Destination Directory : " + templates[i + 1] + " | Backup Type : " + templates[i + 2]);
                    index++;
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
    }
}
