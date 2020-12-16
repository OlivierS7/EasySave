
namespace RemoteClient.NSView
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
            this.Connection = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.executeMenu1 = new RemoteClient.NSView.ExecuteMenu();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Connection
            // 
            this.Connection.Font = new System.Drawing.Font("Impact", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Connection.Location = new System.Drawing.Point(338, 379);
            this.Connection.Name = "Connection";
            this.Connection.Size = new System.Drawing.Size(117, 50);
            this.Connection.TabIndex = 0;
            this.Connection.Text = "Connection";
            this.Connection.UseVisualStyleBackColor = true;
            this.Connection.Click += new System.EventHandler(this.Connection_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Impact", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(265, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please enter ip address of the remote server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Impact", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(283, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Please enter port of the remote server";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Impact", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(300, 150);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(204, 25);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Impact", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox2.Location = new System.Drawing.Point(300, 286);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(204, 25);
            this.textBox2.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Impact", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(-2, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(183, 32);
            this.button2.TabIndex = 7;
            this.button2.Text = "Execute template(s)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // executeMenu1
            // 
            this.executeMenu1.Location = new System.Drawing.Point(-2, 38);
            this.executeMenu1.Name = "executeMenu1";
            this.executeMenu1.Size = new System.Drawing.Size(841, 588);
            this.executeMenu1.TabIndex = 8;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Impact", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button3.Location = new System.Drawing.Point(327, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(173, 32);
            this.button3.TabIndex = 9;
            this.button3.Text = "Connection";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Impact", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button4.Location = new System.Drawing.Point(642, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(178, 32);
            this.button4.TabIndex = 10;
            this.button4.Text = "Exit Application";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // StartMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 601);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.executeMenu1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Connection);
            this.Name = "StartMenu";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.StartMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Connection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private NSView.ExecuteMenu executeMenu1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

