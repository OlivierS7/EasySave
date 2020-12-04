using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NSController;

namespace NSView
{
    public partial class EditConfig : UserControl
    {
        public Controller controller;
        public List<string> forbiddenProcesses;
        public List<string> extensionsToEncrypt;
        public EditConfig()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Show();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button4.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                controller.addForbiddenProcess(textBox1.Text);
                forbiddenProcesses = controller.getForbiddenProcesses();
                ChangelistBox1();
                button3.Hide();
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                controller.addExtensionToEncrypt(textBox2.Text);
                extensionsToEncrypt = controller.getExtensionsToEncrypt();
                ChangelistBox2();
                button4.Hide();
            }
        }

        private void EditConfig_Load(object sender, EventArgs e)
        {
            button3.Hide();
            button4.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            controller.removeForbiddenProcess(listBox1.SelectedIndex + 1);
            forbiddenProcesses = controller.getForbiddenProcesses();
            ChangelistBox1();
            button3.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            controller.removeExtensionToEncrypt(listBox2.SelectedIndex + 1);
            extensionsToEncrypt = controller.getExtensionsToEncrypt();
            ChangelistBox2();
            button4.Hide();
        }

        public void ChangelistBox1()
        {
            listBox1.Items.Clear();
            if (forbiddenProcesses != null)
            {
                for (int i = 0; i < forbiddenProcesses.Count; i++)
                {
                    listBox1.Items.Add("" + forbiddenProcesses[i]);
                };
            }
        }
        public void ChangelistBox2()
        {
            listBox2.Items.Clear();
            if (extensionsToEncrypt != null)
            {
                for (int i = 0; i < extensionsToEncrypt.Count; i++)
                {
                    listBox2.Items.Add("" + extensionsToEncrypt[i]);
                };
            }
        }
    }
}
