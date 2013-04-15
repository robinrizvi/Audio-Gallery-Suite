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

namespace audiogallery
{
    public partial class changepassword : Form
    {
        public changepassword()
        {
            InitializeComponent();
        }

        private void chnagepwdbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(oldpwdtxt.Text) || string.IsNullOrWhiteSpace(newpwdtxt.Text) || string.IsNullOrWhiteSpace(confirmpwdtxt.Text)) MessageBox.Show("Please fill in all the fields");
            else
            {
                if (newpwdtxt.Text == confirmpwdtxt.Text)
                {
                    if (user.checkuserpassword(oldpwdtxt.Text))
                    {
                        if (user.changepassword(newpwdtxt.Text))
                        {
                            MessageBox.Show("Password has been changed successfully");
                            this.Close();
                        }
                        else MessageBox.Show("Password could not be changed successfully due to some internal error");
                    }
                    else MessageBox.Show("The old password entered is incorrect");
                }
                else MessageBox.Show("The new password and confirm password fields do not match");
            }
        }
    }
}
