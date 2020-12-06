using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NSController;
using EasySave_V2.Properties;

namespace NSView
{
    public partial class DeleteSaveTemplate : UserControl
    {
        public List<string> templates;
        public DeleteSaveTemplate()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex + 1 > 0)
            {
                GraphicalView.controller.DeleteSaveTemplate(listBox1.SelectedIndex + 1);
                this.templates = GraphicalView.controller.GetAllTemplates();
                ChangelistBox1();
            } 
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
                    listBox1.Items.Add("ID : " + index + " | " + Resources.Name + " : " + templates[i - 1] + " | " + Resources.SrcDir + " : " + templates[i] + " | " + Resources.DestDir + " : " + templates[i + 1] + " | " + Resources.SaveType + " : " + templates[i + 2]);
                    index++;
                };
            }
        }
        public void loadLang()
        {
            label1.Text = Resources.DelTemp;
            button1.Text = Resources.Confirm;
        }
    }
}
