/*  --------------------------------------------------------------------------------------------------------------------
 *  AUDIO-GALLERY-SUITE
 *  --------------------------------------------------------------------------------------------------------------------
 *  Author:     Robin Rizvi
 *  Email:      mail@robinrizvi.info
 *  Website:    http://robinrizvi.info/
 *  Blog:       http://blog.robinrizvi.info/
 *  Company:    SoftLogic (http://softlogicui.com/)
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
using System.Collections;

namespace audiogallery
{
    public partial class manageuser : Form
    { 
        public manageuser()
        {
            InitializeComponent();
        }

        private void createuserbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nametxt.Text) || string.IsNullOrWhiteSpace(unametxt.Text) || string.IsNullOrWhiteSpace(pwdtxt.Text) || string.IsNullOrWhiteSpace(confirmpwdtxt.Text) || string.IsNullOrWhiteSpace(descriptiontxt.Text)) MessageBox.Show("Please fill in all the fields");
            else
            {
                if (pwdtxt.Text == confirmpwdtxt.Text)
                {
                    createuserbtn.Enabled = false;
                    createuserbtn.Text = "PLEASE WAIT";
                    Application.DoEvents();
                    if (user.createuser(nametxt.Text, unametxt.Text, pwdtxt.Text, descriptiontxt.Text))
                    {
                        MessageBox.Show("The user has been created");
                        clearfields();
                    }
                    else MessageBox.Show("The user could not be created due to some internal error or username mismatch. Try changing the username");
                    createuserbtn.Enabled = true;
                    createuserbtn.Text = "CREATE USER";
                }
                else MessageBox.Show("The password and confirm password fields do not match");
            }
        }

        private void clearfields()
        {
            nametxt.Text = string.Empty;
            unametxt.Text = string.Empty;
            pwdtxt.Text = string.Empty;
            confirmpwdtxt.Text = string.Empty;
            descriptiontxt.Text = string.Empty;
        }

        private void disablefields()
        {
            editnametxt.Enabled = false;
            editunametxt.Enabled = false;
            editpwdtxt.Enabled = false;
            editconfirmpwdtxt.Enabled = false;
            editdescriptiontxt.Enabled = false;
            editusrbtn.Enabled = false;
        }

        private void disabledeletefields()
        {
            deletenametxt.Enabled = false;
            deleteunametxt.Enabled = false;
            deletepwdtxt.Enabled = false;
            deletedescriptiontxt.Enabled = false;
            deleteuserbtn.Enabled = false;
        }

        private void edittab_Enter(object sender, EventArgs e)
        {
            Dictionary<string, UInt32> users = user.listallusers();
            ArrayList userslist = new ArrayList();
            foreach (KeyValuePair<string, UInt32> eachuser in users)
            {
                userslist.Add(eachuser);
            }
            usercmb.DisplayMember = "Key";
            usercmb.ValueMember = "Value";
            usercmb.DataSource = userslist;
            if (usercmb.Items.Count <= 0) disablefields();
            else usercmb.SelectedIndex = 0;
        }

        private void usercmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usercmb.SelectedIndex != -1)
            {
                string[] userdetails = user.getuserdetails((UInt32)usercmb.SelectedValue);
                editnametxt.Text = userdetails[0];
                editunametxt.Text = userdetails[1];
                editpwdtxt.Text = userdetails[2];
                editconfirmpwdtxt.Text = userdetails[2];
                editdescriptiontxt.Text = userdetails[3];
            }
        }

        private void editusrbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(editnametxt.Text) || string.IsNullOrWhiteSpace(editunametxt.Text) || string.IsNullOrWhiteSpace(editpwdtxt.Text) || string.IsNullOrWhiteSpace(editconfirmpwdtxt.Text) || string.IsNullOrWhiteSpace(editdescriptiontxt.Text)) MessageBox.Show("Please fill in all the fields");
            else
            {
                if (editpwdtxt.Text == editconfirmpwdtxt.Text)
                {
                    editusrbtn.Enabled = false;
                    editusrbtn.Text = "PLEASE WAIT";
                    Application.DoEvents();
                    if (user.edituser((UInt32)usercmb.SelectedValue, editnametxt.Text, editunametxt.Text, editpwdtxt.Text, editdescriptiontxt.Text))
                    {
                        refresheditcmb(usercmb.SelectedIndex);
                        MessageBox.Show("This user has been edited successfully");
                    }
                    else MessageBox.Show("The user could not be edited due to some internal error or username mismatch. Try changing the username");
                    editusrbtn.Enabled = true;
                    editusrbtn.Text = "EDIT USER";
                }
                else MessageBox.Show("The password and confirm password fields do not match");
            }
        }

        private void refresheditcmb(int currentindex)
        {
            Dictionary<string, UInt32> users = user.listallusers();
            ArrayList userslist = new ArrayList();
            foreach (KeyValuePair<string, UInt32> eachuser in users)
            {
                userslist.Add(eachuser);
            }
            usercmb.DisplayMember = "Key";
            usercmb.ValueMember = "Value";
            usercmb.DataSource = userslist;
            if (usercmb.Items.Count <= 0) disablefields();
            else usercmb.SelectedIndex = currentindex;
        }

        private void deletetab_Enter(object sender, EventArgs e)
        {
            Dictionary<string, UInt32> users = user.listnormalusers();
            ArrayList userslist = new ArrayList();
            foreach (KeyValuePair<string, UInt32> eachuser in users)
            {
                userslist.Add(eachuser);
            }
            deleteusercmb.DisplayMember = "Key";
            deleteusercmb.ValueMember = "Value";
            deleteusercmb.DataSource = userslist;
            if (deleteusercmb.Items.Count <= 0) disabledeletefields();
            else deleteusercmb.SelectedIndex = 0;
        }

        private void deleteusercmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deleteusercmb.SelectedIndex != -1)
            {
                string[] userdetails = user.getuserdetails((UInt32)deleteusercmb.SelectedValue);
                deletenametxt.Text = userdetails[0];
                deleteunametxt.Text = userdetails[1];
                deletepwdtxt.Text = userdetails[2];
                deletedescriptiontxt.Text = userdetails[3];
            }
        }

        private void deleteuserbtn_Click(object sender, EventArgs e)
        {
            deleteuserbtn.Enabled = false;
            deleteuserbtn.Text = "PLEASE WAIT";
            Application.DoEvents();
            if (user.deleteuser((UInt32)deleteusercmb.SelectedValue))
            {
                deletetab_Enter(this, null);
                MessageBox.Show("The user has been deleted");
            }
            else MessageBox.Show("The user could not be deleted due to some internal errors");
            deleteuserbtn.Enabled = true;
            deleteuserbtn.Text = "DELETE USER";
        }
    }
}
