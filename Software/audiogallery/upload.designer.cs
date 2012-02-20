namespace audiogallery
{
    partial class upload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(upload));
            this.uploadfileopendialog = new System.Windows.Forms.OpenFileDialog();
            this.progressbar = new System.Windows.Forms.ProgressBar();
            this.uploadfilebtn = new System.Windows.Forms.Button();
            this.choosefilebtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.descriptiontxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.titletxt = new System.Windows.Forms.TextBox();
            this.filenametxt = new System.Windows.Forms.TextBox();
            this.fileuploadbackgroundworker = new System.ComponentModel.BackgroundWorker();
            this.numofbytesuploadedstatuslbl = new System.Windows.Forms.Label();
            this.playlistnamelbl = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uploadfileopendialog
            // 
            this.uploadfileopendialog.Filter = "audios|*.flv;*.mp4;*.mov;*.3gp;*.3g2;*.f4v";
            // 
            // progressbar
            // 
            this.progressbar.Location = new System.Drawing.Point(2, 224);
            this.progressbar.MarqueeAnimationSpeed = 1;
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(374, 26);
            this.progressbar.TabIndex = 0;
            // 
            // uploadfilebtn
            // 
            this.uploadfilebtn.BackColor = System.Drawing.Color.Gainsboro;
            this.uploadfilebtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.uploadfilebtn.Location = new System.Drawing.Point(0, 203);
            this.uploadfilebtn.Name = "uploadfilebtn";
            this.uploadfilebtn.Size = new System.Drawing.Size(376, 23);
            this.uploadfilebtn.TabIndex = 10;
            this.uploadfilebtn.Text = "UPLOAD AUDIO";
            this.uploadfilebtn.UseVisualStyleBackColor = false;
            this.uploadfilebtn.Click += new System.EventHandler(this.uploadfilesbtn_Click);
            // 
            // choosefilebtn
            // 
            this.choosefilebtn.BackColor = System.Drawing.Color.Gainsboro;
            this.choosefilebtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.choosefilebtn.Location = new System.Drawing.Point(277, 19);
            this.choosefilebtn.Name = "choosefilebtn";
            this.choosefilebtn.Size = new System.Drawing.Size(89, 23);
            this.choosefilebtn.TabIndex = 11;
            this.choosefilebtn.Text = "BROWSE";
            this.choosefilebtn.UseVisualStyleBackColor = false;
            this.choosefilebtn.Click += new System.EventHandler(this.choosefilebtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.descriptiontxt);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.titletxt);
            this.groupBox1.Controls.Add(this.filenametxt);
            this.groupBox1.Controls.Add(this.choosefilebtn);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 177);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Description";
            // 
            // descriptiontxt
            // 
            this.descriptiontxt.Location = new System.Drawing.Point(76, 90);
            this.descriptiontxt.Multiline = true;
            this.descriptiontxt.Name = "descriptiontxt";
            this.descriptiontxt.Size = new System.Drawing.Size(290, 69);
            this.descriptiontxt.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Title";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "File";
            // 
            // titletxt
            // 
            this.titletxt.Location = new System.Drawing.Point(76, 55);
            this.titletxt.Name = "titletxt";
            this.titletxt.Size = new System.Drawing.Size(290, 20);
            this.titletxt.TabIndex = 13;
            // 
            // filenametxt
            // 
            this.filenametxt.Enabled = false;
            this.filenametxt.Location = new System.Drawing.Point(76, 20);
            this.filenametxt.Name = "filenametxt";
            this.filenametxt.Size = new System.Drawing.Size(195, 20);
            this.filenametxt.TabIndex = 12;
            // 
            // fileuploadbackgroundworker
            // 
            this.fileuploadbackgroundworker.WorkerReportsProgress = true;
            this.fileuploadbackgroundworker.WorkerSupportsCancellation = true;
            this.fileuploadbackgroundworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.fileuploadbackgroundworker_DoWork);
            this.fileuploadbackgroundworker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.fileuploadbackgroundworker_ProgressChanged);
            this.fileuploadbackgroundworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.fileuploadbackgroundworker_RunWorkerCompleted);
            // 
            // numofbytesuploadedstatuslbl
            // 
            this.numofbytesuploadedstatuslbl.BackColor = System.Drawing.Color.Transparent;
            this.numofbytesuploadedstatuslbl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.numofbytesuploadedstatuslbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numofbytesuploadedstatuslbl.ForeColor = System.Drawing.Color.White;
            this.numofbytesuploadedstatuslbl.Location = new System.Drawing.Point(0, 250);
            this.numofbytesuploadedstatuslbl.Name = "numofbytesuploadedstatuslbl";
            this.numofbytesuploadedstatuslbl.Size = new System.Drawing.Size(378, 20);
            this.numofbytesuploadedstatuslbl.TabIndex = 14;
            this.numofbytesuploadedstatuslbl.Text = "Ready";
            this.numofbytesuploadedstatuslbl.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // playlistnamelbl
            // 
            this.playlistnamelbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.playlistnamelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playlistnamelbl.ForeColor = System.Drawing.Color.White;
            this.playlistnamelbl.Location = new System.Drawing.Point(0, 0);
            this.playlistnamelbl.Name = "playlistnamelbl";
            this.playlistnamelbl.Size = new System.Drawing.Size(378, 23);
            this.playlistnamelbl.TabIndex = 16;
            this.playlistnamelbl.Text = "label1";
            this.playlistnamelbl.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // upload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(378, 270);
            this.Controls.Add(this.uploadfilebtn);
            this.Controls.Add(this.playlistnamelbl);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressbar);
            this.Controls.Add(this.numofbytesuploadedstatuslbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "upload";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload Audios";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.upload_FormClosing);
            this.Load += new System.EventHandler(this.upload_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog uploadfileopendialog;
        private System.Windows.Forms.ProgressBar progressbar;
        private System.Windows.Forms.Button uploadfilebtn;
        private System.Windows.Forms.Button choosefilebtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker fileuploadbackgroundworker;
        private System.Windows.Forms.Label numofbytesuploadedstatuslbl;
        private System.Windows.Forms.Label playlistnamelbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox descriptiontxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox titletxt;
        private System.Windows.Forms.TextBox filenametxt;
    }
}