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
using System.Linq;
using System.Text;
using audiogallery.Properties;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace audiogallery
{
	public class FtpListing
	{

		#region Constructor

		public FtpListing()
		{

		}

		#endregion

		# region Private Members

		private string _fileName = string.Empty; // Represents the filename without extension

		private string _fileExtension = string.Empty; // Represents the file extension

		private string _path = string.Empty; // Represents the complete path

		private DirectoryEntryType _fileType; // Represents if the current listing represents a file/directory.

		private long _size; // Represents the size.

		private DateTime _fileDateTime; // DateTime of file/Directory

		private string _permissions; // Permissions on the directory

		private string _fullName; // Represents FileName with extension

		IFormatProvider culture = System.Globalization.CultureInfo.InvariantCulture; //Eliminate DateTime parsing issues.

		# endregion

		# region Public Properties

		public string FileName
		{

			get { return _fileName; }

			set
			{

				_fileName = value;

				// Set the FileExtension.

				if (_fileName.LastIndexOf(".") > -1)
				{

					FileExtension = _fileName.Substring(_fileName.LastIndexOf(".") + 1);

				}

			}

		}

		public string FileExtension
		{

			get { return _fileExtension; }

			set { _fileExtension = value; }

		}

		public string FullName
		{

			get { return _fullName; }

			set { _fullName = value; }

		}

		public string Path
		{

			get { return _path; }

			set { _path = value; }

		}

		internal DirectoryEntryType FileType;

		public long Size
		{

			get { return _size; }

			set { _size = value; }

		}

		public DateTime FileDateTime
		{

			get { return _fileDateTime; }

			set { _fileDateTime = value; }

		}

		public string Permissions
		{

			get { return _permissions; }

			set { _permissions = value; }

		}

		public string NameOnly
		{

			get
			{

				int i = this.FileName.LastIndexOf(".");

				if (i > 0)

					return this.FileName.Substring(0, i);

				else

					return this.FileName;

			}

		}

		public enum DirectoryEntryType
		{

			File,

			Directory

		}

		# endregion

		# region Regular expressions for parsing List results

		/// <summary>

		/// List of REGEX formats for different FTP server listing formats

		/// The first three are various UNIX/LINUX formats,

		/// fourth is for MS FTP in detailed mode and the last for MS FTP in 'DOS' mode.

		/// </summary>

		// These regular expressions will be used to match a directory/file

		// listing as explained at the top of this class.

		internal string[] _ParseFormats =

	{

		@"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\w+\s+\w+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{4})\s+(?<name>.+)",

		@"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\d+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{4})\s+(?<name>.+)",

		@"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\d+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{1,2}:\d{2})\s+(?<name>.+)",

		@"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\w+\s+\w+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{1,2}:\d{2})\s+(?<name>.+)",

		@"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})(\s+)(?<size>(\d+))(\s+)(?<ctbit>(\w+\s\w+))(\s+)(?<size2>(\d+))\s+(?<timestamp>\w+\s+\d+\s+\d{2}:\d{2})\s+(?<name>.+)",

		@"(?<timestamp>\d{2}\-\d{2}\-\d{2}\s+\d{2}:\d{2}[Aa|Pp][mM])\s+(?<dir>\<\w+\>){0,1}(?<size>\d+){0,1}\s+(?<name>.+)",

		@"([<timestamp>]*\d{2}\-\d{2}\-\d{2}\s+\d{2}:\d{2}[Aa|Pp][mM])\s+([<dir>]*\<\w+\>){0,1}([<size>]*\d+){0,1}\s+([<name>]*.+)"

	};

		# endregion

		# region Private Functions

		/// <summary>

		/// Depending on the various directory listing formats,

		/// the current listing will be matched against the set of available matches.

		/// </summary>

		/// <param name="line"></param>

		/// <returns></returns>

		// This method evaluates the directory/file listing by applying

		// each of the regular expression defined by the string array, _ParseFormats and returns success on a successful match.

		private Match GetMatchingRegex(string line)
		{

			Regex regExpression;

			Match match;

			int counter;

			for (counter = 0; counter < _ParseFormats.Length - 1; counter++)
			{

				regExpression = new Regex(_ParseFormats[counter], RegexOptions.IgnoreCase);

				match = regExpression.Match(line);

				if (match.Success)

					return match;

			}

			return null;

		}

		# endregion

		# region Public Functions

		/// <summary>

		/// The method accepts a directory listing and initialises all the attributes of a file.

		/// </summary>

		/// <param name="line">Directory Listing line returned by the DetailedDirectoryList method</param>

		/// <param name="path">The path of the Directory</param>

		// This method populates the needful properties such as filename,path etc.

		public void GetFtpFileInfo(string line, string path)
		{

			string directory;

			Match match = GetMatchingRegex(line); //Get the match of the current listing.

			if (match == null)

				throw new Exception("Unable to parse the line " + line);

			else
			{

				_fileName = match.Groups["name"].Value; //Set the name of the file/directory.

				_path = path; // Set the path from which the listing needs to be obtained.

				_permissions = match.Groups["permission"].Value; // Set the permissions available for the listing

				directory = match.Groups["dir"].Value;

				//Set the filetype to either Directory or File basing on the listing.

				if (!string.IsNullOrEmpty(directory) && directory != "-")

					_fileType = DirectoryEntryType.Directory;

				else
				{

					_fileType = DirectoryEntryType.File;

					_size = long.Parse(match.Groups["size"].Value, culture);

				}

				try
				{

					_fileDateTime = DateTime.Parse(match.Groups["timestamp"].Value, culture); // Set the datetime of the listing.

				}

				catch
				{

					_fileDateTime = DateTime.Now;

				}

			}

			// Initialize the readonly properties.

			FileName = _fileName;

			Path = _path;

			FileType = _fileType;

			FullName = Path + FileName;

			Size = _size;

			FileDateTime = _fileDateTime;

			Permissions = _permissions;

		}

		# endregion

	}
	public class DeleteFTPDirectory
	{

		public DeleteFTPDirectory()
		{

		}

		// Set the credentials here to access the target FTP Site.

		// For ananymous access, leave it blank.

		public ICredentials GetCredentials()
		{
			return new NetworkCredential(audiogallery.user.ftpusername, audiogallery.user.ftppassword); //Fill this to logon to your FTP Server.
		}

		//Send the command/request to the target FTP location identified by the parameter, URI

		public FtpWebRequest GetRequest(string URI)
		{

			FtpWebRequest result = ((FtpWebRequest)(FtpWebRequest.Create(URI)));

			result.Credentials = GetCredentials();

			result.KeepAlive = true;//this was set to false earlier which degraded performance

			result.EnableSsl = false;

			result.UsePassive = false;

			result.ReadWriteTimeout = 100;

			return result;

		}

		//Deletes the target FTP file identified by the parameter, filePath

		public bool DeleteFile(string filePath)
		{

			string URI = (filePath.TrimEnd());

			FtpWebRequest ftp = GetRequest(URI);

			ftp.Credentials = GetCredentials();

			ftp.Method = WebRequestMethods.Ftp.DeleteFile;

			string str = GetStringResponse(ftp);

			return true;

		}

		//The master method that deletes the entire directory hierarchy. Root directory is identified by the parameter, dirPath

		// Remember: if the path was ftp://testserver/testfolder/testorder then all the folders inside the testorder folder will be deleted including testorder folder.

		public bool DeleteDirectoryHierarcy(string dirPath)
		{

			FtpListing fileObj = new FtpListing(); //Create the FTPListing object

			ArrayList DirectoriesList = new ArrayList(); //Create a collection to hold list of directories within the root and including the root directory.

			DirectoriesList.Add(dirPath); //Add the root folder to the collection

			string currentDirectory = string.Empty;



			//For each of the directories in the DirectoriesListCollection, obtain the

			// sub directories. Add them to the collection.Repeat the process until every path

			// was traversed. For example consider the following hierarchy.



			// Ex: ftp://testftpserver/testfolders/testfolder

			// Sample Hierarchy of testfolder

			// testfolder

			// - testfolder1

			// - testfolder2

			// - testfolder3

			// - testfile.txt

			// - testfolder4

			// DirectorieList is a self modifying collection. Meaning it loops through itself and adds directories to itself.

			// DirectoriesList will have the following entries basing on the above hierarchy by the time the first for loop gets complete executed.

			//ftp://testftpserver/testfolders/testfolder

			//ftp://testftpserver/testfolders/testfolder/testfolder1

			//ftp://testftpserver/testfolders/testfolder/testfolder2

			//ftp://testftpserver/testfolders/testfolder/testfolder4

			//ftp://testftpserver/testfolders/testfolder/testfolder1/testfolder3

			//At the end of the for loop the DirectorLists is traversed bottom up

			//Meaning deletion of folders will be from the last entry towards to first,

			//which would ensure that all the subdirectories are deleted before the root folder is going to be deleted.

			for (int directoryCount = 0; directoryCount < DirectoriesList.Count; directoryCount++)
			{

				currentDirectory = DirectoriesList[directoryCount].ToString() + "/";

				if (currentDirectory.StartsWith("ftp://", StringComparison.OrdinalIgnoreCase) == false)
				{

					currentDirectory = string.Concat("ftp://", currentDirectory);

				}

				string[] directoryInfo = GetDirectoryList(currentDirectory);

				for (int counter = 0; counter < directoryInfo.Length; counter++)
				{

					string currentFileOrDir = directoryInfo[counter];

					if (currentFileOrDir.Length < 1) // If all entries were scanned then break
					{

						break;

					}

					currentFileOrDir = currentFileOrDir.Replace("\r", "");

					fileObj.GetFtpFileInfo(currentFileOrDir, currentDirectory);

					if (fileObj.FileType == FtpListing.DirectoryEntryType.Directory)
					{

						if (fileObj.FileName != "." && fileObj.FileName != "..") DirectoriesList.Add(fileObj.FullName); //If Directory add to the collection.

					}

					else if (fileObj.FileType == FtpListing.DirectoryEntryType.File)
					{

						DeleteFile(fileObj.FullName); //If file,then delete.

					}

				}

			}

			//Remove the directories in the collection from bottom toward top.

			//This would ensure that all the sub directories were deleted first before deleting the root folder.

			for (int count = DirectoriesList.Count; count > 0; count--)
			{

				RemoveDirectory(DirectoriesList[count - 1].ToString());

			}

			return true;

		}

		//Deletes one target directory at a time, identified by the parameter, dirPath

		public bool RemoveDirectory(string dirPath)
		{

			dirPath = dirPath.Trim(new char[] { '/' }); //Trim the path.

			FtpWebRequest ftp = GetRequest(dirPath); //Prepare the request.

			ftp.Credentials = GetCredentials(); //Set the Credentials to access the ftp target.

			ftp.Method = WebRequestMethods.Ftp.RemoveDirectory; //set request method, RemoveDirectory

			string str = GetStringResponse(ftp); // Fire the command to remove directory.

			return true;

		}





		//Gets the directory listing given the path

		public string[] GetDirectoryList(string path)
		{

			string[] result = null;

			FtpWebRequest ftpReq = GetRequest(path.TrimEnd()); //Create the request

			ftpReq.Method = WebRequestMethods.Ftp.ListDirectoryDetails; //Set the request method

			ftpReq.Credentials = GetCredentials(); //Set the credentials

			FtpWebResponse ftpResp = (FtpWebResponse)ftpReq.GetResponse();//Fire the command

			Stream ftpResponseStream = ftpResp.GetResponseStream(); //Get the output

			StreamReader reader = new StreamReader(ftpResponseStream, System.Text.Encoding.UTF8);//Encode the output to UTF8 format

			result = (reader.ReadToEnd().Split('\n')); //Split the output for newline characters.

			ftpResp.Close(); //Close the response object.

			return result; // return the output

		}

		//Gets the response for a given request. Meaning executes the command identified

		// by FtpWebRequest and outputs the response/output.

		public string GetStringResponse(FtpWebRequest ftpRequest)
		{

			//Get the result, streaming to a string

			string result = "";

			using (FtpWebResponse response = ((FtpWebResponse)(ftpRequest.GetResponse()))) //Get the response for the request identified by the parameter ftpRequest
			{

				using (Stream datastream = response.GetResponseStream())
				{

					using (StreamReader sr = new StreamReader(datastream))
					{

						result = sr.ReadToEnd();

						sr.Close();

					}

					datastream.Close();

				}

				response.Close();

			}

			return result;

		}

	}
	static class user
	{
		#region properties
		static MySqlConnection conn = new MySqlConnection(Resources.mysqlconnectionstring);
		public static string ftpurl;
		public static string ftpusername;
		public static string ftppassword;
		public struct playlist
		{
			public UInt64 id;
			public string name;
			public string description;
			public string thumb;
		}
		public struct audio
		{
			public UInt64 id;
			public string name;
			public string title;
			public string description;
		}
		#endregion

		#region currentstate
		public static bool isvalid;
		public static UInt32 userid;
		public static bool superuser;
		public static bool uploading = false;
		public static Int32 currentplaylistid = -1;
		public static List<playlist> playlists = new List<playlist>();
		public static List<audio> audios = new List<audio>();
		#endregion

		#region methods
		public static bool initialize()
		{
			bool initialized = false;
			try
			{
				if (conn.State == ConnectionState.Closed) conn.Open();
				string query = "SELECT * FROM settings";
				MySqlCommand cmd = new MySqlCommand(query, conn);
				MySqlDataReader rdr = cmd.ExecuteReader();
				if (rdr.Read())
				{
					ftpurl = rdr["ftpurl"].ToString();
					ftpusername = rdr["ftpusername"].ToString();
					ftppassword = rdr["ftppassword"].ToString();
					initialized = true;
				}
				conn.Close();
			}
			catch (Exception)
			{
				System.Windows.Forms.MessageBox.Show("The connection to the database could not be made. Please check your internet connection.");
				initialized = false;
				//System.Windows.Forms.Application.Exit();
			}
			return initialized;
		}

		public static void login(object data)
		{
			string[] unamepwd= (string[]) data;
			bool validuser = false;
			if (initialize())
			{
				try
				{
					if (conn.State == ConnectionState.Closed) conn.Open();
					string query = "SELECT * FROM user WHERE username=@uname and password=@pwd";
					MySqlCommand cmd = new MySqlCommand(query, conn);
					cmd.Parameters.AddWithValue("@uname", unamepwd[0]);
					cmd.Parameters.AddWithValue("@pwd", unamepwd[1]);
					MySqlDataReader rdr = cmd.ExecuteReader();
					if (rdr.Read())
					{
						validuser = true;
						userid = UInt32.Parse(rdr["id"].ToString());
						superuser=(userid==0);//if userid=0 then the user is the superuser
					}
					conn.Close();
				}
				catch (Exception)
				{
					System.Windows.Forms.MessageBox.Show("The connection to the database could not be made. Please check your internet connection.");
					validuser = false;
					//System.Windows.Forms.Application.Exit();
				}
			}
			isvalid=validuser;
		}

		public static void getplaylists()//reads the records from playlist table and for each entry calls downloadplaylist() to download the thumb and save it into local temp foler
		{
			playlists.Clear();
			try
			{
				if (conn.State == ConnectionState.Closed) conn.Open();
				string query = "SELECT * FROM playlist WHERE user_id=@userid";
				MySqlCommand cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@userid", userid);
				MySqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					playlist a1;
					a1.id = UInt64.Parse(rdr["id"].ToString());
					a1.name = rdr["name"].ToString();
					a1.description = rdr["description"].ToString();
					a1.thumb = rdr["thumb"].ToString();
					if (downloadplaylistthumb(a1)) playlists.Add(a1);//add the playlist to the list of playlist and do not make it dependent on whether the thumb is downloaded or not
				}
				conn.Close();
			}
			catch (Exception)
			{
				System.Windows.Forms.MessageBox.Show("The connection to the database could not be made. Please check your internet connection." + Environment.NewLine+"The application will now exit.");
				System.Windows.Forms.Application.Exit();
			}
		}

		private static bool downloadplaylistthumb(playlist playlisttodownload)//downloads the playlist thumbs and saves it into local temp and adds the entries in the albums list
		{
			try
			{
				string pathtotemp = System.Windows.Forms.Application.StartupPath + "\\temp";
				if (!Directory.Exists(pathtotemp)) Directory.CreateDirectory(pathtotemp);
				string pathtoplaylist = pathtotemp + "\\playlist_" + playlisttodownload.id;
				if (!Directory.Exists(pathtoplaylist)) Directory.CreateDirectory(pathtoplaylist);
				if (!Directory.Exists(pathtoplaylist + "\\audios")) Directory.CreateDirectory(pathtoplaylist + "\\audios");
				//if (!Directory.Exists(pathtoplaylist + "\\thumbs")) Directory.CreateDirectory(pathtoplaylist + "\\thumbs");Audios have no thumbs
				string filename = pathtoplaylist + "\\" + playlisttodownload.thumb;
				if (!File.Exists(filename))
				{
					FileStream fs = new FileStream(filename, FileMode.Create);
					string remotealbumthumb = "user_" + userid + "/playlist_" + playlisttodownload.id + "/" + playlisttodownload.thumb;
					FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpurl + remotealbumthumb);
					request.Method = WebRequestMethods.Ftp.DownloadFile;
					request.Credentials = new NetworkCredential(ftpusername, ftppassword);
					FtpWebResponse response = (FtpWebResponse)request.GetResponse();
					Stream responseStream = response.GetResponseStream();
					byte[] buffer = new byte[1024];
					int bytesread = 0;
					while ((bytesread = responseStream.Read(buffer, 0, buffer.Length)) > 0)
						fs.Write(buffer, 0, bytesread);
					fs.Close();
					responseStream.Close();
					response.Close();
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		public static void getaudios(object data)//reads records from the audio table for a particular albumid and then saves the entries in the pictures list
		{
			Int32 currentplaylistid = (Int32)data;
			audios.Clear();
			try
			{
				if (conn.State == ConnectionState.Closed) conn.Open();
				string query = "SELECT * FROM audio WHERE playlist_id=@playlistid";
				MySqlCommand cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@playlistid", currentplaylistid);
				MySqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					audio p1;
					p1.id = UInt64.Parse(rdr["id"].ToString());
					p1.name = rdr["name"].ToString();
					p1.title = rdr["title"].ToString();
					p1.description = rdr["description"].ToString();
					if (downloadaudio(p1, currentplaylistid)) audios.Add(p1);//add the audios to the list
				}
				conn.Close();
			}
			catch (Exception)
			{
				System.Windows.Forms.MessageBox.Show("The connection to the database could not be made. Please check your internet connection." + Environment.NewLine+"The application will now exit.");
				System.Windows.Forms.Application.Exit();
			}
		}

		private static bool downloadaudio(audio audiotodownload, Int32 playlistid)//downloads audio and audio thumbs and saves it to local temp in particular album's directory. This feature will be used in the future to allow the users to listen to audios as well inside the software
		{
			try
			{
				string pathtotemp = System.Windows.Forms.Application.StartupPath + "\\temp";
				if (!Directory.Exists(pathtotemp)) Directory.CreateDirectory(pathtotemp);
				string pathtoplaylist = pathtotemp + "\\playlist_" + playlistid;
				if (!Directory.Exists(pathtoplaylist)) Directory.CreateDirectory(pathtoplaylist);
				if (!Directory.Exists(pathtoplaylist + "\\audios")) Directory.CreateDirectory(pathtoplaylist + "\\audios");
				//if (!Directory.Exists(pathtoplaylist + "\\thumbs")) Directory.CreateDirectory(pathtoplaylist + "\\thumbs");
				string audiofilename = pathtoplaylist + "\\audios\\" + audiotodownload.name;
				//string audiothumbfilename = pathtoplaylist + "\\thumbs\\" + audiotodownload.thumb;
				//skipped downloading of the audio that would take a whole lot of time
				//if (!File.Exists(audiofilename))
				//{
				//    FileStream fs = new FileStream(audiofilename, FileMode.Create);
				//    string remotepicturefilename = "user_" + userid + "/playlist_" + playlistid + "/audios/" + audiotodownload.name;
				//    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpurl + remotepicturefilename);
				//    request.Method = WebRequestMethods.Ftp.DownloadFile;
				//    request.Credentials = new NetworkCredential(ftpusername, ftppassword);
				//    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
				//    Stream responseStream = response.GetResponseStream();
				//    byte[] buffer = new byte[1024];
				//    int bytesread = 0;
				//    while ((bytesread = responseStream.Read(buffer, 0, buffer.Length)) > 0)
				//        fs.Write(buffer, 0, bytesread);
				//    fs.Close();
				//    responseStream.Close();
				//    response.Close();
				//}
				//if (!File.Exists(audiothumbfilename))
				//{
				//    FileStream fs = new FileStream(audiothumbfilename, FileMode.Create);
				//    string remotealbumthumb = "user_" + userid + "/playlist_" + playlistid + "/thumbs/" + audiotodownload.thumb;
				//    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpurl + remotealbumthumb);
				//    request.Method = WebRequestMethods.Ftp.DownloadFile;
				//    request.Credentials = new NetworkCredential(ftpusername, ftppassword);
				//    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
				//    Stream responseStream = response.GetResponseStream();
				//    byte[] buffer = new byte[1024];
				//    int bytesread = 0;
				//    while ((bytesread = responseStream.Read(buffer, 0, buffer.Length)) > 0)
				//        fs.Write(buffer, 0, bytesread);
				//    fs.Close();
				//    responseStream.Close();
				//    response.Close();
				//}
			}
			catch
			{
				return false;
			}
			return true;
		}

		public static bool insertaudio(string name,string title,string description,long playlist_id)
		{
			bool inserted=true;
			try
			{
				if (conn.State == ConnectionState.Closed) conn.Open();
				string query = "INSERT INTO audio (name,title,description,playlist_id) VALUES(@name,@title,@description,@playlist_id)";
				MySqlCommand cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@name", name);
				cmd.Parameters.AddWithValue("@title", title);
				cmd.Parameters.AddWithValue("@description", description);
				cmd.Parameters.AddWithValue("@playlist_id", playlist_id);
				if (cmd.ExecuteNonQuery() < 0) inserted = false;
				conn.Close();
			}
			catch
			{
				inserted = false;
			}
			return inserted;
		}

		public static bool deleteaudio(ulong id)
		{
			bool deleted = true;
			try
			{
				if (conn.State == ConnectionState.Closed) conn.Open();
				string query = "DELETE FROM audio WHERE id=@id";
				MySqlCommand cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@id", id);
				if (cmd.ExecuteNonQuery() < 0) deleted = false;
				conn.Close();
			}
			catch
			{
				deleted = false;
			}
			return deleted;
		}

		public static bool deleteaudio(string name,int playlist_id)//this is an unnecessary method. But let it be
		{
			bool deleted = true;
			try
			{
				if (conn.State == ConnectionState.Closed) conn.Open();
				string query = "DELETE FROM audio WHERE name=@name and playlist_id=@playlist_id";
				MySqlCommand cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@name", name);
				cmd.Parameters.AddWithValue("@playlist_id", playlist_id);
				if (cmd.ExecuteNonQuery() < 0) deleted = false;
				conn.Close();
			}
			catch
			{
				deleted = false;
			}
			return deleted;
		}

		public static UInt32 addplaylist(string name,string thumb,string description)
		{
			UInt32 playlistid = 0;
			try
			{
				if (conn.State == ConnectionState.Closed) conn.Open();
				string query = "INSERT INTO playlist (name,thumb,description,user_id) VALUES(@name,@thumb,@description,@user_id)";
				MySqlCommand cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@name", name);
				cmd.Parameters.AddWithValue("@thumb", thumb);
				cmd.Parameters.AddWithValue("@description", description);
				cmd.Parameters.AddWithValue("@user_id", userid);
				if (cmd.ExecuteNonQuery() < 0) throw new Exception();

				query = "SELECT id FROM playlist WHERE name=@name AND user_id=@user_id";
				cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@name", name);
				cmd.Parameters.AddWithValue("@user_id", userid);
				MySqlDataReader rdr = cmd.ExecuteReader();
				if (rdr.Read()) playlistid = UInt32.Parse(rdr["id"].ToString());
				conn.Close();
			}
			catch
			{
				playlistid = 0;
			}
			return playlistid;
		}

		public static bool deleteplaylist(UInt32 id)
		{
			bool deleted = true;
			try
			{
				if (conn.State == ConnectionState.Closed) conn.Open();
				string query = "DELETE FROM playlist WHERE id=@id";
				MySqlCommand cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@id", id);
				if (cmd.ExecuteNonQuery() < 0) deleted = false;
				conn.Close();
			}
			catch
			{
				deleted = false;
			}
			return deleted;
		}

		public static bool updateplaylist(UInt32 id,string name,string thumb,string description)
		{
			bool updated = true;
			try
			{
				if (conn.State == ConnectionState.Closed) conn.Open();
				string query = "UPDATE playlist SET name=@name,thumb=@thumb,description=@description WHERE id=@id";
				MySqlCommand cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.Parameters.AddWithValue("@name", name);
				cmd.Parameters.AddWithValue("@thumb", thumb);
				cmd.Parameters.AddWithValue("@description", description);
				if (cmd.ExecuteNonQuery() < 0) updated = false;
				conn.Close();
			}
			catch
			{
				updated = false;
			}
			return updated;
		}

		public static bool checkuserpassword(string pwd)
		{
			bool validpwd = false;
			if (conn.State == ConnectionState.Closed) conn.Open();
			string query = "SELECT password FROM user where id=@userid";
			MySqlCommand cmd = new MySqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@userid", user.userid);
			MySqlDataReader rdr = cmd.ExecuteReader();
			if (rdr.Read())
			{
				string dbpwd = rdr["password"].ToString();
				if (dbpwd == pwd) validpwd = true;
			}
			conn.Close();
			return validpwd;
		}

		public static bool changepassword(string pwd)
		{
			bool changed = true;
			if (conn.State == ConnectionState.Closed) conn.Open();
			string query = "UPDATE user SET password=@pwd where id=@userid";
			MySqlCommand cmd = new MySqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@userid", user.userid);
			cmd.Parameters.AddWithValue("@pwd", pwd);
			if (cmd.ExecuteNonQuery() < 0) changed = false;
			conn.Close();
			return changed;
		}

		public static bool createuser(string name,string username,string password,string description)
		{
			bool created = true;
            UInt32 userid=0;//Used 0 here creating a new user will never result in a 0 because I create the superuser with the id=0 myself and don't let it deleted. So...
			try
			{
				#region Inserting record in the database
				if (conn.State == ConnectionState.Closed) conn.Open();
				string query = "INSERT INTO user (name,username,password,description) VALUES(@name,@username,@password,@description)";
				MySqlCommand cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@name", name);
				cmd.Parameters.AddWithValue("@username", username);
				cmd.Parameters.AddWithValue("@password", password);
				cmd.Parameters.AddWithValue("@description", description);
				if (cmd.ExecuteNonQuery() < 0) throw new Exception();
				conn.Close(); 
				#endregion

				#region Getting the id of newly inserted user
				if (conn.State == ConnectionState.Closed) conn.Open();
				query = "SELECT id FROM user WHERE username=@username";
				cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@username", username);
				MySqlDataReader rdr = cmd.ExecuteReader();
				if (rdr.Read()) userid = UInt32.Parse(rdr["id"].ToString());
				else throw new Exception();
				conn.Close(); 
				#endregion

				#region Creating folder for user on the remote site
				string createpath = user.ftpurl + "user_" + userid;
				FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(createpath);
				request.Credentials = new NetworkCredential(user.ftpusername, user.ftppassword);
				request.KeepAlive = true;
				request.Method = WebRequestMethods.Ftp.MakeDirectory;
				request.GetResponse(); 
				#endregion
			}
			catch
			{
				created = false;
                #region Deleting record from the database
                if (userid != 0)
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    string query = "DELETE FROM user WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", userid);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                #endregion
            }
			return created;
		}

		public static Dictionary<string,UInt32> listallusers()
		{
			Dictionary<string,UInt32> users = new Dictionary<string,UInt32>();
			try
			{
				if (conn.State == ConnectionState.Closed) conn.Open();
				string query = "SELECT username,id FROM user";
				MySqlCommand cmd = new MySqlCommand(query, conn);
				MySqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					string uname=rdr["username"].ToString();
					UInt32 id=UInt32.Parse(rdr["id"].ToString());
					users.Add(uname, id);
				}
				conn.Close();
			}
			catch
			{
				//do nothing
			}
			return users;
		}

        public static Dictionary<string, UInt32> listnormalusers()
        {
            Dictionary<string, UInt32> users = new Dictionary<string, UInt32>();
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = "SELECT username,id FROM user WHERE id<>0";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string uname = rdr["username"].ToString();
                    UInt32 id = UInt32.Parse(rdr["id"].ToString());
                    users.Add(uname, id);
                }
                conn.Close();
            }
            catch
            {
                //do nothing
            }
            return users;
        }

		public static string[] getuserdetails(UInt32 id)
		{
			string[] details = new string[4];
			try
			{
				if (conn.State == ConnectionState.Closed) conn.Open();
				string query = "SELECT * FROM user WHERE id=@id";
				MySqlCommand cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@id", id);
				MySqlDataReader rdr = cmd.ExecuteReader();
				if (rdr.Read())
				{
					details[0] = rdr["name"].ToString();
					details[1] = rdr["username"].ToString();
					details[2] = rdr["password"].ToString();
					details[3] = rdr["description"].ToString();
				}
				conn.Close();
			}
			catch
			{
				//do nothing
			}
			return details;
		}

		public static bool edituser(UInt32 id,string name, string username, string password, string description)
		{
			bool edited = true;
			try
			{
				if (conn.State == ConnectionState.Closed) conn.Open();
				string query = "UPDATE user SET name=@name,username=@username,password=@password,description=@description WHERE id=@id";
				MySqlCommand cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.Parameters.AddWithValue("@name", name);
				cmd.Parameters.AddWithValue("@username", username);
				cmd.Parameters.AddWithValue("@password", password);
				cmd.Parameters.AddWithValue("@description", description);
				if (cmd.ExecuteNonQuery() < 0) throw new Exception();
				conn.Close();
			}
			catch
			{
				edited = false;
			}
			return edited;
		}

        public static bool deleteuser(UInt32 id)
        {
            bool deleted = true;
            try
            {
                #region Deleting record from the database
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = "DELETE FROM user WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                if (cmd.ExecuteNonQuery() < 0) throw new Exception();
                conn.Close();
                #endregion

                #region Delete folder of the user on the remote site
                string deletepath = user.ftpurl + "user_" + id;
                new DeleteFTPDirectory().DeleteDirectoryHierarcy(deletepath);
                #endregion
            }
            catch
            {
                deleted = false;
            }
            return deleted;
        }

        public static bool updateaudio(ulong id, string title,string description)
        {
            bool updated = true;
            try
            {
                if (conn.State==ConnectionState.Closed) conn.Open();
                string query = "UPDATE audio SET title=@title,description=@description WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);
                if (cmd.ExecuteNonQuery() < 0) updated = false;
                conn.Close();
            }
            catch
            {
                updated = false;
            }
            return updated;
        }

        public static string getaudiofilename(ulong audioid)
        {
            string audiofilename = string.Empty;
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = "SELECT name FROM audio WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", audioid);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    audiofilename = rdr["name"].ToString();
                }
                conn.Close();
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("The connection to the database could not be made. Please check your internet connection." + Environment.NewLine + "The application will now exit.");
                System.Windows.Forms.Application.Exit();
            }
            return audiofilename;
        }
		#endregion
	}
}
