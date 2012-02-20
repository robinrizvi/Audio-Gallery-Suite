namespace audiogallery
{
    partial class login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.loginbtn = new System.Windows.Forms.Button();
            this.pwdtxt = new System.Windows.Forms.TextBox();
            this.unametxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.panel3 = new System.Windows.Forms.Panel();
            this.softlogiclink = new System.Windows.Forms.LinkLabel();
            this.userstatuspic = new System.Windows.Forms.PictureBox();
            this.loginloadingindicator = new MRG.Controls.UI.LoadingCircle();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userstatuspic)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.loginbtn);
            this.groupBox1.Controls.Add(this.pwdtxt);
            this.groupBox1.Controls.Add(this.unametxt);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(1, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 166);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enter your credentials";
            // 
            // loginbtn
            // 
            this.loginbtn.BackColor = System.Drawing.Color.Gainsboro;
            this.loginbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.loginbtn.Location = new System.Drawing.Point(151, 109);
            this.loginbtn.Name = "loginbtn";
            this.loginbtn.Size = new System.Drawing.Size(75, 23);
            this.loginbtn.TabIndex = 9;
            this.loginbtn.Text = "LOGIN";
            this.loginbtn.UseVisualStyleBackColor = false;
            this.loginbtn.Click += new System.EventHandler(this.loginbtn_Click);
            // 
            // pwdtxt
            // 
            this.pwdtxt.Location = new System.Drawing.Point(152, 71);
            this.pwdtxt.Name = "pwdtxt";
            this.pwdtxt.Size = new System.Drawing.Size(163, 20);
            this.pwdtxt.TabIndex = 8;
            this.pwdtxt.UseSystemPasswordChar = true;
            // 
            // unametxt
            // 
            this.unametxt.Location = new System.Drawing.Point(152, 40);
            this.unametxt.Name = "unametxt";
            this.unametxt.Size = new System.Drawing.Size(163, 20);
            this.unametxt.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(67, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "PASSWORD";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.Location = new System.Drawing.Point(67, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "USERNAME";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Helvetica-Condensed", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(98, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 36);
            this.label1.TabIndex = 4;
            this.label1.Text = "AUDIO GALLERY";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 52);
            this.panel1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::audiogallery.Properties.Resources.sl_logo;
            this.pictureBox1.Location = new System.Drawing.Point(376, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(413, 285);
            this.shapeContainer1.TabIndex = 6;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape2
            // 
            this.lineShape2.BorderColor = System.Drawing.Color.White;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 1;
            this.lineShape2.X2 = 412;
            this.lineShape2.Y1 = 253;
            this.lineShape2.Y2 = 253;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.Color.White;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 0;
            this.lineShape1.X2 = 411;
            this.lineShape1.Y1 = 52;
            this.lineShape1.Y2 = 52;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DimGray;
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.softlogiclink);
            this.panel3.Location = new System.Drawing.Point(0, 254);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(413, 32);
            this.panel3.TabIndex = 7;
            // 
            // softlogiclink
            // 
            this.softlogiclink.ActiveLinkColor = System.Drawing.Color.LightBlue;
            this.softlogiclink.AutoSize = true;
            this.softlogiclink.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.softlogiclink.Font = new System.Drawing.Font("Agency FB", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.softlogiclink.ForeColor = System.Drawing.SystemColors.ControlText;
            this.softlogiclink.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.softlogiclink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.softlogiclink.LinkColor = System.Drawing.SystemColors.ControlText;
            this.softlogiclink.Location = new System.Drawing.Point(95, 1);
            this.softlogiclink.Name = "softlogiclink";
            this.softlogiclink.Size = new System.Drawing.Size(221, 28);
            this.softlogiclink.TabIndex = 1;
            this.softlogiclink.Text = " DEVELOPED BY ROBIN (SOFTLOGIC)";
            this.softlogiclink.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.softlogiclink.UseCompatibleTextRendering = true;
            this.softlogiclink.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            this.softlogiclink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.softlogiclink_LinkClicked);
            // 
            // userstatuspic
            // 
            this.userstatuspic.Image = global::audiogallery.Properties.Resources.user_remove;
            this.userstatuspic.Location = new System.Drawing.Point(168, 0);
            this.userstatuspic.Name = "userstatuspic";
            this.userstatuspic.Size = new System.Drawing.Size(37, 31);
            this.userstatuspic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.userstatuspic.TabIndex = 8;
            this.userstatuspic.TabStop = false;
            // 
            // loginloadingindicator
            // 
            this.loginloadingindicator.Active = false;
            this.loginloadingindicator.Color = System.Drawing.Color.DarkGray;
            this.loginloadingindicator.InnerCircleRadius = 5;
            this.loginloadingindicator.Location = new System.Drawing.Point(168, 1);
            this.loginloadingindicator.Name = "loginloadingindicator";
            this.loginloadingindicator.NumberSpoke = 12;
            this.loginloadingindicator.OuterCircleRadius = 11;
            this.loginloadingindicator.RotationSpeed = 100;
            this.loginloadingindicator.Size = new System.Drawing.Size(37, 31);
            this.loginloadingindicator.SpokeThickness = 2;
            this.loginloadingindicator.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loginloadingindicator.TabIndex = 9;
            this.loginloadingindicator.Text = "loadingCircle1";
            this.loginloadingindicator.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DimGray;
            this.panel4.Controls.Add(this.loginloadingindicator);
            this.panel4.Controls.Add(this.userstatuspic);
            this.panel4.Location = new System.Drawing.Point(0, 221);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(413, 32);
            this.panel4.TabIndex = 8;
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(413, 285);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "login";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audio Gallery Management";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userstatuspic)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button loginbtn;
        private System.Windows.Forms.TextBox pwdtxt;
        private System.Windows.Forms.TextBox unametxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox userstatuspic;
        private MRG.Controls.UI.LoadingCircle loginloadingindicator;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.LinkLabel softlogiclink;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}