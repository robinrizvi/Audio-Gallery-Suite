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
using System.IO;
using System.Net;

namespace audiogallery
{
    public partial class delete : Form
    {
        Dictionary<string, string> audios;

        public delete(Dictionary<string,string> audios)
        {
            InitializeComponent();
            if (audios.Keys.Count > 0)
            {
                this.audios = audios;
            }
        }

        private void delete_Load(object sender, EventArgs e)
        {
            foreach (user.playlist playlist in user.playlists)
            {
                if (playlist.id == (ulong)user.currentplaylistid)
                {
                    albumnamelbl.Text = playlist.name;
                    break;
                }
            }
            foreach (string audioname in audios.Values)
            {
                filestodeletelistbox.Items.Add(audioname);
            }
        }

        private void deletefilesbtn_Click(object sender, EventArgs e)
        {
            if (audios.Keys.Count > 0)
            {
                deletefilesbtn.Enabled = false;
                deleteaudiosbackgroundworker.RunWorkerAsync();
            }
            
        }

        private void deleteaudiosbackgroundworker_DoWork(object sender, DoWorkEventArgs e)
        {
            long totalfiles = audios.Keys.Count;
            long totalfilesdeleted = 0;
            long totalfilesskipped = 0;
            
            //selecting each audio one by one and deleting it from database,local temp and remote server
            foreach (KeyValuePair<string, string> audio in audios)
            {
                bool filedeleted = false;
                string audiofilename=user.getaudiofilename(ulong.Parse(audio.Key));

                #region deleting audio record from database
                if (user.deleteaudio(ulong.Parse(audio.Key))) filedeleted = true;
                #endregion

                if (filedeleted == true)
                {
                    #region deleting audio from the local temp folder
                    try
                    {
                        string pathtoaudio = Application.StartupPath + "\\temp\\playlist_" + user.currentplaylistid + "\\audios\\" + audiofilename;
                        if (File.Exists(pathtoaudio)) File.Delete(pathtoaudio);
                    }
                    catch
                    {
                        //do nothing. Later on we can do garbage collection in the local temp folder
                    }
                    #endregion
                }

                if (filedeleted == true)
                {
                    #region deleting audio from the remote server
                    try
                    {
                        #region deleting audio from the remote server
                        string deletepath = user.ftpurl + "user_" + user.userid + "/playlist_" + user.currentplaylistid + "/audios/" + audiofilename;
                        FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(deletepath);
                        request.Credentials = new NetworkCredential(user.ftpusername, user.ftppassword);
                        request.KeepAlive = true;
                        request.Method = WebRequestMethods.Ftp.DeleteFile;
                        request.GetResponse();
                        #endregion
                    }
                    catch
                    {
                        //do nothing. Later on we could do garbage collection at the remote server
                    }
                    #endregion
                }

                if (filedeleted == true)
                {
                    totalfilesdeleted++;
                }
                else
                {
                    totalfilesskipped++;
                }

                long[] userstate=new long[2];
                userstate[0]=totalfilesdeleted;
                userstate[1]=totalfiles;
                int percentage = (int)((float)(totalfilesdeleted * 100) / (float)(totalfiles - totalfilesskipped));
                deleteaudiosbackgroundworker.ReportProgress(percentage, userstate);
            }
            if (totalfilesskipped > 0) MessageBox.Show("Some of the files could not be deleted");
        }

        private void deleteaudiosbackgroundworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string labeltxt = ((long[])e.UserState)[0].ToString() + "/" + ((long[])e.UserState)[1].ToString();
            deletestatuslbl.Text = labeltxt;
            deleteprogressbar.Value = e.ProgressPercentage;
        }

        private void deleteaudiosbackgroundworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            deletestatuslbl.Text = "Operation Completed";
            MessageBox.Show("The delete operation has completed");
            this.Close();
        }

        private void delete_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (deleteaudiosbackgroundworker.IsBusy)
            {
                if (DialogResult.OK == MessageBox.Show("Are you sure you want to cancel the deletion?", "Cancel Upload", MessageBoxButtons.OKCancel))
                {
                    deleteaudiosbackgroundworker.CancelAsync();
                }
                else e.Cancel = true;
            }
        }
    }
}
