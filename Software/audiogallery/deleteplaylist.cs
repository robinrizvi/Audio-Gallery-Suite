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
using System.IO;

namespace audiogallery
{
    public partial class deleteplaylist : Form
    {
        UInt32 playlisttodeleteid;
        Thread folderdeletion;
        
        public deleteplaylist(UInt32 playlisttodeleteid)
        {
            InitializeComponent();
            this.playlisttodeleteid = playlisttodeleteid;
        }

        void folderdeletionthread()
        {
            #region deleting folder that were created for the playlist on the remote server and the local temp folder
            try
            {
                //deleting on the remote server
                string deletepath = user.ftpurl + "user_" + user.userid + "/playlist_" + playlisttodeleteid;
                new DeleteFTPDirectory().DeleteDirectoryHierarcy(deletepath);
                //deleting on the local temp folder
                deletepath = Application.StartupPath + "\\temp\\playlist_" + playlisttodeleteid;
                if (Directory.Exists(deletepath)) Directory.Delete(deletepath, true);
            }
            catch (Exception)
            {
                //In case the remote folders could not be deleted I'll do nothing they will later be garbage collected
            }
            #endregion
        }

        private void deleteplaylist_Shown(object sender, EventArgs e)
        {
            //creating this thread so that the ui thread may remain responsive
            folderdeletion = new Thread(folderdeletionthread);
            folderdeletion.Start();
            while (folderdeletion.IsAlive) Application.DoEvents();
            MessageBox.Show("Playlist has been deleted.");
            this.Close();
        }

        private void deleteplaylist_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (folderdeletion.IsAlive)
            {
                MessageBox.Show("The deletion is still in progress. Please wait.");
                e.Cancel = true;
            }
        }
    }
}
