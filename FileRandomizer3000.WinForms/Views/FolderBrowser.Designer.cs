namespace FileRandomizer3000.WinForms.Views
{
    partial class FolderBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderBrowser));
            this.treeViewFolderBrowser = new Raccoom.Windows.Forms.TreeViewFolderBrowser();
            this.SuspendLayout();
            // 
            // treeViewFolderBrowser
            // 
            this.treeViewFolderBrowser.BackColor = System.Drawing.SystemColors.Control;
            this.treeViewFolderBrowser.DataSource = null;
            this.treeViewFolderBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFolderBrowser.HideSelection = false;
            this.treeViewFolderBrowser.Location = new System.Drawing.Point(0, 0);
            this.treeViewFolderBrowser.Name = "treeViewFolderBrowser2";
            this.treeViewFolderBrowser.ShowLines = false;
            this.treeViewFolderBrowser.ShowRootLines = false;
            this.treeViewFolderBrowser.Size = new System.Drawing.Size(284, 261);
            this.treeViewFolderBrowser.TabIndex = 1;
            // 
            // FolderBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.treeViewFolderBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FolderBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion
        private Raccoom.Windows.Forms.TreeViewFolderBrowser treeViewFolderBrowser;
    }
}