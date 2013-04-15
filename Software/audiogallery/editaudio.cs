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
    public partial class editaudio : Form
    {
        user.audio audio;
        Thread editaudiothread = null;

        public editaudio(ulong audioid)
        {
            foreach (user.audio audio in user.audios)
            {
                if (audio.id == audioid)
                {
                    this.audio = audio;
                    break;
                }
            }
            InitializeComponent();
        }

        private void editaudio_Load(object sender, EventArgs e)
        {
            nametxt.Text = audio.name;
            titletxt.Text = audio.title;
            descriptiontxt.Text = audio.description;
        }

        private void editaudiobtn_Click(object sender, EventArgs e)
        {
            editaudiobtn.Enabled = false;
            titletxt.Enabled = false;
            descriptiontxt.Enabled = false;
            editaudiobtn.Text = "Please Wait...";
            Application.DoEvents();
            if (string.IsNullOrEmpty(nametxt.Text) || string.IsNullOrEmpty(titletxt.Text) || string.IsNullOrEmpty(descriptiontxt.Text))
            {
                MessageBox.Show("Please fill in all the fields");
            }
            else
            {
                string[] fields = {titletxt.Text,descriptiontxt.Text};
                editaudiothread = new Thread(audioedit);
                editaudiothread.Start(fields);
                while (editaudiothread.IsAlive) Application.DoEvents();
            }
            this.Close();
        }

        private void audioedit(object fields)
        {
            string[] title_decription = (string[])fields;
            if (user.updateaudio(audio.id, title_decription[0], title_decription[1])) MessageBox.Show("The audio has been updated");
            else MessageBox.Show("The audio could not updated due to a title mismatch or some internal errors. Try changing the title or try again later");
        }

        private void editaudio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (editaudiothread != null && editaudiothread.IsAlive)
            {
                MessageBox.Show("An audio is being updated. Please wait just a few seconds.");
                e.Cancel = true;
            }
        }
    }
}
