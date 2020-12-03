using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NSController;

namespace EasySave_V2.View
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Show();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button4.Show();
        }
        private void ChangelistBox1()
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
        private void ChangelistBox2()
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

        private void button1_Click(object sender, EventArgs e)
        {
            controller.addForbiddenProcess(textBox1.Text);
            forbiddenProcesses = controller.getForbiddenProcesses();
            ChangelistBox1();
            button3.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            controller.addExtensionToEncrypt(textBox2.Text);
            extensionsToEncrypt = controller.getExtensionsToEncrypt();
            ChangelistBox2();
            button4.Hide();
        }

        private void EditConfig_Load(object sender, EventArgs e)
        {
            ChangelistBox1();
            ChangelistBox2();
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
    }
}
