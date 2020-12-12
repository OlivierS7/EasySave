using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EasySave_V3.Properties;
using System.Threading;
using System.Globalization;

namespace NSView
{
    public partial class EditConfig : UserControl
    {
        public List<string> forbiddenProcesses;
        public List<string> extensionsToEncrypt;
        public List<string> priorityFilesExtensions;
        public string maxFileSize;
        public StartMenu startMenu;
        public EditConfig()
        {
            InitializeComponent();
        }

        private void EditConfig_Load(object sender, EventArgs e)
        {
            button3.Hide();
            button4.Hide();
            button6.Hide();
        }


        /* Add a forbidden procces */
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                bool isEgal = false;
                foreach(string item in listBox1.Items)
                {
                    if (textBox1.Text == item)
                        isEgal = true;
                }
                if (!isEgal)
                {
                    GraphicalView.controller.addForbiddenProcess(textBox1.Text);
                    forbiddenProcesses = GraphicalView.controller.getForbiddenProcesses();
                    ChangelistBox1();
                    button3.Hide();
                }
            }  
        }

        /* Add an extension to encrypt */
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                bool isEgal = false;
                foreach (string item in listBox2.Items)
                {
                    if (textBox2.Text == item)
                        isEgal = true;
                }
                if (!isEgal)
                {
                    GraphicalView.controller.addExtensionToEncrypt(textBox2.Text);
                    extensionsToEncrypt = GraphicalView.controller.getExtensionsToEncrypt();
                    ChangelistBox2();
                    button4.Hide();
                }
            }
        }

        /* Add a priority files extension */
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                bool isEgal = false;
                foreach (string item in listBox3.Items)
                {
                    if (textBox3.Text == item)
                        isEgal = true;
                }
                if (!isEgal)
                {
                    GraphicalView.controller.addPriorityFilesExtension(textBox3.Text);
                    priorityFilesExtensions = GraphicalView.controller.getpriorityFilesExtensions();
                    ChangelistBox3();
                    button6.Hide();
                }
            }
        }


        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                GraphicalView.controller.addMaxFileSize(textBox4.Text);
                maxFileSize = GraphicalView.controller.getMaxFileSize().ToString();
            }
        }

        /* Remove a forbidden process */
        private void button3_Click(object sender, EventArgs e)
        {
            GraphicalView.controller.removeForbiddenProcess(listBox1.SelectedIndex + 1);
            forbiddenProcesses = GraphicalView.controller.getForbiddenProcesses();
            ChangelistBox1();
            button3.Hide();
        }

        /* Remove an extension to encrypt */
        private void button4_Click(object sender, EventArgs e)
        {
            GraphicalView.controller.removeExtensionToEncrypt(listBox2.SelectedIndex + 1);
            extensionsToEncrypt = GraphicalView.controller.getExtensionsToEncrypt();
            ChangelistBox2();
            button4.Hide();
        }

        /* Remove an priority files extension */
        private void button6_Click(object sender, EventArgs e)
        {
            GraphicalView.controller.removePriorityFilesExtension(listBox3.SelectedIndex + 1);
            priorityFilesExtensions = GraphicalView.controller.getpriorityFilesExtensions();
            ChangelistBox3();
            button6.Hide();
        }

        /* Refresh the list */
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

        /* Refresh the list */
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

        /* Refresh the list */
        public void ChangelistBox3()
        {
            listBox3.Items.Clear();
            if (priorityFilesExtensions != null)
            {
                for (int i = 0; i < priorityFilesExtensions.Count; i++)
                {
                    listBox3.Items.Add("" + priorityFilesExtensions[i]);
                };
            }
        }

        public void ChangetextBox4()
        {
            textBox4.Text = maxFileSize;
        }

        /* Allows the use of multiple languages */
        public void loadLang()
        {
            label3.Text = Resources.EditParam;
            label1.Text = Resources.Extensions;
            label2.Text = Resources.Softwares;
            label5.Text = Resources.PriorityExtensions;
            label6.Text = Resources.MaxFileSize;
            button1.Text = Resources.Add;
            button2.Text = Resources.Add;
            button5.Text = Resources.Add;
            button3.Text = Resources.Remove;
            button4.Text = Resources.Remove;
            button6.Text = Resources.Remove;
        }

       /* Change language */
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                    break;
                case 1:
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");
                    break;
                case 2:
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ko-KR");
                    break;
            }
            this.startMenu.LoadAllLang();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Show();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button4.Show();
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            button6.Show();
        }
    }
}
