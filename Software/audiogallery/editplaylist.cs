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
using System.Threading;

namespace audiogallery
{
    public partial class editplaylist : Form
    {
        user.playlist playlisttoedit;
        Thread editplaylistthread=null;
        
        public editplaylist(UInt32 playlistid)
        {
            foreach (user.playlist playlist in user.playlists)
            {
                if (playlist.id == (ulong)playlistid)
                {
                    playlisttoedit = playlist;
                    break;
                }
            }
            InitializeComponent();
        }

        private void editplaylist_Load(object sender, EventArgs e)
        {
            nametxt.Text = playlisttoedit.name;
            thumbnametxt.Text = playlisttoedit.thumb;
            descriptiontxt.Text = playlisttoedit.description;
        }

        private void editplaylistbtn_Click(object sender, EventArgs e)
        {
            editplaylistbtn.Enabled = false;
            nametxt.Enabled = false;
            thumbnametxt.Enabled = false;
            descriptiontxt.Enabled = false;
            editplaylistbtn.Text = "Please Wait...";
            Application.DoEvents();
            if (string.IsNullOrEmpty(nametxt.Text) || string.IsNullOrEmpty(thumbnametxt.Text) || string.IsNullOrEmpty(descriptiontxt.Text))
            {
                MessageBox.Show("Please fill in all the fields");
            }
            else
            {
                editplaylistthread = new Thread(playlistedit);
                editplaylistthread.Start();
                while (editplaylistthread.IsAlive) Application.DoEvents();
            }
            this.Close();
        }

        private bool thumbnailcreationfailedcallback()
        {
            return false;
        }

        private void choosethumbbtn_Click(object sender, EventArgs e)
        {
            thumbfileopendialog.Filter = "Images|*.jpg;*.jpeg;*.bmp";
            if (thumbfileopendialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in thumbfileopendialog.FileNames)
                {
                    thumbnametxt.Text = filename;
                }
            }
        }

        private void playlistedit()
        {
            bool thumbuploaded = true;
            if (new FileInfo(thumbnametxt.Text).Name != playlisttoedit.thumb)
            {
                #region uploading thumbnail of the playlist to the remote server
                try
                {
                    int thumbnailwidth = 150;
                    int thumbnailheight = 150;
                    int ChunkSize = 4096, NumRetries = 0, MaxRetries = 20;
                    byte[] Buffer = new byte[ChunkSize];
                    string onlythumbname = new FileInfo(thumbnametxt.Text).Name;
                    Image thumbimg = Image.FromFile(thumbnametxt.Text).GetThumbnailImage(thumbnailwidth, thumbnailheight, thumbnailcreationfailedcallback, IntPtr.Zero);
                    MemoryStream thumbimgstream = new MemoryStream();
                    thumbimg.Save(thumbimgstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    thumbimgstream.Position = 0;
                    string UploadPath = user.ftpurl + "user_" + user.userid + "/playlist_" + playlisttoedit.id + "/" + onlythumbname;
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(UploadPath);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.KeepAlive = true;
                    request.Credentials = new NetworkCredential(user.ftpusername, user.ftppassword);
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        using (thumbimgstream)
                        {
                            int BytesRead = thumbimgstream.Read(Buffer, 0, ChunkSize);	// read the first chunk in the buffer
                            // send the chunks to the web service one by one, until FileStream.Read() returns 0, meaning the entire file has been read.
                            while (BytesRead > 0)
                            {
                                try
                                {
                                    requestStream.Write(Buffer, 0, BytesRead);
                                }
                                catch
                                {
                                    if (NumRetries++ < MaxRetries)
                                    {
                                        // rewind the imagememeorystream and keep trying
                                        thumbimgstream.Position -= BytesRead;
                                    }
                                    else
                                    {
                                        throw new Exception();
                                    }
                                }
                                BytesRead = thumbimgstream.Read(Buffer, 0, ChunkSize);	// read the next chunk (if it exists) into the buffer.  the while loop will terminate if there is nothing left to read
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    //thumbnail could not be uploaded
                    thumbuploaded = false;
                }
                #endregion
            }
            if (thumbuploaded)
            {
                if (!(user.updateplaylist((UInt32)playlisttoedit.id, nametxt.Text, new FileInfo(thumbnametxt.Text).Name, descriptiontxt.Text)))
                {
                    #region deleting the new thumbnail that was uploaded because the entry in the database could not be made
                    if (new FileInfo(thumbnametxt.Text).Name != playlisttoedit.thumb)
                    {
                        try
                        {
                            string deletepath = user.ftpurl + "user_" + user.userid + "/playlist_" + playlisttoedit.id + "/" + new FileInfo(thumbnametxt.Text).Name;
                            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(deletepath);
                            request.Credentials = new NetworkCredential(user.ftpusername, user.ftppassword);
                            request.Method = WebRequestMethods.Ftp.DeleteFile;
                            request.KeepAlive = true;
                            request.GetResponse();
                        }
                        catch (Exception)
                        {
                            //do nothing if thumb could not be deleted, later on it'll be done via garbage collector   
                        }
                    }
                    #endregion
                    MessageBox.Show("The playlist could not be updated");
                }
                else
                {
                    #region deleting the old playlist thumb from the remote server and the local temp folder
                    if (new FileInfo(thumbnametxt.Text).Name != playlisttoedit.thumb)
                    {
                        try
                        {
                            string deletepath = user.ftpurl + "user_" + user.userid + "/playlist_" + playlisttoedit.id + "/" + playlisttoedit.thumb;
                            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(deletepath);
                            request.Credentials = new NetworkCredential(user.ftpusername, user.ftppassword);
                            request.Method = WebRequestMethods.Ftp.DeleteFile;
                            request.KeepAlive = true;
                            request.GetResponse();
                            //deleting the old thumb from the local temp folder
                            deletepath = Application.StartupPath + "\\temp\\playlist_" + playlisttoedit.id + "\\" + playlisttoedit.thumb;
                            if (File.Exists(deletepath)) File.Delete(deletepath);
                        }
                        catch (Exception)
                        {
                            //do nothing if old thumb could not be deleted, later on it'll be done via garbage collector   
                        }
                    }
                    #endregion
                    MessageBox.Show("The playlist has been updated");
                }
            }
            else
            {
                MessageBox.Show("The playlist could not be updated");
            }
        }

        private void editplaylist_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (editplaylistthread!=null && editplaylistthread.IsAlive)
            {
                MessageBox.Show("An playlist is being updated. Please wait just a few seconds.");
                e.Cancel = true;
            }
        }
    }
}
