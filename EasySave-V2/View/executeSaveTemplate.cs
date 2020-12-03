using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NSController;

namespace EasySave_V2.View
{
    public partial class ExecuteSaveTemplate : UserControl
    {
        public Controller controller;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.controller.ExecuteOneSave(listBox1.SelectedIndex + 1);
        }
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

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Show();
            button4.Show();
            label1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            controller.ExecuteAllSave();
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
    }
}
