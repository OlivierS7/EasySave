
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
            this.aboutUs1 = new NSView.AboutUs();
            this.AboutUs = new System.Windows.Forms.Button();
            this.parameters = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Execute = new System.Windows.Forms.Button();
            this.Manage = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // manageTemplate1
            // 
            this.manageTemplate1.BackColor = System.Drawing.Color.Transparent;
            this.manageTemplate1.Location = new System.Drawing.Point(-3, 75);
            this.manageTemplate1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.manageTemplate1.Name = "manageTemplate1";
            this.manageTemplate1.Size = new System.Drawing.Size(1527, 822);
            this.manageTemplate1.TabIndex = 8;
            this.manageTemplate1.Visible = false;
            // 
            // executeSaveTemplate1
            // 
            this.executeSaveTemplate1.BackColor = System.Drawing.Color.Transparent;
            this.executeSaveTemplate1.Location = new System.Drawing.Point(13, 96);
            this.executeSaveTemplate1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.executeSaveTemplate1.Name = "executeSaveTemplate1";
            this.executeSaveTemplate1.Size = new System.Drawing.Size(1498, 776);
            this.executeSaveTemplate1.TabIndex = 9;
            this.executeSaveTemplate1.Visible = false;
            // 
            // editConfig1
            // 
            this.editConfig1.BackColor = System.Drawing.Color.Transparent;
            this.editConfig1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.editConfig1.Location = new System.Drawing.Point(-3, 48);
            this.editConfig1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.editConfig1.Name = "editConfig1";
            this.editConfig1.Size = new System.Drawing.Size(1498, 833);
            this.editConfig1.TabIndex = 9;
            this.editConfig1.Visible = false;
            // 
            // aboutUs1
            // 
            this.aboutUs1.BackColor = System.Drawing.Color.Transparent;
            this.aboutUs1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.aboutUs1.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.aboutUs1.ForeColor = System.Drawing.Color.Transparent;
            this.aboutUs1.Location = new System.Drawing.Point(13, 49);
            this.aboutUs1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.aboutUs1.Name = "aboutUs1";
            this.aboutUs1.Size = new System.Drawing.Size(1514, 832);
            this.aboutUs1.TabIndex = 11;
            // 
            // AboutUs
            // 
            this.AboutUs.BackColor = System.Drawing.Color.AliceBlue;
            this.AboutUs.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AboutUs.Location = new System.Drawing.Point(1052, 0);
            this.AboutUs.Name = "AboutUs";
            this.AboutUs.Size = new System.Drawing.Size(147, 42);
            this.AboutUs.TabIndex = 12;
            this.AboutUs.Text = "About us";
            this.AboutUs.UseVisualStyleBackColor = false;
            this.AboutUs.Click += new System.EventHandler(this.AboutUs_Click);
            // 
            // parameters
            // 
            this.parameters.BackColor = System.Drawing.Color.AliceBlue;
            this.parameters.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.parameters.Location = new System.Drawing.Point(839, 0);
            this.parameters.Name = "parameters";
            this.parameters.Size = new System.Drawing.Size(216, 42);
            this.parameters.TabIndex = 13;
            this.parameters.Text = "Parameters";
            this.parameters.UseVisualStyleBackColor = false;
            this.parameters.Click += new System.EventHandler(this.parameters_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.AliceBlue;
            this.button1.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(566, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(276, 42);
            this.button1.TabIndex = 14;
            this.button1.Text = "Open Logs";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.OpenLogs_Click);
            // 
            // Execute
            // 
            this.Execute.BackColor = System.Drawing.Color.AliceBlue;
            this.Execute.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Execute.Location = new System.Drawing.Point(286, 0);
            this.Execute.Name = "Execute";
            this.Execute.Size = new System.Drawing.Size(283, 42);
            this.Execute.TabIndex = 15;
            this.Execute.Text = "Execute save(s)";
            this.Execute.UseVisualStyleBackColor = false;
            this.Execute.Click += new System.EventHandler(this.Execute_Click);
            // 
            // Manage
            // 
            this.Manage.BackColor = System.Drawing.Color.AliceBlue;
            this.Manage.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Manage.Location = new System.Drawing.Point(-3, 0);
            this.Manage.Name = "Manage";
            this.Manage.Size = new System.Drawing.Size(292, 42);
            this.Manage.TabIndex = 16;
            this.Manage.Text = "Manage save template(s)";
            this.Manage.UseVisualStyleBackColor = false;
            this.Manage.Click += new System.EventHandler(this.Manage_Click);
            // 
            // Exit
            // 
            this.Exit.BackColor = System.Drawing.Color.AliceBlue;
            this.Exit.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Exit.Location = new System.Drawing.Point(1345, 0);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(179, 42);
            this.Exit.TabIndex = 17;
            this.Exit.Text = "Exit Application";
            this.Exit.UseVisualStyleBackColor = false;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // StartMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(112)))), ((int)(((byte)(193)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1524, 893);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Manage);
            this.Controls.Add(this.Execute);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.parameters);
            this.Controls.Add(this.AboutUs);
            this.Controls.Add(this.aboutUs1);
            this.Controls.Add(this.executeSaveTemplate1);
            this.Controls.Add(this.editConfig1);
            this.Controls.Add(this.manageTemplate1);
            this.Font = new System.Drawing.Font("Broadway", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MaximumSize = new System.Drawing.Size(1540, 932);
            this.MinimumSize = new System.Drawing.Size(1540, 932);
            this.Name = "StartMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EasySave V2.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private NSView.ManageTemplate manageTemplate1;
        private NSView.ExecuteSaveTemplate executeSaveTemplate1;
        private NSView.EditConfig editConfig1;
        private NSView.AboutUs aboutUs1;
        private System.Windows.Forms.Button AboutUs;
        private System.Windows.Forms.Button parameters;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Execute;
        private System.Windows.Forms.Button Manage;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

