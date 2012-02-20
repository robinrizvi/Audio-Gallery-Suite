namespace audiogallery
{
    partial class main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.headerpanel = new System.Windows.Forms.Panel();
            this.manageuserbtn = new System.Windows.Forms.Button();
            this.changepwdbtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.footerpanel = new System.Windows.Forms.Panel();
            this.playlistview = new System.Windows.Forms.ListView();
            this.playlistimagelist = new System.Windows.Forms.ImageList(this.components);
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape6 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.toolbarpanel = new System.Windows.Forms.Panel();
            this.addaudiobtn = new System.Windows.Forms.Button();
            this.deleteaudiobtn = new System.Windows.Forms.Button();
            this.editaudiobtn = new System.Windows.Forms.Button();
            this.addplaylistbtn = new System.Windows.Forms.Button();
            this.deleteplaylistbtn = new System.Windows.Forms.Button();
            this.editplaylistbtn = new System.Windows.Forms.Button();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape4 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.panel1 = new System.Windows.Forms.Panel();
            this.audioloadingindicator = new MRG.Controls.UI.LoadingCircle();
            this.playlistloadingindicator = new MRG.Controls.UI.LoadingCircle();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.shapeContainer3 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape5 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.audiolistview = new System.Windows.Forms.ListView();
            this.audioimagelist = new System.Windows.Forms.ImageList(this.components);
            this.softlogiclink = new System.Windows.Forms.LinkLabel();
            this.headerpanel.SuspendLayout();
            this.footerpanel.SuspendLayout();
            this.toolbarpanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerpanel
            // 
            this.headerpanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.headerpanel.BackColor = System.Drawing.Color.DimGray;
            this.headerpanel.Controls.Add(this.manageuserbtn);
            this.headerpanel.Controls.Add(this.changepwdbtn);
            this.headerpanel.Controls.Add(this.label5);
            this.headerpanel.Location = new System.Drawing.Point(0, 0);
            this.headerpanel.Name = "headerpanel";
            this.headerpanel.Size = new System.Drawing.Size(1008, 45);
            this.headerpanel.TabIndex = 8;
            // 
            // manageuserbtn
            // 
            this.manageuserbtn.AutoSize = true;
            this.manageuserbtn.BackColor = System.Drawing.Color.Gainsboro;
            this.manageuserbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.manageuserbtn.Location = new System.Drawing.Point(796, 22);
            this.manageuserbtn.Name = "manageuserbtn";
            this.manageuserbtn.Size = new System.Drawing.Size(103, 23);
            this.manageuserbtn.TabIndex = 11;
            this.manageuserbtn.Text = "Manage Users";
            this.manageuserbtn.UseVisualStyleBackColor = false;
            this.manageuserbtn.Visible = false;
            this.manageuserbtn.Click += new System.EventHandler(this.manageuserbtn_Click);
            // 
            // changepwdbtn
            // 
            this.changepwdbtn.AutoSize = true;
            this.changepwdbtn.BackColor = System.Drawing.Color.Gainsboro;
            this.changepwdbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.changepwdbtn.Location = new System.Drawing.Point(905, 22);
            this.changepwdbtn.Name = "changepwdbtn";
            this.changepwdbtn.Size = new System.Drawing.Size(103, 23);
            this.changepwdbtn.TabIndex = 10;
            this.changepwdbtn.Text = "Change Password";
            this.changepwdbtn.UseVisualStyleBackColor = false;
            this.changepwdbtn.Click += new System.EventHandler(this.changepwdbtn_Click);
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Agency FB", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1008, 40);
            this.label5.TabIndex = 0;
            this.label5.Text = "AUDIO GALLERY MANAGEMENT";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // footerpanel
            // 
            this.footerpanel.BackColor = System.Drawing.Color.Gray;
            this.footerpanel.Controls.Add(this.softlogiclink);
            this.footerpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerpanel.Location = new System.Drawing.Point(0, 630);
            this.footerpanel.Name = "footerpanel";
            this.footerpanel.Size = new System.Drawing.Size(1008, 32);
            this.footerpanel.TabIndex = 9;
            // 
            // playlistview
            // 
            this.playlistview.BackColor = System.Drawing.Color.DimGray;
            this.playlistview.ForeColor = System.Drawing.Color.White;
            this.playlistview.LargeImageList = this.playlistimagelist;
            this.playlistview.Location = new System.Drawing.Point(0, 79);
            this.playlistview.MultiSelect = false;
            this.playlistview.Name = "playlistview";
            this.playlistview.Size = new System.Drawing.Size(219, 517);
            this.playlistview.SmallImageList = this.playlistimagelist;
            this.playlistview.TabIndex = 10;
            this.playlistview.UseCompatibleStateImageBehavior = false;
            this.playlistview.Click += new System.EventHandler(this.playlistlistview_Click);
            // 
            // playlistimagelist
            // 
            this.playlistimagelist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.playlistimagelist.ImageSize = new System.Drawing.Size(150, 150);
            this.playlistimagelist.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape6,
            this.lineShape3,
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(1008, 662);
            this.shapeContainer1.TabIndex = 13;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape6
            // 
            this.lineShape6.BorderColor = System.Drawing.Color.White;
            this.lineShape6.Name = "lineShape6";
            this.lineShape6.X1 = 2;
            this.lineShape6.X2 = 1006;
            this.lineShape6.Y1 = 78;
            this.lineShape6.Y2 = 78;
            // 
            // lineShape3
            // 
            this.lineShape3.BorderColor = System.Drawing.Color.White;
            this.lineShape3.Name = "lineShape3";
            this.lineShape3.X1 = 2;
            this.lineShape3.X2 = 1006;
            this.lineShape3.Y1 = 596;
            this.lineShape3.Y2 = 596;
            // 
            // lineShape2
            // 
            this.lineShape2.BorderColor = System.Drawing.Color.White;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 2;
            this.lineShape2.X2 = 1006;
            this.lineShape2.Y1 = 629;
            this.lineShape2.Y2 = 629;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.Color.White;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 1;
            this.lineShape1.X2 = 1005;
            this.lineShape1.Y1 = 45;
            this.lineShape1.Y2 = 45;
            // 
            // toolbarpanel
            // 
            this.toolbarpanel.BackColor = System.Drawing.Color.Gray;
            this.toolbarpanel.Controls.Add(this.addaudiobtn);
            this.toolbarpanel.Controls.Add(this.deleteaudiobtn);
            this.toolbarpanel.Controls.Add(this.editaudiobtn);
            this.toolbarpanel.Controls.Add(this.addplaylistbtn);
            this.toolbarpanel.Controls.Add(this.deleteplaylistbtn);
            this.toolbarpanel.Controls.Add(this.editplaylistbtn);
            this.toolbarpanel.Controls.Add(this.shapeContainer2);
            this.toolbarpanel.Location = new System.Drawing.Point(0, 597);
            this.toolbarpanel.Name = "toolbarpanel";
            this.toolbarpanel.Size = new System.Drawing.Size(1008, 32);
            this.toolbarpanel.TabIndex = 10;
            // 
            // addaudiobtn
            // 
            this.addaudiobtn.AutoSize = true;
            this.addaudiobtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addaudiobtn.BackColor = System.Drawing.Color.Gainsboro;
            this.addaudiobtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addaudiobtn.Location = new System.Drawing.Point(962, 5);
            this.addaudiobtn.Name = "addaudiobtn";
            this.addaudiobtn.Size = new System.Drawing.Size(40, 23);
            this.addaudiobtn.TabIndex = 25;
            this.addaudiobtn.Text = "ADD";
            this.addaudiobtn.UseVisualStyleBackColor = false;
            this.addaudiobtn.Click += new System.EventHandler(this.addaudioebtn_Click);
            // 
            // deleteaudiobtn
            // 
            this.deleteaudiobtn.AutoSize = true;
            this.deleteaudiobtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.deleteaudiobtn.BackColor = System.Drawing.Color.Gainsboro;
            this.deleteaudiobtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.deleteaudiobtn.Location = new System.Drawing.Point(897, 5);
            this.deleteaudiobtn.Name = "deleteaudiobtn";
            this.deleteaudiobtn.Size = new System.Drawing.Size(59, 23);
            this.deleteaudiobtn.TabIndex = 24;
            this.deleteaudiobtn.Text = "DELETE";
            this.deleteaudiobtn.UseVisualStyleBackColor = false;
            this.deleteaudiobtn.Click += new System.EventHandler(this.deleteaudiobtn_Click);
            // 
            // editaudiobtn
            // 
            this.editaudiobtn.AutoSize = true;
            this.editaudiobtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editaudiobtn.BackColor = System.Drawing.Color.Gainsboro;
            this.editaudiobtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.editaudiobtn.Location = new System.Drawing.Point(849, 5);
            this.editaudiobtn.Name = "editaudiobtn";
            this.editaudiobtn.Size = new System.Drawing.Size(42, 23);
            this.editaudiobtn.TabIndex = 23;
            this.editaudiobtn.Text = "EDIT";
            this.editaudiobtn.UseVisualStyleBackColor = false;
            this.editaudiobtn.Click += new System.EventHandler(this.editaudiobtn_Click);
            // 
            // addplaylistbtn
            // 
            this.addplaylistbtn.AutoSize = true;
            this.addplaylistbtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addplaylistbtn.BackColor = System.Drawing.Color.Gainsboro;
            this.addplaylistbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addplaylistbtn.Location = new System.Drawing.Point(172, 5);
            this.addplaylistbtn.Name = "addplaylistbtn";
            this.addplaylistbtn.Size = new System.Drawing.Size(40, 23);
            this.addplaylistbtn.TabIndex = 22;
            this.addplaylistbtn.Text = "ADD";
            this.addplaylistbtn.UseVisualStyleBackColor = false;
            this.addplaylistbtn.Click += new System.EventHandler(this.addplaylistbtn_Click);
            // 
            // deleteplaylistbtn
            // 
            this.deleteplaylistbtn.AutoSize = true;
            this.deleteplaylistbtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.deleteplaylistbtn.BackColor = System.Drawing.Color.Gainsboro;
            this.deleteplaylistbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.deleteplaylistbtn.Location = new System.Drawing.Point(79, 5);
            this.deleteplaylistbtn.Name = "deleteplaylistbtn";
            this.deleteplaylistbtn.Size = new System.Drawing.Size(59, 23);
            this.deleteplaylistbtn.TabIndex = 21;
            this.deleteplaylistbtn.Text = "DELETE";
            this.deleteplaylistbtn.UseVisualStyleBackColor = false;
            this.deleteplaylistbtn.Click += new System.EventHandler(this.deleteplaylistbtn_Click);
            // 
            // editplaylistbtn
            // 
            this.editplaylistbtn.AutoSize = true;
            this.editplaylistbtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editplaylistbtn.BackColor = System.Drawing.Color.Gainsboro;
            this.editplaylistbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.editplaylistbtn.Location = new System.Drawing.Point(2, 5);
            this.editplaylistbtn.Name = "editplaylistbtn";
            this.editplaylistbtn.Size = new System.Drawing.Size(42, 23);
            this.editplaylistbtn.TabIndex = 20;
            this.editplaylistbtn.Text = "EDIT";
            this.editplaylistbtn.UseVisualStyleBackColor = false;
            this.editplaylistbtn.Click += new System.EventHandler(this.editplaylistbtn_Click);
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape4});
            this.shapeContainer2.Size = new System.Drawing.Size(1008, 32);
            this.shapeContainer2.TabIndex = 0;
            this.shapeContainer2.TabStop = false;
            // 
            // lineShape4
            // 
            this.lineShape4.BorderColor = System.Drawing.Color.White;
            this.lineShape4.Name = "lineShape4";
            this.lineShape4.X1 = 215;
            this.lineShape4.X2 = 215;
            this.lineShape4.Y1 = 0;
            this.lineShape4.Y2 = 32;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.audioloadingindicator);
            this.panel1.Controls.Add(this.playlistloadingindicator);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.shapeContainer3);
            this.panel1.Location = new System.Drawing.Point(-1, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 31);
            this.panel1.TabIndex = 20;
            // 
            // audioloadingindicator
            // 
            this.audioloadingindicator.Active = false;
            this.audioloadingindicator.Color = System.Drawing.Color.Red;
            this.audioloadingindicator.InnerCircleRadius = 5;
            this.audioloadingindicator.Location = new System.Drawing.Point(978, 6);
            this.audioloadingindicator.Name = "audioloadingindicator";
            this.audioloadingindicator.NumberSpoke = 12;
            this.audioloadingindicator.OuterCircleRadius = 11;
            this.audioloadingindicator.RotationSpeed = 100;
            this.audioloadingindicator.Size = new System.Drawing.Size(25, 23);
            this.audioloadingindicator.SpokeThickness = 2;
            this.audioloadingindicator.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.audioloadingindicator.TabIndex = 26;
            this.audioloadingindicator.Text = "loadingCircle2";
            this.audioloadingindicator.UseWaitCursor = true;
            this.audioloadingindicator.Visible = false;
            // 
            // playlistloadingindicator
            // 
            this.playlistloadingindicator.Active = false;
            this.playlistloadingindicator.Color = System.Drawing.Color.Red;
            this.playlistloadingindicator.InnerCircleRadius = 5;
            this.playlistloadingindicator.Location = new System.Drawing.Point(184, 6);
            this.playlistloadingindicator.Name = "playlistloadingindicator";
            this.playlistloadingindicator.NumberSpoke = 12;
            this.playlistloadingindicator.OuterCircleRadius = 11;
            this.playlistloadingindicator.RotationSpeed = 100;
            this.playlistloadingindicator.Size = new System.Drawing.Size(25, 23);
            this.playlistloadingindicator.SpokeThickness = 2;
            this.playlistloadingindicator.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.playlistloadingindicator.TabIndex = 24;
            this.playlistloadingindicator.Text = "loadingCircle1";
            this.playlistloadingindicator.UseWaitCursor = true;
            this.playlistloadingindicator.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(585, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "AUDIOS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(51, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "PLAYLISTS";
            // 
            // shapeContainer3
            // 
            this.shapeContainer3.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer3.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer3.Name = "shapeContainer3";
            this.shapeContainer3.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape5});
            this.shapeContainer3.Size = new System.Drawing.Size(1008, 31);
            this.shapeContainer3.TabIndex = 0;
            this.shapeContainer3.TabStop = false;
            // 
            // lineShape5
            // 
            this.lineShape5.BorderColor = System.Drawing.Color.White;
            this.lineShape5.Name = "lineShape4";
            this.lineShape5.X1 = 215;
            this.lineShape5.X2 = 215;
            this.lineShape5.Y1 = 0;
            this.lineShape5.Y2 = 32;
            // 
            // audiolistview
            // 
            this.audiolistview.BackColor = System.Drawing.Color.DimGray;
            this.audiolistview.ForeColor = System.Drawing.Color.White;
            this.audiolistview.LargeImageList = this.audioimagelist;
            this.audiolistview.Location = new System.Drawing.Point(215, 79);
            this.audiolistview.Name = "audiolistview";
            this.audiolistview.Size = new System.Drawing.Size(793, 517);
            this.audiolistview.SmallImageList = this.audioimagelist;
            this.audiolistview.TabIndex = 14;
            this.audiolistview.UseCompatibleStateImageBehavior = false;
            // 
            // audioimagelist
            // 
            this.audioimagelist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.audioimagelist.ImageSize = new System.Drawing.Size(145, 91);
            this.audioimagelist.TransparentColor = System.Drawing.Color.Transparent;
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
            this.softlogiclink.Location = new System.Drawing.Point(403, 2);
            this.softlogiclink.Name = "softlogiclink";
            this.softlogiclink.Size = new System.Drawing.Size(221, 28);
            this.softlogiclink.TabIndex = 2;
            this.softlogiclink.Text = " DEVELOPED BY ROBIN (SOFTLOGIC)";
            this.softlogiclink.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.softlogiclink.UseCompatibleTextRendering = true;
            this.softlogiclink.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(1008, 662);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolbarpanel);
            this.Controls.Add(this.footerpanel);
            this.Controls.Add(this.headerpanel);
            this.Controls.Add(this.audiolistview);
            this.Controls.Add(this.playlistview);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audio Gallery Management";
            this.Shown += new System.EventHandler(this.main_Shown);
            this.headerpanel.ResumeLayout(false);
            this.headerpanel.PerformLayout();
            this.footerpanel.ResumeLayout(false);
            this.footerpanel.PerformLayout();
            this.toolbarpanel.ResumeLayout(false);
            this.toolbarpanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerpanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel footerpanel;
        private System.Windows.Forms.ListView playlistview;
        private System.Windows.Forms.ImageList playlistimagelist;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
        private System.Windows.Forms.Panel toolbarpanel;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape4;
        private System.Windows.Forms.ListView audiolistview;
        private System.Windows.Forms.Panel panel1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer3;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape5;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList audioimagelist;
        private System.Windows.Forms.Button changepwdbtn;
        private System.Windows.Forms.Button manageuserbtn;
        private System.Windows.Forms.Button addplaylistbtn;
        private System.Windows.Forms.Button deleteplaylistbtn;
        private System.Windows.Forms.Button editplaylistbtn;
        private MRG.Controls.UI.LoadingCircle audioloadingindicator;
        private MRG.Controls.UI.LoadingCircle playlistloadingindicator;
        private System.Windows.Forms.Button addaudiobtn;
        private System.Windows.Forms.Button deleteaudiobtn;
        private System.Windows.Forms.Button editaudiobtn;
        private System.Windows.Forms.LinkLabel softlogiclink;


    }
}