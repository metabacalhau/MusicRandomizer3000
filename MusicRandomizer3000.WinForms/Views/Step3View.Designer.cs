namespace MusicRandomizer3000.WinForms.Views
{
    partial class Step3View
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxShowRandomizedFiles = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnPreviousStep3 = new System.Windows.Forms.Button();
            this.cbxSaveSettings = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picOnDuplicateOptionDescription = new System.Windows.Forms.PictureBox();
            this.lblInCaseOfDuplicateFiles = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbSortOptions = new MusicRandomizer3000.WinForms.Controls.BindableComboBox();
            this.cmbOnDuplicateActions = new MusicRandomizer3000.WinForms.Controls.BindableComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOnDuplicateOptionDescription)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.cbxShowRandomizedFiles);
            this.panel1.Controls.Add(this.btnProcess);
            this.panel1.Controls.Add(this.btnPreviousStep3);
            this.panel1.Controls.Add(this.cbxSaveSettings);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 190);
            this.panel1.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 44;
            this.label2.Text = "order.";
            // 
            // cbxShowRandomizedFiles
            // 
            this.cbxShowRandomizedFiles.AutoSize = true;
            this.cbxShowRandomizedFiles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbxShowRandomizedFiles.Location = new System.Drawing.Point(39, 120);
            this.cbxShowRandomizedFiles.Name = "cbxShowRandomizedFiles";
            this.cbxShowRandomizedFiles.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbxShowRandomizedFiles.Size = new System.Drawing.Size(236, 20);
            this.cbxShowRandomizedFiles.TabIndex = 43;
            this.cbxShowRandomizedFiles.Text = "Show randomized files before copy";
            this.cbxShowRandomizedFiles.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 16);
            this.label1.TabIndex = 40;
            this.label1.Text = "Sort files by file name in";
            // 
            // btnProcess
            // 
            this.btnProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnProcess.Location = new System.Drawing.Point(253, 159);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(146, 28);
            this.btnProcess.TabIndex = 32;
            this.btnProcess.Text = "Randomize";
            this.btnProcess.UseVisualStyleBackColor = true;
            // 
            // btnPreviousStep3
            // 
            this.btnPreviousStep3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreviousStep3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnPreviousStep3.Location = new System.Drawing.Point(147, 159);
            this.btnPreviousStep3.Name = "btnPreviousStep3";
            this.btnPreviousStep3.Size = new System.Drawing.Size(102, 28);
            this.btnPreviousStep3.TabIndex = 31;
            this.btnPreviousStep3.Text = "Previous Step";
            this.btnPreviousStep3.UseVisualStyleBackColor = true;
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
            this.cbxSaveSettings.TabIndex = 36;
            this.cbxSaveSettings.Text = "Save these settings";
            this.cbxSaveSettings.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.picOnDuplicateOptionDescription);
            this.panel2.Controls.Add(this.cmbOnDuplicateActions);
            this.panel2.Controls.Add(this.lblInCaseOfDuplicateFiles);
            this.panel2.Location = new System.Drawing.Point(39, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(331, 34);
            this.panel2.TabIndex = 45;
            // 
            // picOnDuplicateOptionDescription
            // 
            this.picOnDuplicateOptionDescription.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picOnDuplicateOptionDescription.Image = global::MusicRandomizer3000.WinForms.Properties.Resources.question;
            this.picOnDuplicateOptionDescription.Location = new System.Drawing.Point(301, 5);
            this.picOnDuplicateOptionDescription.Name = "picOnDuplicateOptionDescription";
            this.picOnDuplicateOptionDescription.Size = new System.Drawing.Size(24, 24);
            this.picOnDuplicateOptionDescription.TabIndex = 42;
            this.picOnDuplicateOptionDescription.TabStop = false;
            // 
            // lblInCaseOfDuplicateFiles
            // 
            this.lblInCaseOfDuplicateFiles.AutoSize = true;
            this.lblInCaseOfDuplicateFiles.Location = new System.Drawing.Point(3, 8);
            this.lblInCaseOfDuplicateFiles.Name = "lblInCaseOfDuplicateFiles";
            this.lblInCaseOfDuplicateFiles.Size = new System.Drawing.Size(150, 16);
            this.lblInCaseOfDuplicateFiles.TabIndex = 40;
            this.lblInCaseOfDuplicateFiles.Text = "In case of duplicate files";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.cmbSortOptions);
            this.panel3.Location = new System.Drawing.Point(39, 88);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(331, 37);
            this.panel3.TabIndex = 46;
            // 
            // cmbSortOptions
            // 
            this.cmbSortOptions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbSortOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortOptions.FormattingEnabled = true;
            this.cmbSortOptions.Items.AddRange(new object[] {
            "ascending",
            "descending"});
            this.cmbSortOptions.Location = new System.Drawing.Point(156, 5);
            this.cmbSortOptions.Name = "cmbSortOptions";
            this.cmbSortOptions.Size = new System.Drawing.Size(100, 24);
            this.cmbSortOptions.TabIndex = 41;
            // 
            // cmbOnDuplicateActions
            // 
            this.cmbOnDuplicateActions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbOnDuplicateActions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOnDuplicateActions.FormattingEnabled = true;
            this.cmbOnDuplicateActions.Items.AddRange(new object[] {
            "skip",
            "overwrite",
            "add random prefix",
            "add random postfix"});
            this.cmbOnDuplicateActions.Location = new System.Drawing.Point(156, 5);
            this.cmbOnDuplicateActions.Name = "cmbOnDuplicateActions";
            this.cmbOnDuplicateActions.Size = new System.Drawing.Size(145, 24);
            this.cmbOnDuplicateActions.TabIndex = 41;
            // 
            // Step3View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "Step3View";
            this.Size = new System.Drawing.Size(405, 190);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOnDuplicateOptionDescription)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnPreviousStep3;
        private System.Windows.Forms.CheckBox cbxSaveSettings;
        private System.Windows.Forms.Label label1;
        private Controls.BindableComboBox cmbSortOptions;
        private System.Windows.Forms.CheckBox cbxShowRandomizedFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picOnDuplicateOptionDescription;
        private Controls.BindableComboBox cmbOnDuplicateActions;
        private System.Windows.Forms.Label lblInCaseOfDuplicateFiles;
    }
}
