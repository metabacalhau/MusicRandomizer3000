namespace MusicRandomizer3000.WinForms.Views
{
    partial class Step2View
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.btnSelectPathTo = new System.Windows.Forms.Button();
            this.tbxPathTo = new System.Windows.Forms.TextBox();
            this.lblFolderToCopyFilesTo = new System.Windows.Forms.Label();
            this.lblUponCopy = new System.Windows.Forms.Label();
            this.btnNextStep = new System.Windows.Forms.Button();
            this.cbxSaveSettings = new System.Windows.Forms.CheckBox();
            this.btnPreviousStep = new System.Windows.Forms.Button();
            this.picOnCopyActionsDescription = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbActionsOnCopy = new MusicRandomizer3000.WinForms.Controls.BindableComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picOnCopyActionsDescription)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectPathTo
            // 
            this.btnSelectPathTo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectPathTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnSelectPathTo.Image = global::MusicRandomizer3000.WinForms.Properties.Resources.folder;
            this.btnSelectPathTo.Location = new System.Drawing.Point(372, 32);
            this.btnSelectPathTo.Name = "btnSelectPathTo";
            this.btnSelectPathTo.Size = new System.Drawing.Size(28, 23);
            this.btnSelectPathTo.TabIndex = 9;
            this.btnSelectPathTo.UseVisualStyleBackColor = true;
            // 
            // tbxPathTo
            // 
            this.tbxPathTo.Location = new System.Drawing.Point(4, 32);
            this.tbxPathTo.Name = "tbxPathTo";
            this.tbxPathTo.Size = new System.Drawing.Size(362, 22);
            this.tbxPathTo.TabIndex = 10;
            // 
            // lblFolderToCopyFilesTo
            // 
            this.lblFolderToCopyFilesTo.AutoSize = true;
            this.lblFolderToCopyFilesTo.Location = new System.Drawing.Point(1, 13);
            this.lblFolderToCopyFilesTo.Name = "lblFolderToCopyFilesTo";
            this.lblFolderToCopyFilesTo.Size = new System.Drawing.Size(138, 16);
            this.lblFolderToCopyFilesTo.TabIndex = 11;
            this.lblFolderToCopyFilesTo.Text = "Folder to copy files to:";
            // 
            // lblUponCopy
            // 
            this.lblUponCopy.AutoSize = true;
            this.lblUponCopy.Location = new System.Drawing.Point(86, 89);
            this.lblUponCopy.Name = "lblUponCopy";
            this.lblUponCopy.Size = new System.Drawing.Size(85, 16);
            this.lblUponCopy.TabIndex = 42;
            this.lblUponCopy.Text = "On copy add";
            // 
            // btnNextStep
            // 
            this.btnNextStep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnNextStep.Location = new System.Drawing.Point(298, 159);
            this.btnNextStep.Name = "btnNextStep";
            this.btnNextStep.Size = new System.Drawing.Size(102, 28);
            this.btnNextStep.TabIndex = 12;
            this.btnNextStep.Text = "Next Step";
            this.btnNextStep.UseVisualStyleBackColor = true;
            // 
            // cbxSaveSettings
            // 
            this.cbxSaveSettings.AutoSize = true;
            this.cbxSaveSettings.Checked = true;
            this.cbxSaveSettings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxSaveSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbxSaveSettings.Location = new System.Drawing.Point(5, 164);
            this.cbxSaveSettings.Name = "cbxSaveSettings";
            this.cbxSaveSettings.Size = new System.Drawing.Size(144, 20);
            this.cbxSaveSettings.TabIndex = 45;
            this.cbxSaveSettings.Text = "Save these settings";
            this.cbxSaveSettings.UseVisualStyleBackColor = true;
            // 
            // btnPreviousStep
            // 
            this.btnPreviousStep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreviousStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnPreviousStep.Location = new System.Drawing.Point(192, 159);
            this.btnPreviousStep.Name = "btnPreviousStep";
            this.btnPreviousStep.Size = new System.Drawing.Size(102, 28);
            this.btnPreviousStep.TabIndex = 13;
            this.btnPreviousStep.Text = "Previous Step";
            this.btnPreviousStep.UseVisualStyleBackColor = true;
            // 
            // picOnCopyActionsDescription
            // 
            this.picOnCopyActionsDescription.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picOnCopyActionsDescription.Image = global::MusicRandomizer3000.WinForms.Properties.Resources.question;
            this.picOnCopyActionsDescription.Location = new System.Drawing.Point(294, 86);
            this.picOnCopyActionsDescription.Name = "picOnCopyActionsDescription";
            this.picOnCopyActionsDescription.Size = new System.Drawing.Size(24, 24);
            this.picOnCopyActionsDescription.TabIndex = 48;
            this.picOnCopyActionsDescription.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.picOnCopyActionsDescription);
            this.panel1.Controls.Add(this.btnPreviousStep);
            this.panel1.Controls.Add(this.cbxSaveSettings);
            this.panel1.Controls.Add(this.btnNextStep);
            this.panel1.Controls.Add(this.cmbActionsOnCopy);
            this.panel1.Controls.Add(this.lblUponCopy);
            this.panel1.Controls.Add(this.lblFolderToCopyFilesTo);
            this.panel1.Controls.Add(this.tbxPathTo);
            this.panel1.Controls.Add(this.btnSelectPathTo);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 190);
            this.panel1.TabIndex = 46;
            // 
            // cmbActionsOnCopy
            // 
            this.cmbActionsOnCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbActionsOnCopy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionsOnCopy.FormattingEnabled = true;
            this.cmbActionsOnCopy.Items.AddRange(new object[] {
            "nothing",
            "random prefix",
            "random postfix"});
            this.cmbActionsOnCopy.Location = new System.Drawing.Point(177, 86);
            this.cmbActionsOnCopy.Name = "cmbActionsOnCopy";
            this.cmbActionsOnCopy.Size = new System.Drawing.Size(117, 24);
            this.cmbActionsOnCopy.TabIndex = 44;
            // 
            // Step2View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "Step2View";
            this.Size = new System.Drawing.Size(405, 190);
            ((System.ComponentModel.ISupportInitialize)(this.picOnCopyActionsDescription)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Button btnSelectPathTo;
        private System.Windows.Forms.TextBox tbxPathTo;
        private System.Windows.Forms.Label lblFolderToCopyFilesTo;
        private System.Windows.Forms.Label lblUponCopy;
        private Controls.BindableComboBox cmbActionsOnCopy;
        private System.Windows.Forms.Button btnNextStep;
        private System.Windows.Forms.CheckBox cbxSaveSettings;
        private System.Windows.Forms.Button btnPreviousStep;
        private System.Windows.Forms.PictureBox picOnCopyActionsDescription;
        private System.Windows.Forms.Panel panel1;
    }
}
