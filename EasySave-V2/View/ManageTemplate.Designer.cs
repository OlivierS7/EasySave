﻿namespace NSView
{
    partial class ManageTemplate
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.creaSaveTemplate1 = new NSView.creaSaveTemplate();
            this.delSaveTemplate1 = new NSView.DeleteSaveTemplate();
            this.button3 = new System.Windows.Forms.Button();
            this.modifySaveTemplate1 = new NSView.ModifySaveTemplate();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(36, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 48);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create a save template";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(202, 28);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 48);
            this.button2.TabIndex = 1;
            this.button2.Text = "Delete a save template";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // creaSaveTemplate1
            // 
            this.creaSaveTemplate1.Location = new System.Drawing.Point(36, 98);
            this.creaSaveTemplate1.Name = "creaSaveTemplate1";
            this.creaSaveTemplate1.Size = new System.Drawing.Size(997, 417);
            this.creaSaveTemplate1.TabIndex = 2;
            // 
            // delSaveTemplate1
            // 
            this.delSaveTemplate1.Location = new System.Drawing.Point(36, 114);
            this.delSaveTemplate1.Name = "delSaveTemplate1";
            this.delSaveTemplate1.Size = new System.Drawing.Size(965, 485);
            this.delSaveTemplate1.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(370, 28);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(143, 48);
            this.button3.TabIndex = 4;
            this.button3.Text = "Modify a save template";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // modifySaveTemplate1
            // 
            this.modifySaveTemplate1.Location = new System.Drawing.Point(55, 82);
            this.modifySaveTemplate1.Name = "modifySaveTemplate1";
            this.modifySaveTemplate1.Size = new System.Drawing.Size(1086, 638);
            this.modifySaveTemplate1.TabIndex = 5;
            // 
            // ManageTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.modifySaveTemplate1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.delSaveTemplate1);
            this.Controls.Add(this.creaSaveTemplate1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "ManageTemplate";
            this.Size = new System.Drawing.Size(1195, 740);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private creaSaveTemplate creaSaveTemplate1;
        private DeleteSaveTemplate delSaveTemplate1;
        private System.Windows.Forms.Button button3;
        private ModifySaveTemplate modifySaveTemplate1;
    }
}
