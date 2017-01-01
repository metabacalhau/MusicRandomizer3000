namespace MusicRandomizer3000.WinForms.Views
{
    partial class RandomizationProcessView
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
            this.btnStopRandomizationProcess = new System.Windows.Forms.Button();
            this.btnGoToPreviousStep = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.imgLoader = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLoader)).BeginInit();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.Controls.Add(this.btnStopRandomizationProcess);
            this.panel1.Controls.Add(this.btnGoToPreviousStep);
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Controls.Add(this.imgLoader);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 190);
            this.panel1.TabIndex = 39;
            //
            // btnStopRandomizationProcess
            //
            this.btnStopRandomizationProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopRandomizationProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnStopRandomizationProcess.Location = new System.Drawing.Point(194, 160);
            this.btnStopRandomizationProcess.Name = "btnStopRandomizationProcess";
            this.btnStopRandomizationProcess.Size = new System.Drawing.Size(102, 28);
            this.btnStopRandomizationProcess.TabIndex = 33;
            this.btnStopRandomizationProcess.Text = "Terminate";
            this.btnStopRandomizationProcess.UseVisualStyleBackColor = true;
            //
            // btnGoToPreviousStep
            //
            this.btnGoToPreviousStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnGoToPreviousStep.Location = new System.Drawing.Point(300, 160);
            this.btnGoToPreviousStep.Name = "btnGoToPreviousStep";
            this.btnGoToPreviousStep.Size = new System.Drawing.Size(102, 28);
            this.btnGoToPreviousStep.TabIndex = 37;
            this.btnGoToPreviousStep.Text = "Previous step";
            this.btnGoToPreviousStep.UseVisualStyleBackColor = true;
            //
            // lblInfo
            //
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(173, 79);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(43, 16);
            this.lblInfo.TabIndex = 35;
            this.lblInfo.Text = "lblInfo";
            //
            // imgLoader
            //
            this.imgLoader.Image = global::MusicRandomizer3000.WinForms.Properties.Resources.loading;
            this.imgLoader.Location = new System.Drawing.Point(182, 70);
            this.imgLoader.Name = "imgLoader";
            this.imgLoader.Size = new System.Drawing.Size(36, 33);
            this.imgLoader.TabIndex = 0;
            this.imgLoader.TabStop = false;
            //
            // RandomizationProcessView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "RandomizationProcessView";
            this.Size = new System.Drawing.Size(405, 190);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLoader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStopRandomizationProcess;
        private System.Windows.Forms.Button btnGoToPreviousStep;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.PictureBox imgLoader;
    }
}
