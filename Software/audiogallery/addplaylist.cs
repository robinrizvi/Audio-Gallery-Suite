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
using System.Net;
using System.IO;
using System.Threading;

namespace audiogallery
{
    public partial class addplaylist : Form
    {
        Thread addplaylistthread;
        
        public addplaylist()
        {
            InitializeComponent();
        }

        private void addplaylistbtn_Click(object sender, EventArgs e)
        {
            addplaylistbtn.Enabled = false;
            nametxt.Enabled = false;
            thumbnametxt.Enabled = false;
            descriptiontxt.Enabled = false;
            choosethumbbtn.Enabled = false;
            addplaylistbtn.Text = "Please Wait...";
            Application.DoEvents();
            if (string.IsNullOrEmpty(nametxt.Text) || string.IsNullOrEmpty(thumbnametxt.Text) || string.IsNullOrEmpty(descriptiontxt.Text))
            {
                MessageBox.Show("Please fill in all the fields");
            }
            else
            {
                addplaylistthread = new Thread(playlistadd);
                addplaylistthread.Start();
                while (addplaylistthread.IsAlive) Application.DoEvents();
            }
            nametxt.Text = string.Empty;
            thumbnametxt.Text = string.Empty;
            descriptiontxt.Text = string.Empty;
            nametxt.Enabled = true;
            thumbnametxt.Enabled = true;
            descriptiontxt.Enabled = true;
            choosethumbbtn.Enabled = true;
            addplaylistbtn.Enabled = true;
            addplaylistbtn.Text = "ADD PLAYLIST";
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

        private bool thumbnailcreationfailedcallback()
        {
            return false;
        }

        private void playlistadd()
        {
            int thumbnailwidth = 150;
            int thumbnailheight = 150;
            try
            {
                #region add records in the playlist table for the new playlist
                UInt32 newaddedplaylistid = user.addplaylist(nametxt.Text, new FileInfo(thumbnametxt.Text).Name, descriptiontxt.Text);
                if (newaddedplaylistid == 0) throw new Exception();
                #endregion
                try
                {
                    #region creating folders for the playlist on the remote server
                    //creating playlist folder
                    string createpath = user.ftpurl + "user_" + user.userid + "/playlist_" + newaddedplaylistid;
                    FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(createpath);
                    request.Credentials = new NetworkCredential(user.ftpusername, user.ftppassword);
                    request.KeepAlive = true;
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.GetResponse();

                    //creating thumbs folder in playlist folder
                    //createpath = user.ftpurl + "user_" + user.userid + "/playlist_" + newaddedplaylistid + "/thumbs";
                    //request = (FtpWebRequest)FtpWebRequest.Create(createpath);
                    //request.Credentials = new NetworkCredential(user.ftpusername, user.ftppassword);
                    //request.KeepAlive = true;
                    //request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    //request.GetResponse();

                    //creating audios folder in the playlist folder
                    createpath = user.ftpurl + "user_" + user.userid + "/playlist_" + newaddedplaylistid + "/audios";
                    request = (FtpWebRequest)FtpWebRequest.Create(createpath);
                    request.Credentials = new NetworkCredential(user.ftpusername, user.ftppassword);
                    request.KeepAlive = true;
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.GetResponse();
                    #endregion

                    #region uploading thumbnail of the playlist to the remote server
                    try
                    {
                        int ChunkSize = 4096, NumRetries = 0, MaxRetries = 20;
                        byte[] Buffer = new byte[ChunkSize];
                        string onlythumbname = new FileInfo(thumbnametxt.Text).Name;
                        Image thumbimg = Image.FromFile(thumbnametxt.Text).GetThumbnailImage(thumbnailwidth, thumbnailheight, thumbnailcreationfailedcallback, IntPtr.Zero);
                        MemoryStream thumbimgstream = new MemoryStream();
                        thumbimg.Save(thumbimgstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        thumbimgstream.Position = 0;
                        string UploadPath = user.ftpurl + "user_" + user.userid + "/playlist_" + newaddedplaylistid + "/" + onlythumbname;
                        request = (FtpWebRequest)WebRequest.Create(UploadPath);
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
                        //thumbnail could not be uploaded so remote folders have to be deleted
                        #region deleting folder that were created for the playlist
                        string deletepath = user.ftpurl + "user_" + user.userid + "/playlist_" + newaddedplaylistid;
                        new DeleteFTPDirectory().DeleteDirectoryHierarcy(deletepath);
                        #endregion
                        throw new Exception();
                    }
                    #endregion
                }
                catch (Exception)
                {
                    #region deleting playlist record from the database
                    //If folder could not be created or thumbnail could not be created remove the playlist record from the playlist table in the database
                    user.deleteplaylist(newaddedplaylistid);
                    throw new Exception();
                    #endregion
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The playlist could not be added");
                return;
            }
            MessageBox.Show("The playlist has been added");
            //this.Close();
        }

        private void addplaylist_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (addplaylistthread!=null && addplaylistthread.IsAlive)
            {
                MessageBox.Show("An playlist is being added. Please wait just a few seconds.");
                e.Cancel = true;
            }
        }
    }
}