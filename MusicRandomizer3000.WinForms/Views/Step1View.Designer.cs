using MusicRandomizer3000.WinForms.Controls;
namespace MusicRandomizer3000.WinForms.Views
{
    partial class Step1View
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlByFolders = new System.Windows.Forms.Panel();
            this.numFiles = new System.Windows.Forms.NumericUpDown();
            this.numFolders = new System.Windows.Forms.NumericUpDown();
            this.picOrganizeFilesDescription = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbFilesLimitTypes = new MusicRandomizer3000.WinForms.Controls.BindableComboBox();
            this.btnSelectPathFrom = new System.Windows.Forms.Button();
            this.pnlByNumber = new System.Windows.Forms.Panel();
            this.lblLimitRange = new System.Windows.Forms.Label();
            this.numFilesNumber = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlByTotalSize = new System.Windows.Forms.Panel();
            this.lblSizeRange = new System.Windows.Forms.Label();
            this.lblLimitFilesByTotalSize = new System.Windows.Forms.Label();
            this.tbxSizeLimit = new System.Windows.Forms.TextBox();
            this.cmbUnits = new MusicRandomizer3000.WinForms.Controls.BindableComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxSaveSettings = new System.Windows.Forms.CheckBox();
            this.btnNextStep1 = new System.Windows.Forms.Button();
            this.cbxFindOnlyUniqueFiles = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.pnlByFolders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFolders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOrganizeFilesDescription)).BeginInit();
            this.pnlByNumber.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFilesNumber)).BeginInit();
            this.pnlByTotalSize.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlByFolders);
            this.panel1.Controls.Add(this.cmbFilesLimitTypes);
            this.panel1.Controls.Add(this.btnSelectPathFrom);
            this.panel1.Controls.Add(this.pnlByNumber);
            this.panel1.Controls.Add(this.pnlByTotalSize);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbxSaveSettings);
            this.panel1.Controls.Add(this.btnNextStep1);
            this.panel1.Controls.Add(this.cbxFindOnlyUniqueFiles);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 190);
            this.panel1.TabIndex = 31;
            // 
            // pnlByFolders
            // 
            this.pnlByFolders.AutoSize = true;
            this.pnlByFolders.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlByFolders.Controls.Add(this.numFiles);
            this.pnlByFolders.Controls.Add(this.numFolders);
            this.pnlByFolders.Controls.Add(this.picOrganizeFilesDescription);
            this.pnlByFolders.Controls.Add(this.label4);
            this.pnlByFolders.Controls.Add(this.label5);
            this.pnlByFolders.Controls.Add(this.label6);
            this.pnlByFolders.Location = new System.Drawing.Point(26, 31);
            this.pnlByFolders.Name = "pnlByFolders";
            this.pnlByFolders.Size = new System.Drawing.Size(340, 35);
            this.pnlByFolders.TabIndex = 56;
            // 
            // numFiles
            // 
            this.numFiles.Location = new System.Drawing.Point(186, 9);
            this.numFiles.Name = "numFiles";
            this.numFiles.Size = new System.Drawing.Size(48, 22);
            this.numFiles.TabIndex = 56;
            // 
            // numFolders
            // 
            this.numFolders.Location = new System.Drawing.Point(51, 9);
            this.numFolders.Name = "numFolders";
            this.numFolders.Size = new System.Drawing.Size(36, 22);
            this.numFolders.TabIndex = 55;
            // 
            // picOrganizeFilesDescription
            // 
            this.picOrganizeFilesDescription.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picOrganizeFilesDescription.Image = global::MusicRandomizer3000.WinForms.Properties.Resources.question;
            this.picOrganizeFilesDescription.Location = new System.Drawing.Point(313, 8);
            this.picOrganizeFilesDescription.Name = "picOrganizeFilesDescription";
            this.picOrganizeFilesDescription.Size = new System.Drawing.Size(24, 24);
            this.picOrganizeFilesDescription.TabIndex = 54;
            this.picOrganizeFilesDescription.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 49;
            this.label4.Text = "Create";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 16);
            this.label5.TabIndex = 53;
            this.label5.Text = "files in each.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(89, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 16);
            this.label6.TabIndex = 51;
            this.label6.Text = "folders and put";
            // 
            // cmbFilesLimitTypes
            // 
            this.cmbFilesLimitTypes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbFilesLimitTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilesLimitTypes.FormattingEnabled = true;
            this.cmbFilesLimitTypes.Items.AddRange(new object[] {
            "total size",
            "number"});
            this.cmbFilesLimitTypes.Location = new System.Drawing.Point(316, 61);
            this.cmbFilesLimitTypes.Name = "cmbFilesLimitTypes";
            this.cmbFilesLimitTypes.Size = new System.Drawing.Size(84, 24);
            this.cmbFilesLimitTypes.TabIndex = 44;
            // 
            // btnSelectPathFrom
            // 
            this.btnSelectPathFrom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectPathFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnSelectPathFrom.Image = global::MusicRandomizer3000.WinForms.Properties.Resources.folder;
            this.btnSelectPathFrom.Location = new System.Drawing.Point(231, 9);
            this.btnSelectPathFrom.Name = "btnSelectPathFrom";
            this.btnSelectPathFrom.Size = new System.Drawing.Size(28, 23);
            this.btnSelectPathFrom.TabIndex = 43;
            this.btnSelectPathFrom.UseVisualStyleBackColor = true;
            // 
            // pnlByNumber
            // 
            this.pnlByNumber.AutoSize = true;
            this.pnlByNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlByNumber.Controls.Add(this.lblLimitRange);
            this.pnlByNumber.Controls.Add(this.numFilesNumber);
            this.pnlByNumber.Controls.Add(this.label3);
            this.pnlByNumber.Location = new System.Drawing.Point(63, 129);
            this.pnlByNumber.Name = "pnlByNumber";
            this.pnlByNumber.Size = new System.Drawing.Size(258, 30);
            this.pnlByNumber.TabIndex = 42;
            // 
            // lblLimitRange
            // 
            this.lblLimitRange.AutoSize = true;
            this.lblLimitRange.Location = new System.Drawing.Point(212, 7);
            this.lblLimitRange.Name = "lblLimitRange";
            this.lblLimitRange.Size = new System.Drawing.Size(43, 16);
            this.lblLimitRange.TabIndex = 45;
            this.lblLimitRange.Text = "range";
            // 
            // numFilesNumber
            // 
            this.numFilesNumber.Location = new System.Drawing.Point(137, 5);
            this.numFilesNumber.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numFilesNumber.Name = "numFilesNumber";
            this.numFilesNumber.Size = new System.Drawing.Size(69, 22);
            this.numFilesNumber.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Enter number of files:";
            // 
            // pnlByTotalSize
            // 
            this.pnlByTotalSize.AutoSize = true;
            this.pnlByTotalSize.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlByTotalSize.Controls.Add(this.lblSizeRange);
            this.pnlByTotalSize.Controls.Add(this.lblLimitFilesByTotalSize);
            this.pnlByTotalSize.Controls.Add(this.tbxSizeLimit);
            this.pnlByTotalSize.Controls.Add(this.cmbUnits);
            this.pnlByTotalSize.Location = new System.Drawing.Point(42, 96);
            this.pnlByTotalSize.Name = "pnlByTotalSize";
            this.pnlByTotalSize.Size = new System.Drawing.Size(292, 30);
            this.pnlByTotalSize.TabIndex = 41;
            // 
            // lblSizeRange
            // 
            this.lblSizeRange.AutoSize = true;
            this.lblSizeRange.Location = new System.Drawing.Point(246, 7);
            this.lblSizeRange.Name = "lblSizeRange";
            this.lblSizeRange.Size = new System.Drawing.Size(43, 16);
            this.lblSizeRange.TabIndex = 40;
            this.lblSizeRange.Text = "range";
            // 
            // lblLimitFilesByTotalSize
            // 
            this.lblLimitFilesByTotalSize.AutoSize = true;
            this.lblLimitFilesByTotalSize.Location = new System.Drawing.Point(3, 7);
            this.lblLimitFilesByTotalSize.Name = "lblLimitFilesByTotalSize";
            this.lblLimitFilesByTotalSize.Size = new System.Drawing.Size(97, 16);
            this.lblLimitFilesByTotalSize.TabIndex = 39;
            this.lblLimitFilesByTotalSize.Text = "Enter total size:";
            // 
            // tbxSizeLimit
            // 
            this.tbxSizeLimit.CausesValidation = false;
            this.tbxSizeLimit.Location = new System.Drawing.Point(100, 4);
            this.tbxSizeLimit.MaxLength = 4;
            this.tbxSizeLimit.Name = "tbxSizeLimit";
            this.tbxSizeLimit.Size = new System.Drawing.Size(41, 22);
            this.tbxSizeLimit.TabIndex = 37;
            // 
            // cmbUnits
            // 
            this.cmbUnits.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnits.Location = new System.Drawing.Point(147, 3);
            this.cmbUnits.Name = "cmbUnits";
            this.cmbUnits.Size = new System.Drawing.Size(93, 24);
            this.cmbUnits.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 16);
            this.label2.TabIndex = 40;
            this.label2.Text = "Limit files by:";
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
            this.cbxSaveSettings.TabIndex = 37;
            this.cbxSaveSettings.Text = "Save these settings";
            this.cbxSaveSettings.UseVisualStyleBackColor = true;
            // 
            // btnNextStep1
            // 
            this.btnNextStep1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextStep1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnNextStep1.Location = new System.Drawing.Point(298, 159);
            this.btnNextStep1.Name = "btnNextStep1";
            this.btnNextStep1.Size = new System.Drawing.Size(102, 28);
            this.btnNextStep1.TabIndex = 4;
            this.btnNextStep1.Text = "Next Step";
            this.btnNextStep1.UseVisualStyleBackColor = true;
            // 
            // cbxFindOnlyUniqueFiles
            // 
            this.cbxFindOnlyUniqueFiles.AutoSize = true;
            this.cbxFindOnlyUniqueFiles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbxFindOnlyUniqueFiles.Location = new System.Drawing.Point(5, 61);
            this.cbxFindOnlyUniqueFiles.Name = "cbxFindOnlyUniqueFiles";
            this.cbxFindOnlyUniqueFiles.Size = new System.Drawing.Size(199, 20);
            this.cbxFindOnlyUniqueFiles.TabIndex = 28;
            this.cbxFindOnlyUniqueFiles.Text = "Random files must be unique";
            this.cbxFindOnlyUniqueFiles.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Pick a folder to get random files from:";
            // 
            // Step1View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "Step1View";
            this.Size = new System.Drawing.Size(405, 190);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlByFolders.ResumeLayout(false);
            this.pnlByFolders.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFolders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOrganizeFilesDescription)).EndInit();
            this.pnlByNumber.ResumeLayout(false);
            this.pnlByNumber.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFilesNumber)).EndInit();
            this.pnlByTotalSize.ResumeLayout(false);
            this.pnlByTotalSize.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNextStep1;
        private System.Windows.Forms.CheckBox cbxFindOnlyUniqueFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.CheckBox cbxSaveSettings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlByTotalSize;
        private System.Windows.Forms.Label lblLimitFilesByTotalSize;
        private BindableComboBox cmbUnits;
        private System.Windows.Forms.TextBox tbxSizeLimit;
        private System.Windows.Forms.Label lblSizeRange;
        private System.Windows.Forms.Panel pnlByNumber;
        private System.Windows.Forms.Button btnSelectPathFrom;
        private System.Windows.Forms.Label label3;
        private BindableComboBox cmbFilesLimitTypes;
        private System.Windows.Forms.NumericUpDown numFilesNumber;
        private System.Windows.Forms.Label lblLimitRange;
        private System.Windows.Forms.Panel pnlByFolders;
        private System.Windows.Forms.NumericUpDown numFolders;
        private System.Windows.Forms.PictureBox picOrganizeFilesDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numFiles;
    }
}
