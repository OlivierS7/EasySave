
namespace RemoteClient.View
{
    partial class ExecuteMenu
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.id = new System.Windows.Forms.ColumnHeader();
            this.BackupName = new System.Windows.Forms.ColumnHeader();
            this.srcDir = new System.Windows.Forms.ColumnHeader();
            this.destDir = new System.Windows.Forms.ColumnHeader();
            this.Type = new System.Windows.Forms.ColumnHeader();
            this.Status = new System.Windows.Forms.ColumnHeader();
            this.Progression = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.BackupName,
            this.srcDir,
            this.destDir,
            this.Type,
            this.Status,
            this.Progression});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(23, 65);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(742, 258);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // id
            // 
            this.id.Text = "Id";
            this.id.Width = 30;
            // 
            // BackupName
            // 
            this.BackupName.Text = "Name";
            // 
            // srcDir
            // 
            this.srcDir.Text = "Source Directory";
            this.srcDir.Width = 250;
            // 
            // destDir
            // 
            this.destDir.Text = "Destination Directory";
            this.destDir.Width = 250;
            // 
            // Type
            // 
            this.Type.Text = "Backup Type";
            // 
            // Status
            // 
            this.Status.Text = "Status";
            // 
            // Progression
            // 
            this.Progression.Text = "Progression";
            this.Progression.Width = 40;
            // 
            // ExecuteMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Name = "ExecuteMenu";
            this.Size = new System.Drawing.Size(793, 588);
            this.Load += new System.EventHandler(this.ExecuteMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader BackupName;
        private System.Windows.Forms.ColumnHeader srcDir;
        private System.Windows.Forms.ColumnHeader destDir;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader Progression;
    }
}
