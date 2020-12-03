using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NSController;
using System.Diagnostics;

namespace EasySave_V2.View
{
    public partial class DelSaveTemplate : UserControl
    {
        public Controller controller;
        public List<string> templates;
        public DelSaveTemplate()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.controller.DeleteSaveTemplate(listBox1.SelectedIndex + 1);
            this.templates = this.controller.GetAllTemplates();
            ChangelistBox1();
        }


        private void DelSaveTemplate_Load(object sender, EventArgs e)
        {
            ChangelistBox1();
        }

        public void ChangelistBox1()
        {
            listBox1.Items.Clear();
            int index = 1;
            if(templates != null)
            {
                for (int i = 1; i <= templates.Count; i += 4)
                {
                    listBox1.Items.Add("ID : " + index + " | Name : " + templates[i - 1] + " | Source Directory : " + templates[i] + " | Destination Directory : " + templates[i + 1] + " | Backup Type : " + templates[i + 2]);
                    index++;
                };
            }
        }
    }
}
