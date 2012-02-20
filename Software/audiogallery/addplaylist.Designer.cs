namespace audiogallery
{
    partial class addplaylist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(addplaylist));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.thumbnametxt = new System.Windows.Forms.TextBox();
            this.choosethumbbtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.descriptiontxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nametxt = new System.Windows.Forms.TextBox();
            this.addplaylistbtn = new System.Windows.Forms.Button();
            this.thumbfileopendialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.thumbnametxt);
            this.groupBox1.Controls.Add(this.choosethumbbtn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.descriptiontxt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nametxt);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 170);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Thumbnail";
            // 
            // thumbnametxt
            // 
            this.thumbnametxt.Enabled = false;
            this.thumbnametxt.Location = new System.Drawing.Point(76, 55);
            this.thumbnametxt.Name = "thumbnametxt";
            this.thumbnametxt.Size = new System.Drawing.Size(195, 20);
            this.thumbnametxt.TabIndex = 19;
            // 
            // choosethumbbtn
            // 
            this.choosethumbbtn.BackColor = System.Drawing.Color.Gainsboro;
            this.choosethumbbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.choosethumbbtn.Location = new System.Drawing.Point(277, 54);
            this.choosethumbbtn.Name = "choosethumbbtn";
            this.choosethumbbtn.Size = new System.Drawing.Size(89, 23);
            this.choosethumbbtn.TabIndex = 18;
            this.choosethumbbtn.Text = "BROWSE";
            this.choosethumbbtn.UseVisualStyleBackColor = false;
            this.choosethumbbtn.Click += new System.EventHandler(this.choosethumbbtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Description";
            // 
            // descriptiontxt
            // 
            this.descriptiontxt.Location = new System.Drawing.Point(76, 96);
            this.descriptiontxt.Multiline = true;
            this.descriptiontxt.Name = "descriptiontxt";
            this.descriptiontxt.Size = new System.Drawing.Size(290, 69);
            this.descriptiontxt.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Name";
            // 
            // nametxt
            // 
            this.nametxt.Location = new System.Drawing.Point(76, 20);
            this.nametxt.Name = "nametxt";
            this.nametxt.Size = new System.Drawing.Size(287, 20);
            this.nametxt.TabIndex = 12;
            // 
            // addplaylistbtn
            // 
            this.addplaylistbtn.BackColor = System.Drawing.Color.Gainsboro;
            this.addplaylistbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addplaylistbtn.Location = new System.Drawing.Point(-1, 165);
            this.addplaylistbtn.Name = "addplaylistbtn";
            this.addplaylistbtn.Size = new System.Drawing.Size(376, 23);
            this.addplaylistbtn.TabIndex = 14;
            this.addplaylistbtn.Text = "ADD PLAYLIST";
            this.addplaylistbtn.UseVisualStyleBackColor = false;
            this.addplaylistbtn.Click += new System.EventHandler(this.addplaylistbtn_Click);
            // 
            // thumbfileopendialog
            // 
            this.thumbfileopendialog.Filter = "audios|*.flv;*.mp4;*.mov;*.3gp;*.3g2;*.f4v";
            // 
            // addplaylist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(375, 187);
            this.Controls.Add(this.addplaylistbtn);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "addplaylist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ADD PLAYLIST";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.addplaylist_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox thumbnametxt;
        private System.Windows.Forms.Button choosethumbbtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox descriptiontxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nametxt;
        private System.Windows.Forms.Button addplaylistbtn;
        private System.Windows.Forms.OpenFileDialog thumbfileopendialog;
    }
}