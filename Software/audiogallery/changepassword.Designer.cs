namespace audiogallery
{
    partial class changepassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(changepassword));
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.confirmpwdtxt = new System.Windows.Forms.TextBox();
            this.newpwdtxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.oldpwdtxt = new System.Windows.Forms.TextBox();
            this.chnagepwdbtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "New Password";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.confirmpwdtxt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.newpwdtxt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.oldpwdtxt);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 128);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Confirm password";
            // 
            // confirmpwdtxt
            // 
            this.confirmpwdtxt.Location = new System.Drawing.Point(96, 93);
            this.confirmpwdtxt.Name = "confirmpwdtxt";
            this.confirmpwdtxt.Size = new System.Drawing.Size(267, 20);
            this.confirmpwdtxt.TabIndex = 21;
            this.confirmpwdtxt.UseSystemPasswordChar = true;
            // 
            // newpwdtxt
            // 
            this.newpwdtxt.Location = new System.Drawing.Point(96, 57);
            this.newpwdtxt.Name = "newpwdtxt";
            this.newpwdtxt.Size = new System.Drawing.Size(267, 20);
            this.newpwdtxt.TabIndex = 19;
            this.newpwdtxt.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Old Password";
            // 
            // oldpwdtxt
            // 
            this.oldpwdtxt.Location = new System.Drawing.Point(96, 22);
            this.oldpwdtxt.Name = "oldpwdtxt";
            this.oldpwdtxt.Size = new System.Drawing.Size(268, 20);
            this.oldpwdtxt.TabIndex = 12;
            this.oldpwdtxt.UseSystemPasswordChar = true;
            // 
            // chnagepwdbtn
            // 
            this.chnagepwdbtn.BackColor = System.Drawing.Color.Gainsboro;
            this.chnagepwdbtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chnagepwdbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chnagepwdbtn.Location = new System.Drawing.Point(0, 122);
            this.chnagepwdbtn.Name = "chnagepwdbtn";
            this.chnagepwdbtn.Size = new System.Drawing.Size(375, 23);
            this.chnagepwdbtn.TabIndex = 16;
            this.chnagepwdbtn.Text = "CHANGE PASSWORD";
            this.chnagepwdbtn.UseVisualStyleBackColor = false;
            this.chnagepwdbtn.Click += new System.EventHandler(this.chnagepwdbtn_Click);
            // 
            // changepassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(375, 145);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chnagepwdbtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "changepassword";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Password";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox newpwdtxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox oldpwdtxt;
        private System.Windows.Forms.Button chnagepwdbtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox confirmpwdtxt;
    }
}