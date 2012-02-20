namespace audiogallery
{
    partial class editaudio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(editaudio));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.titletxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.descriptiontxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nametxt = new System.Windows.Forms.TextBox();
            this.editaudiobtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.titletxt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.descriptiontxt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nametxt);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(-1, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 170);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Title";
            // 
            // titletxt
            // 
            this.titletxt.Location = new System.Drawing.Point(76, 55);
            this.titletxt.Name = "titletxt";
            this.titletxt.Size = new System.Drawing.Size(290, 20);
            this.titletxt.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 95);
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
            this.descriptiontxt.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Name";
            // 
            // nametxt
            // 
            this.nametxt.Enabled = false;
            this.nametxt.Location = new System.Drawing.Point(76, 20);
            this.nametxt.Name = "nametxt";
            this.nametxt.Size = new System.Drawing.Size(290, 20);
            this.nametxt.TabIndex = 1;
            // 
            // editaudiobtn
            // 
            this.editaudiobtn.BackColor = System.Drawing.Color.Gainsboro;
            this.editaudiobtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.editaudiobtn.Location = new System.Drawing.Point(-2, 165);
            this.editaudiobtn.Name = "editaudiobtn";
            this.editaudiobtn.Size = new System.Drawing.Size(377, 23);
            this.editaudiobtn.TabIndex = 4;
            this.editaudiobtn.Text = "UPDATE AUDIO";
            this.editaudiobtn.UseVisualStyleBackColor = false;
            this.editaudiobtn.Click += new System.EventHandler(this.editaudiobtn_Click);
            // 
            // editaudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(375, 187);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.editaudiobtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "editaudio";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Audio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.editaudio_FormClosing);
            this.Load += new System.EventHandler(this.editaudio_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox titletxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox descriptiontxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nametxt;
        private System.Windows.Forms.Button editaudiobtn;
    }
}