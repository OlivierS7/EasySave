using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NSController;
using EasySave_V2.Properties;
using System.Threading;
using System.Globalization;

namespace NSView
{
    public partial class EditConfig : UserControl
    {
        public List<string> forbiddenProcesses;
        public List<string> extensionsToEncrypt;
        public StartMenu startMenu;
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

        private void EditConfig_Load(object sender, EventArgs e)
        {
            button3.Hide();
            button4.Hide();
        }

        /* Remove a forbidden process */
        private void button3_Click(object sender, EventArgs e)
        {
            GraphicalView.controller.removeForbiddenProcess(listBox1.SelectedIndex + 1);
            forbiddenProcesses = GraphicalView.controller.getForbiddenProcesses();
            ChangelistBox1();
            button3.Hide();
        }

        /* Remove an extensions to encrypt */
        private void button4_Click(object sender, EventArgs e)
        {
            GraphicalView.controller.removeExtensionToEncrypt(listBox2.SelectedIndex + 1);
            extensionsToEncrypt = GraphicalView.controller.getExtensionsToEncrypt();
            ChangelistBox2();
            button4.Hide();
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

        /* Allows the use of multiple languages */
        public void loadLang()
        {
            label3.Text = Resources.EditParam;
            label1.Text = Resources.Extensions;
            label2.Text = Resources.Softwares;
            button1.Text = Resources.Add;
            button2.Text = Resources.Add;
            button3.Text = Resources.Remove;
            button4.Text = Resources.Remove;
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
    }
}
