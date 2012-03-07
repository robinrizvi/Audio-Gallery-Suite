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
using System.Threading;
using System.IO;
using System.Net;

namespace audiogallery
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void main_Shown(object sender, EventArgs e)
        {
            manageuserbtn.Visible = user.superuser;
            playlistloadingindicator.Visible = true;
            playlistloadingindicator.Active = true;
            showplaylists();
            playlistloadingindicator.Active = false;
            playlistloadingindicator.Visible = false;

            audioloadingindicator.Visible = true;
            audioloadingindicator.Active = true;
            if (playlistview.Items.Count > 0)
            {
                user.currentplaylistid = Int32.Parse(playlistview.Items[0].ImageKey);
                showaudios(user.currentplaylistid);
            }
            audioloadingindicator.Active = false;
            audioloadingindicator.Visible = false;
        }

        private void showplaylists()
        {
            Thread getplaylistthread = new Thread(user.getplaylists);
            getplaylistthread.Start();
            while (getplaylistthread.IsAlive) Application.DoEvents();
            playlistimagelist.Images.Clear();
            playlistview.Items.Clear();
            foreach (user.playlist playlist in user.playlists)
            {
                string filename = Application.StartupPath + "\\temp\\playlist_" + playlist.id + "\\" + playlist.thumb;
                Bitmap img = new Bitmap(filename);
                playlistimagelist.Images.Add(playlist.id.ToString(), img);
                playlistview.Items.Add(playlist.name, playlist.id.ToString());
            }
        }

        private void playlistlistview_Click(object sender, EventArgs e)
        {
            audioloadingindicator.Visible = true;
            audioloadingindicator.Active = true;
            user.currentplaylistid = Int32.Parse(playlistview.SelectedItems[0].ImageKey);
            showaudios(user.currentplaylistid);
            audioloadingindicator.Active = false;
            audioloadingindicator.Visible = false;
        }

        private void showaudios(Int32 currentplaylistid)
        {
            Thread getaudiothread = new Thread(user.getaudios);
            getaudiothread.Start(currentplaylistid);
            while (getaudiothread.IsAlive) Application.DoEvents();
            audioimagelist.Images.Clear();
            audiolistview.Items.Clear();
            foreach (user.audio audio in user.audios)
            {
                audioimagelist.Images.Add(audio.id.ToString(), Properties.Resources.audio);
                audiolistview.Items.Add(audio.title, audio.id.ToString());
            }
        }

        private void addaudioebtn_Click(object sender, EventArgs e)
        {
            if (user.uploading) MessageBox.Show("An upload is already in progress. Please wait for the upload to finish", "Please Wait", MessageBoxButtons.OK);
            else
            {
                upload frm = new upload();
                frm.ShowDialog();

                //refresh the picture view list box
                audioloadingindicator.Visible = true;
                audioloadingindicator.Active = true;
                showaudios(user.currentplaylistid);
                audioloadingindicator.Active = false;
                audioloadingindicator.Visible = false;
                //end refresh
            }
        }

        private void deleteaudiobtn_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> audios = new Dictionary<string, string>();
            string[] filenames = new string[audiolistview.SelectedItems.Count];
            for (int i=0;i<audiolistview.SelectedItems.Count;i++)
            {
                audios.Add(audiolistview.SelectedItems[i].ImageKey, audiolistview.SelectedItems[i].Text);
            }
            if (audios.Count > 0)
            {
                delete frm = new delete(audios);
                frm.ShowDialog();
            }
            else MessageBox.Show("Please select some audios to delete");

            //refresh the audio view list box
            audioloadingindicator.Visible = true;
            audioloadingindicator.Active = true;
            showaudios(user.currentplaylistid);
            audioloadingindicator.Active = false;
            audioloadingindicator.Visible = false;
            //end refresh
        }

        private void addplaylistbtn_Click(object sender, EventArgs e)
        {
            addplaylist frm = new addplaylist();
            frm.ShowDialog();

            playlistloadingindicator.Visible = true;
            playlistloadingindicator.Active = true;
            showplaylists();
            playlistloadingindicator.Active = false;
            playlistloadingindicator.Visible = false;
        }

        private void deleteplaylistbtn_Click(object sender, EventArgs e)
        {
            if (playlistview.SelectedItems.Count > 0)
            {
                if (DialogResult.OK == MessageBox.Show("Are you sure you want to delete the playlist?", "Delete Playlist", MessageBoxButtons.OKCancel))
                {
                    UInt32 playlisttodeleteid = UInt32.Parse(playlistview.SelectedItems[0].ImageKey);
                    try
                    {
                        if (!(user.deleteplaylist(playlisttodeleteid))) throw new Exception();
                        Thread deleteplaylistthread = new Thread(deleteplaylistthreadfunction);
                        deleteplaylistthread.Start(playlisttodeleteid);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Sorry the playlist could not be deleted");
                        return;
                    }
                    //MessageBox.Show("Playlist has been deleted.");
                    #region refresh the main form
                    playlistloadingindicator.Visible = true;
                    playlistloadingindicator.Active = true;
                    showplaylists();
                    playlistloadingindicator.Active = false;
                    playlistloadingindicator.Visible = false;
                    audioloadingindicator.Visible = true;
                    audioloadingindicator.Active = true;
                    if (playlistview.Items.Count > 0)
                    {
                        user.currentplaylistid = Int32.Parse(playlistview.Items[0].ImageKey);
                        showaudios(user.currentplaylistid);
                    }
                    else audiolistview.Items.Clear();
                    audioloadingindicator.Active = false;
                    audioloadingindicator.Visible = false;
                    #endregion
                }
            }
            else MessageBox.Show("Please select the playlist to delete");
        }

        private void editplaylistbtn_Click(object sender, EventArgs e)
        {
            if (playlistview.SelectedItems.Count > 0)
            {
                UInt32 playlisttoeditid = UInt32.Parse(playlistview.SelectedItems[0].ImageKey);
                editplaylist frm = new editplaylist(playlisttoeditid);
                frm.ShowDialog();
                #region refresh the main form
                playlistloadingindicator.Visible = true;
                playlistloadingindicator.Active = true;
                showplaylists();
                playlistloadingindicator.Active = false;
                playlistloadingindicator.Visible = false;
                #endregion
            }
            else MessageBox.Show("Please select a playlist to update");
        }

        private void deleteplaylistthreadfunction(object data)
        {
            UInt32 playlisttodeleteid = (UInt32)data;
            deleteplaylist frm = new deleteplaylist(playlisttodeleteid);
            frm.ShowDialog();
        }

        private void changepwdbtn_Click(object sender, EventArgs e)
        {
            changepassword frm = new changepassword();
            frm.ShowDialog();
        }

        private void manageuserbtn_Click(object sender, EventArgs e)
        {
            manageuser frm = new manageuser();
            frm.ShowDialog();
        }

        private void editaudiobtn_Click(object sender, EventArgs e)
        {
            if (audiolistview.SelectedItems.Count == 1)
            {
                ulong audiotoeditid = ulong.Parse(audiolistview.SelectedItems[0].ImageKey);
                editaudio frm = new editaudio(audiotoeditid);
                frm.ShowDialog();
                //refresh the audio view list box
                audioloadingindicator.Visible = true;
                audioloadingindicator.Active = true;
                showaudios(user.currentplaylistid);
                audioloadingindicator.Active = false;
                audioloadingindicator.Visible = false;
                //end refresh
            }
            else if (audiolistview.SelectedItems.Count < 1) MessageBox.Show("Please select an audio to edit");
            else MessageBox.Show("Please select an single audio to edit");
        }
    }
}
