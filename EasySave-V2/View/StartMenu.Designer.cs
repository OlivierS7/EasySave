
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartMenu));
            this.manageTemplate1 = new NSView.ManageTemplate();
            this.executeSaveTemplate1 = new NSView.ExecuteSaveTemplate();
            this.editConfig1 = new NSView.EditConfig();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // manageTemplate1
            // 
            this.manageTemplate1.BackColor = System.Drawing.Color.Transparent;
            this.manageTemplate1.Location = new System.Drawing.Point(233, 12);
            this.manageTemplate1.Name = "manageTemplate1";
            this.manageTemplate1.Size = new System.Drawing.Size(1279, 881);
            this.manageTemplate1.TabIndex = 8;
            // 
            // executeSaveTemplate1
            // 
            this.executeSaveTemplate1.BackColor = System.Drawing.Color.Transparent;
            this.executeSaveTemplate1.Location = new System.Drawing.Point(233, 93);
            this.executeSaveTemplate1.Name = "executeSaveTemplate1";
            this.executeSaveTemplate1.Size = new System.Drawing.Size(934, 518);
            this.executeSaveTemplate1.TabIndex = 9;
            // 
            // editConfig1
            // 
            this.editConfig1.BackColor = System.Drawing.Color.Transparent;
            this.editConfig1.Location = new System.Drawing.Point(478, 112);
            this.editConfig1.Name = "editConfig1";
            this.editConfig1.Size = new System.Drawing.Size(389, 482);
            this.editConfig1.TabIndex = 9;
            this.editConfig1.Visible = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.button5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button5.ForeColor = System.Drawing.Color.Gainsboro;
            this.button5.Location = new System.Drawing.Point(23, 576);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(169, 90);
            this.button5.TabIndex = 4;
            this.button5.Text = "Parameters";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button4.Location = new System.Drawing.Point(36, 739);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(144, 54);
            this.button4.TabIndex = 3;
            this.button4.Text = "Exit Application";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.button3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button3.ForeColor = System.Drawing.Color.Gainsboro;
            this.button3.Location = new System.Drawing.Point(23, 389);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(169, 90);
            this.button3.TabIndex = 2;
            this.button3.Text = "Open logs";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.ForeColor = System.Drawing.Color.Gainsboro;
            this.button2.Location = new System.Drawing.Point(23, 202);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(169, 90);
            this.button2.TabIndex = 1;
            this.button2.Text = "Execute save(s)";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.Gainsboro;
            this.button1.Location = new System.Drawing.Point(23, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 90);
            this.button1.TabIndex = 0;
            this.button1.Text = "Manage Templates";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 881);
            this.panel1.TabIndex = 10;
            // 
            // StartMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(112)))), ((int)(((byte)(193)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1524, 905);
            this.Controls.Add(this.executeSaveTemplate1);
            this.Controls.Add(this.editConfig1);
            this.Controls.Add(this.manageTemplate1);
            this.Controls.Add(this.panel1);
            this.Name = "StartMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EasySave";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private NSView.ManageTemplate manageTemplate1;
        private NSView.ExecuteSaveTemplate executeSaveTemplate1;
        private NSView.EditConfig editConfig1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
    }
}

