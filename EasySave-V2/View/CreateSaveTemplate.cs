﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EasySave_V2.View
{
    public partial class creaSaveTemplate : UserControl
    {

        public creaSaveTemplate()
        {
            InitializeComponent();
        }

        private void SaveTemplateName_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox2.Text = fdb.SelectedPath;
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox3.Text = fdb.SelectedPath;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] items =
            {
                "Full save",
                "Differential save"
            };
            comboBox1.Items.AddRange(items);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
