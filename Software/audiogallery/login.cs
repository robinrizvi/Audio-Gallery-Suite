/*  --------------------------------------------------------------------------------------------------------------------
 *  AUDIO-GALLERY-SUITE
 *  --------------------------------------------------------------------------------------------------------------------
 *  Author:     Robin Rizvi
 *  Email:      mail@robinrizvi.info
 *  Website:    http://robinrizvi.info/
 *  Blog:       http://blog.robinrizvi.info/
 *  Copyright:  Copyright © 2012, Robin Rizvi
 *  License:    MIT (http://www.opensource.org/licenses/MIT)
 *  This attribution (header-comment) should remain intact while using, distributing or modifying the source in any way
 *  -------------------------------------------------------------------------------------------------------------------
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace audiogallery
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();

            softlogiclink.Links.Add(13, 6, "www.robinrizvi.info");
            softlogiclink.Links.Add(21, 9, "www.softlogicui.com");

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                loginbtn_Click(this, null);
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            if (unametxt.Text != string.Empty && pwdtxt.Text != string.Empty)
            {
                userstatuspic.Visible = false;
                loginloadingindicator.Visible = true;
                loginloadingindicator.Active = true;
                unametxt.Enabled = false;
                pwdtxt.Enabled = false;
                loginbtn.Enabled = false;
                Thread loginthread = new Thread(user.login);
                string[] unamepwd = { unametxt.Text, pwdtxt.Text };
                loginthread.Start(unamepwd);
                while (loginthread.IsAlive) Application.DoEvents();
                if (user.isvalid)
                {
                    userstatuspic.Image = Properties.Resources.user_accept;
                    userstatuspic.Visible = true;
                    loginloadingindicator.Visible = false;
                    loginloadingindicator.Active = false;
                    Application.DoEvents();
                    Thread.Sleep(2000);
                    main mainfrm = new main();
                    this.Hide();
                    mainfrm.ShowDialog();
                    this.Close();
                }
                else
                {
                    userstatuspic.Image = Properties.Resources.user_remove;
                    unametxt.Enabled = true;
                    pwdtxt.Enabled = true;
                    loginbtn.Enabled = true;
                    userstatuspic.Visible = true;
                    loginloadingindicator.Visible = false;
                    loginloadingindicator.Active = false;
                }
            }
            else MessageBox.Show("Please fill in all the fields.");
        }

        private void softlogiclink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }
    }
}
