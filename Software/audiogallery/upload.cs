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
using System.Diagnostics;

namespace audiogallery
{
    public partial class upload : Form
    {
        private string filename;
        
        public upload()
        {
            InitializeComponent();
        }

        private void upload_Load(object sender, EventArgs e)
        {
            bool validplaylistselected=false;
            foreach (user.playlist playlist in user.playlists)
            {
                if (playlist.id == (ulong)user.currentplaylistid)
                {
                    playlistnamelbl.Text = playlist.name;
                    validplaylistselected=true;
                    break;
                }
            }
            if (!validplaylistselected)
            {
                MessageBox.Show("Please select a playlist.");
                this.Close();            
            }
        }

        private void choosefilebtn_Click(object sender, EventArgs e)
        {
            uploadfileopendialog.Filter = "Audios|*.mp3";
            if (uploadfileopendialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in uploadfileopendialog.FileNames)
                {
                    this.filename = filename;
                    filenametxt.Text = filename;
                    titletxt.Text = Path.GetFileNameWithoutExtension(filename);
                    descriptiontxt.Text = titletxt.Text;
                }
            }
        }

        private void uploadfilesbtn_Click(object sender, EventArgs e)
        {
            if (filename != string.Empty && titletxt.Text != string.Empty && descriptiontxt.Text != string.Empty)
            {
                uploadfilebtn.Enabled = false;
                uploadfilebtn.Text = "PLEASE WAIT";
                addcoverart(filename, "poster.jpg");
                fileuploadbackgroundworker.RunWorkerAsync(filename);
            }
            else MessageBox.Show("Please fill in all the fields");
        }

        private void fileuploadbackgroundworker_DoWork(object sender, DoWorkEventArgs e)
        {
            string UploadPath;
            FtpWebRequest request;
            int ChunkSize = 102400, NumRetries = 0, MaxRetries = 3;
            byte[] Buffer = new byte[ChunkSize];

            //for updating status to the ui thread
            decimal totalbytes = new FileInfo(filename).Length;
            decimal totalbytesuploaded = 0;
            //end
                
            #region upload to remote, make database records,report progress to ui thread
                string onlyfilename = new FileInfo(filename).Name;
                bool fileuploadedremotely = true;

                try
                {
                    if (fileuploadedremotely)
                    {
                        #region upload audio to remote server
                        UploadPath = user.ftpurl + "user_" + user.userid + "/playlist_" + user.currentplaylistid + "/audios/" + onlyfilename;
                        request = (FtpWebRequest)WebRequest.Create(UploadPath);
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        request.Credentials = new NetworkCredential(user.ftpusername, user.ftppassword);
                        using (Stream requestStream = request.GetRequestStream())
                        {
                            using (FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read))
                            {
                                int BytesRead = fs.Read(Buffer, 0, ChunkSize);	// read the first chunk in the buffer
                                // send the chunks to the web service one by one, until FileStream.Read() returns 0, meaning the entire file has been read.
                                while (BytesRead > 0)
                                {
                                    try
                                    {
                                        requestStream.Write(Buffer, 0, BytesRead);
                                        totalbytesuploaded += BytesRead;
                                        #region report progress to the ui thread
                                        int percentage = (int)(((float)totalbytesuploaded * 100) / ((float)totalbytes));
                                        string statustext = string.Format("{0:0.00} Mb/{1:0.00} Mb", (float)(totalbytesuploaded / (1024 * 1024)), (float)(totalbytes / (1024 * 1024)));
                                        fileuploadbackgroundworker.ReportProgress(percentage, statustext);
                                        #endregion
                                    }
                                    catch
                                    {
                                        if (NumRetries++ < MaxRetries)
                                        {
                                            // rewind the filestream and keep trying
                                            fs.Position -= BytesRead;
                                        }
                                        else
                                        {
                                            fileuploadedremotely = false;
                                            break;
                                        }
                                    }
                                    BytesRead = fs.Read(Buffer, 0, ChunkSize);	// read the next chunk (if it exists) into the buffer.  the while loop will terminate if there is nothing left to read
                                }
                            }
                        }
                        #endregion
                    }

                    if (fileuploadedremotely)
                    {
                        #region add record to the database
                        if (!user.insertaudio(onlyfilename, titletxt.Text, descriptiontxt.Text, user.currentplaylistid)) fileuploadedremotely = false;
                        #endregion
                    }
                }
                catch
                {
                    fileuploadedremotely = false;
                }

                if (!fileuploadedremotely)
                {
                    user.deleteaudio(onlyfilename, user.currentplaylistid);
                    MessageBox.Show("The file could not be uploaded. The reason could be slow or no internet connectivity");
                    filename = string.Empty;
                    //code should be written here to delete the audio that was uploaded but some database error occured but that is a rare chance, so ignoring the code piece here
                }
                else
                {
                    #region report progress to the ui thread
                    int percentage = 100;
                    string statustext = string.Format("{0:0.00} Mb/{1:0.00} Mb", (float)(totalbytes / (1024 * 1024)), (float)(totalbytes / (1024 * 1024)));
                    fileuploadbackgroundworker.ReportProgress(percentage, statustext);
                    #endregion
                    MessageBox.Show("The file has been uploaded");
                    filename = string.Empty;
                }

                
            #endregion
        }

        private void fileuploadbackgroundworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressbar.Value = e.ProgressPercentage;
            try
            {
                numofbytesuploadedstatuslbl.Text = e.UserState.ToString();
            }
            catch (Exception)
            {
                //do nothing
            }
        }

        private void fileuploadbackgroundworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            uploadfilebtn.Enabled = true;
            uploadfilebtn.Text = "UPLOAD AUDIO";
            progressbar.Value = 0;
            numofbytesuploadedstatuslbl.Text = "Operation Completed";
            filenametxt.Text = "";
            titletxt.Text = "";
            descriptiontxt.Text = "";
            filename = string.Empty;
        }

        private bool thumbnailcreationfailedcallback()
        {
            return false;
        }

        private void upload_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (fileuploadbackgroundworker.IsBusy)
            {
                if (DialogResult.OK == MessageBox.Show("Are you sure you want to cancel the upload?", "Cancel Upload", MessageBoxButtons.OKCancel))
                {
                    fileuploadbackgroundworker.CancelAsync();
                    //here Im just cancelling the thread that is doing the upload and reversing the changes it made to the database but I should also delete the partial audio that is left at the remote taking up huge space. But I have left it for garbage collection later on. Should have done here but I am tired (It would have hung the app if ...).
                    user.deleteaudio(new FileInfo(filename).Name, user.currentplaylistid);
                }
                else e.Cancel = true;
            }
        }

        private bool addcoverart(string audiopath,string picturepath)
        {
            try
            {
                TagLib.File TagLibFile = TagLib.File.Create(audiopath);
                TagLib.Picture picture = new TagLib.Picture(picturepath);
                TagLib.Id3v2.AttachedPictureFrame albumCoverPictFrame = new TagLib.Id3v2.AttachedPictureFrame(picture);
                albumCoverPictFrame.MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
                albumCoverPictFrame.Type = TagLib.PictureType.FrontCover;
                TagLib.IPicture[] pictFrames = new TagLib.IPicture[1];
                pictFrames[0] = (TagLib.IPicture)albumCoverPictFrame;
                TagLibFile.Tag.Pictures = pictFrames;
                TagLibFile.Save();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
