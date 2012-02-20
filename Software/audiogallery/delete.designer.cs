namespace audiogallery
{
    partial class delete
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(delete));
            this.filestodeletelistbox = new System.Windows.Forms.ListBox();
            this.deletestatuslbl = new System.Windows.Forms.Label();
            this.deletefilesbtn = new System.Windows.Forms.Button();
            this.albumnamelbl = new System.Windows.Forms.Label();
            this.deleteaudiosbackgroundworker = new System.ComponentModel.BackgroundWorker();
            this.deleteprogressbar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // filestodeletelistbox
            // 
            this.filestodeletelistbox.FormattingEnabled = true;
            this.filestodeletelistbox.Location = new System.Drawing.Point(0, 27);
            this.filestodeletelistbox.Name = "filestodeletelistbox";
            this.filestodeletelistbox.Size = new System.Drawing.Size(294, 147);
            this.filestodeletelistbox.TabIndex = 0;
            // 
            // deletestatuslbl
            // 
            this.deletestatuslbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deletestatuslbl.ForeColor = System.Drawing.Color.White;
            this.deletestatuslbl.Location = new System.Drawing.Point(0, 197);
            this.deletestatuslbl.Name = "deletestatuslbl";
            this.deletestatuslbl.Size = new System.Drawing.Size(294, 18);
            this.deletestatuslbl.TabIndex = 2;
            this.deletestatuslbl.Text = "Ready";
            this.deletestatuslbl.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // deletefilesbtn
            // 
            this.deletefilesbtn.BackColor = System.Drawing.Color.Gainsboro;
            this.deletefilesbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.deletefilesbtn.Location = new System.Drawing.Point(0, 174);
            this.deletefilesbtn.Name = "deletefilesbtn";
            this.deletefilesbtn.Size = new System.Drawing.Size(294, 23);
            this.deletefilesbtn.TabIndex = 11;
            this.deletefilesbtn.Text = "DELETE AUDIOS";
            this.deletefilesbtn.UseVisualStyleBackColor = false;
            this.deletefilesbtn.Click += new System.EventHandler(this.deletefilesbtn_Click);
            // 
            // albumnamelbl
            // 
            this.albumnamelbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.albumnamelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumnamelbl.ForeColor = System.Drawing.Color.White;
            this.albumnamelbl.Location = new System.Drawing.Point(0, 0);
            this.albumnamelbl.Name = "albumnamelbl";
            this.albumnamelbl.Size = new System.Drawing.Size(294, 21);
            this.albumnamelbl.TabIndex = 12;
            this.albumnamelbl.Text = "label1";
            this.albumnamelbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // deleteaudiosbackgroundworker
            // 
            this.deleteaudiosbackgroundworker.WorkerReportsProgress = true;
            this.deleteaudiosbackgroundworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.deleteaudiosbackgroundworker_DoWork);
            this.deleteaudiosbackgroundworker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.deleteaudiosbackgroundworker_ProgressChanged);
            this.deleteaudiosbackgroundworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.deleteaudiosbackgroundworker_RunWorkerCompleted);
            // 
            // deleteprogressbar
            // 
            this.deleteprogressbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.deleteprogressbar.Location = new System.Drawing.Point(0, 216);
            this.deleteprogressbar.Name = "deleteprogressbar";
            this.deleteprogressbar.Size = new System.Drawing.Size(294, 23);
            this.deleteprogressbar.TabIndex = 13;
            // 
            // delete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(294, 239);
            this.Controls.Add(this.deleteprogressbar);
            this.Controls.Add(this.albumnamelbl);
            this.Controls.Add(this.deletefilesbtn);
            this.Controls.Add(this.deletestatuslbl);
            this.Controls.Add(this.filestodeletelistbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "delete";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delete audios";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.delete_FormClosing);
            this.Load += new System.EventHandler(this.delete_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox filestodeletelistbox;
        private System.Windows.Forms.Label deletestatuslbl;
        private System.Windows.Forms.Button deletefilesbtn;
        private System.Windows.Forms.Label albumnamelbl;
        private System.ComponentModel.BackgroundWorker deleteaudiosbackgroundworker;
        private System.Windows.Forms.ProgressBar deleteprogressbar;
    }
}