﻿
namespace NSView
{
    partial class StartMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.manageTemplate1 = new NSView.ManageTemplate();
            this.executeSaveTemplate1 = new NSView.ExecuteSaveTemplate();
            this.editConfig1 = new NSView.EditConfig();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "Manage Templates";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 128);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 54);
            this.button2.TabIndex = 1;
            this.button2.Text = "Execute save(s)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(24, 236);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 54);
            this.button3.TabIndex = 2;
            this.button3.Text = "Open logs";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(24, 696);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(144, 54);
            this.button4.TabIndex = 3;
            this.button4.Text = "Exit Application";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 905);
            this.panel1.TabIndex = 6;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(24, 344);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(144, 54);
            this.button5.TabIndex = 4;
            this.button5.Text = "Parameters";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // manageTemplate1
            // 
            this.manageTemplate1.Location = new System.Drawing.Point(206, 0);
            this.manageTemplate1.Name = "manageTemplate1";
            this.manageTemplate1.Size = new System.Drawing.Size(1318, 905);
            this.manageTemplate1.TabIndex = 8;
            // 
            // executeSaveTemplate1
            // 
            this.executeSaveTemplate1.Location = new System.Drawing.Point(206, 98);
            this.executeSaveTemplate1.Name = "executeSaveTemplate1";
            this.executeSaveTemplate1.Size = new System.Drawing.Size(949, 518);
            this.executeSaveTemplate1.TabIndex = 9;
            // 
            // editConfig1
            // 
            this.editConfig1.Location = new System.Drawing.Point(478, 112);
            this.editConfig1.Name = "editConfig1";
            this.editConfig1.Size = new System.Drawing.Size(389, 482);
            this.editConfig1.TabIndex = 9;
            this.editConfig1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 696);
            this.Controls.Add(this.executeSaveTemplate1);
            this.ClientSize = new System.Drawing.Size(1524, 905);
            this.Controls.Add(this.editConfig1);
            this.Controls.Add(this.manageTemplate1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel1;
        private NSView.ManageTemplate manageTemplate1;
        private NSView.ExecuteSaveTemplate executeSaveTemplate1;
        private System.Windows.Forms.Button button5;
        private NSView.EditConfig editConfig1;
    }
}

