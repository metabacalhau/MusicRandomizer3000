namespace FileRandomizer3000.WinForms.Views
{
    partial class CopyProcessView
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
            this.btnShowCopiedFiles = new System.Windows.Forms.Button();
            this.btnStopCopyProcess = new System.Windows.Forms.Button();
            this.btnGoToPreviousStep = new System.Windows.Forms.Button();
            this.lblCurrentFile = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnShowCopiedFiles);
            this.panel1.Controls.Add(this.btnStopCopyProcess);
            this.panel1.Controls.Add(this.btnGoToPreviousStep);
            this.panel1.Controls.Add(this.lblCurrentFile);
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 190);
            this.panel1.TabIndex = 38;
            // 
            // btnShowCopiedFiles
            // 
            this.btnShowCopiedFiles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowCopiedFiles.Location = new System.Drawing.Point(271, 159);
            this.btnShowCopiedFiles.Name = "btnShowCopiedFiles";
            this.btnShowCopiedFiles.Size = new System.Drawing.Size(131, 28);
            this.btnShowCopiedFiles.TabIndex = 37;
            this.btnShowCopiedFiles.Text = "Show copied files";
            this.btnShowCopiedFiles.UseVisualStyleBackColor = true;
            // 
            // btnStopCopyProcess
            // 
            this.btnStopCopyProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopCopyProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnStopCopyProcess.Location = new System.Drawing.Point(187, 160);
            this.btnStopCopyProcess.Name = "btnStopCopyProcess";
            this.btnStopCopyProcess.Size = new System.Drawing.Size(102, 28);
            this.btnStopCopyProcess.TabIndex = 34;
            this.btnStopCopyProcess.Text = "Terminate";
            this.btnStopCopyProcess.UseVisualStyleBackColor = true;
            // 
            // btnGoToPreviousStep
            // 
            this.btnGoToPreviousStep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoToPreviousStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnGoToPreviousStep.Location = new System.Drawing.Point(163, 159);
            this.btnGoToPreviousStep.Name = "btnGoToPreviousStep";
            this.btnGoToPreviousStep.Size = new System.Drawing.Size(102, 28);
            this.btnGoToPreviousStep.TabIndex = 36;
            this.btnGoToPreviousStep.Text = "Previous step";
            this.btnGoToPreviousStep.UseVisualStyleBackColor = true;
            // 
            // lblCurrentFile
            // 
            this.lblCurrentFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurrentFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrentFile.Location = new System.Drawing.Point(3, 45);
            this.lblCurrentFile.Name = "lblCurrentFile";
            this.lblCurrentFile.Size = new System.Drawing.Size(399, 21);
            this.lblCurrentFile.TabIndex = 16;
            this.lblCurrentFile.Text = "lblCurrentFile";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(3, 78);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(399, 29);
            this.progressBar.TabIndex = 15;
            // 
            // CopyProcessView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "CopyProcessView";
            this.Size = new System.Drawing.Size(405, 190);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStopCopyProcess;
        private System.Windows.Forms.Button btnGoToPreviousStep;
        private System.Windows.Forms.Label lblCurrentFile;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnShowCopiedFiles;
    }
}
