<?php

session_start();

error_reporting(0);

include("config.php");
include("imageoperations.php");

if (isset($_POST['command']))
{
	$cmd=$_POST['command'];
	switch($cmd)
	{
		case 'auth':
			authenticate();
			break;
		case 'checkauth':
			checkauthentication();
			break;
		case 'userinfo':
			getuserinformation();
			break;		
		case 'getprivilages':
			getprivilages();
			break;
		case 'getquota':
			getquota();
			break;
		case 'getitems':
			getitems($_POST['playlistid']);
			break;
		case 'uploaditemfile':
			uploaditemfile($_FILES['file'],$_POST['playlistid']);
			break;
		case 'createitementry':
			createitementry($_POST['name'],$_POST['title'],$_POST['description'],$_POST['type'],$_POST['playlistid']);
			break;
		case 'deleteitem':
			deleteitem($_POST['itemid']);
			break;		
		case 'getplaylistitems':
			getplaylistitems();
			break;
		case 'uploadplaylistfile':
			uploadplaylistfile($_POST['playlistid'],$_FILES['file']);
			break;
		case 'createplaylistentry':
			createplaylistentry($_POST['name'],$_POST['description'],$_POST['thumbname']);
			break;
		case 'deleteplaylist':
			deleteplaylist($_POST['playlistid']);
			break;
		case 'editplaylistentry':
			editplaylistentry($_POST['id'],$_POST['name'],$_POST['description'],$_POST['thumbname']);
			break;
		case 'getuseritems':
			getuseritems();
			break;
		case 'uploaduserfile':
			uploaduserfile($_POST['userid'],$_FILES['file']);
			break;
		case 'createuserentry':
			createuserentry($_POST['name'],$_POST['username'],$_POST['password'],$_POST['description'],$_POST['thumbname'],$_POST['privilage_createplaylist'],$_POST['privilage_createaudio'],$_POST['privilage_uploadaudio'],$_POST['privilage_usermanagement'],$_POST['quota_audio'],$_POST['quota_maxaudiosize']);
			break;
		case 'deleteuser':
			deleteuser($_POST['userid']);
			break;		
		case 'edituserentry':
			edituserentry($_POST['id'],$_POST['name'],$_POST['username'],$_POST['password'],$_POST['description'],$_POST['thumbname'],$_POST['privilage_createplaylist'],$_POST['privilage_createaudio'],$_POST['privilage_uploadaudio'],$_POST['privilage_usermanagement'],$_POST['quota_audio'],$_POST['quota_maxaudiosize']);
			break;
		case 'logout':
			logout();
			break;
	}
}


//command protocol functions

function authenticate()
{
	global $mysqli;
	$cmd='auth';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isset($_POST['username']) && isset($_POST['password']))
		{
			/*$username=mysql_real_escape_string($_POST['username']);
			$password=mysql_real_escape_string($_POST['password']);
			if ($username==FALSE) throw new Exception(geterrormsg(103),103);
			if ($password==FALSE) throw new Exception(geterrormsg(103),103);*/
			$username=$_POST['username'];
			$password=$_POST['password'];
			$query="SELECT id FROM user WHERE username='{$username}' AND password='{$password}';";
			$result=$mysqli->query($query);
			if ($tuple=$result->fetch_array()) 
			{
				$_SESSION['valid']=TRUE;
				$_SESSION['userid']=$tuple['id'];
				//$_SESSION['username']=$username;
				$data=array("valid"=>TRUE);
			}
			else $data=array("valid"=>FALSE);
			output($cmd,$success,$data);
		}
		else throw new Exception(geterrormsg(102),102);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}
}

function checkauthentication()
{
	$cmd='checkauth';
	$success=TRUE;
	$data=NULL;
	if (isauthenticated()) $data=array("authenticated"=>TRUE);
	else $data=array("authenticated"=>FALSE);
	output($cmd,$success,$data);
}

function getuserinformation()
{
	global $mysqli;
	global $root;
	$cmd='userinfo';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{
			$query="SELECT id,name,username,avatar,description FROM user WHERE id={$_SESSION['userid']};";
			if ($result=$mysqli->query($query))
			{
			  if ($tuple=$result->fetch_array()) 
			  {				  
				  $id=$tuple["id"];
				  $username=$tuple["username"];
				  $name=$tuple["name"];
				  $avatarurl=$root."audiogallery/user_{$_SESSION['userid']}/".$tuple["avatar"];
				  $description=$tuple["description"];
				  $data=array(
				  	"id"=>$id,
				  	"username"=>$username,
					"name"=>$name,
					"avatar"=>$avatarurl,
					"description"=>$description
				  );
			  }
			  else throw new Exception(geterrormsg(106),106);
			}
			else throw new Exception(geterrormsg(102),102);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}
}

function getitems($playlistid)
{
	global $mysqli;
	global $root;
	$cmd='getitems';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{
			$query="SELECT id,name,title,description,type FROM audio WHERE playlist_id={$playlistid};";
			if ($result=$mysqli->query($query))
			{
			  $data=array();
			  while ($tuple=$result->fetch_array()) 
			  {				  
				  $id=$tuple["id"];
				  $type=$tuple["type"];
				  if ($type=='file') $url=$root."audiogallery/user_{$_SESSION['userid']}/playlist_{$playlistid}/audios/".$tuple["name"];
				  else $url=$tuple["name"];
				  $title=$tuple["title"];
				  $description=$tuple["description"];
				  $item=array(
				  	"id"=>$id,
					"url"=>$url,
					"title"=>$title,
					"description"=>$description
				  );
				  $data[]=$item;
			  }
			}
			else throw new Exception(geterrormsg(102),102);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}	
}

function getprivilages()
{
	global $mysqli;
	global $root;
	$cmd='getprivilages';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{
			$query="SELECT * FROM privilage WHERE user_id={$_SESSION['userid']};";
			if ($result=$mysqli->query($query))
			{
			  while ($tuple=$result->fetch_array())
			  {				  
				  $createplaylist=$tuple["createplaylist"];
				  $createaudio=$tuple["createaudio"];
				  $usermanagement=$tuple["usermanagement"];
				  $uploadaudio=$tuple["uploadaudio"];
				  $data=array(
				  	"createplaylist"=>$createplaylist,
				  	"createaudio"=>$createaudio,
					"usermanagement"=>$usermanagement,
					"uploadaudio"=>$uploadaudio
				  );
			  }
			}
			else throw new Exception(geterrormsg(102),102);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}	
}

function getquota()
{
	global $mysqli;
	global $root;
	$cmd='getquota';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{
			$query="SELECT * FROM quota WHERE user_id={$_SESSION['userid']};";
			if ($result=$mysqli->query($query))
			{
			  while ($tuple=$result->fetch_array())
			  {				  
				  $audio=$tuple["audio"];
				  $maxaudiosize=$tuple["maxaudiosize"];
				  $data=array(
				  	"audio"=>$audio,
					"maxaudiosize"=>$maxaudiosize
				  );
			  }
			}
			else throw new Exception(geterrormsg(102),102);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}	
}

function uploaditemfile($file,$playlistid)
{
	global $filesystemroot;
	$cmd='uploaditemfile';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{
			if (checkprivilage('createaudio'))
			{
				$directory=$filesystemroot."audiogallery/user_{$_SESSION['userid']}/playlist_{$playlistid}/audios/";
				if (checkprivilage('uploadaudio'))
				{
					if (checkquota('audio',null) && checkquota('maxaudiosize',$file["size"]))
					{
						if ($file["error"] > 0) throw new Exception(geterrormsg(106),106);
						else
						{									
							if (file_exists($directory.$file["name"])) throw new Exception(geterrormsg(108),108);
							else
							{
								if (move_uploaded_file($file["tmp_name"],$directory.$file["name"]))
								$data=array("fileuploaded"=>TRUE);
								else throw new Exception(geterrormsg(106),106);										
							}
						}
					}
					else throw new Exception(geterrormsg(105),105);
				}
				else throw new Exception(geterrormsg(104),104);
			}
			else throw new Exception(geterrormsg(104),104);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch(Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}
}

function createitementry($name,$title,$description,$type,$playlistid)
{
	global $mysqli;
	global $filesystemroot;
	$cmd='createitementry';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{
			if (checkprivilage('createaudio'))
			{
				$query="INSERT INTO audio (playlist_id,name,title,description,type) VALUES ({$playlistid},'{$name}','{$title}','{$description}','{$type}')";
				if (!($mysqli->query($query))) throw new Exception(geterrormsg(102),102);
			}
			else throw new Exception(geterrormsg(104),104);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}
}

function deleteitem($itemid)
{
	global $mysqli;
	global $filesystemroot;
	$cmd='deleteitem';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{
			$query="SELECT name,type,playlist_id FROM audio WHERE id={$itemid};";
			if ($result=$mysqli->query($query))
			{
				if ($tuple=$result->fetch_array())
				{
					if ($tuple['type']=="file")
					{
						$item=$filesystemroot."audiogallery/user_{$_SESSION['userid']}/playlist_{$tuple['playlist_id']}/audios/{$tuple['name']}";
						deletefile($item);
					}
				}
			}
			$query="DELETE FROM audio WHERE id={$itemid};";
			if (!($result=$mysqli->query($query))) throw new Exception(geterrormsg(109),109);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}	
}

function getplaylistitems()
{
	global $mysqli;
	global $root;
	$cmd='getplaylistitems';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{
			$query="SELECT id,name,description,thumb FROM playlist WHERE user_id={$_SESSION['userid']};";
			if ($result=$mysqli->query($query))
			{
			  $data=array();
			  while ($tuple=$result->fetch_array()) 
			  {				  
				  $id=$tuple["id"];
				  $thumburl=$root."audiogallery/user_{$_SESSION['userid']}/playlist_{$id}/".$tuple["thumb"];
				  $name=$tuple["name"];
				  $description=$tuple["description"];
				  $item=array(
				  	"id"=>$id,
					"thumburl"=>$thumburl,
					"name"=>$name,
					"description"=>$description
				  );
				  $data[]=$item;
			  }
			}
			else throw new Exception(geterrormsg(102),102);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}	
}

function uploadplaylistfile($playlistid,$file)
{
	global $mysqli;
	global $filesystemroot;
	$cmd='uploadplaylistfile';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{
			if (checkprivilage('createplaylist'))
			{				
				$playlistidtype=$playlistid;
				$playlistid=$playlistid=="auto_generate"?getlastcreatedplaylistid()+1:$playlistid;
				$directory=$filesystemroot."audiogallery/user_{$_SESSION['userid']}/playlist_{$playlistid}/";
				$audiosdirectory=$directory."audios/";
				mkdir($directory);
				mkdir($audiosdirectory);
				if ($file["error"] > 0) throw new Exception(geterrormsg(106),106);
				else
				{									
					if (file_exists($directory.$file["name"])) throw new Exception(geterrormsg(108),108);
					else
					{
						if (move_uploaded_file($file["tmp_name"],$directory.$file["name"]))
						{
							$data=array("fileuploaded"=>TRUE);
							//resizing the image to thumb size
							$sourcefile=$directory.$file["name"];
							generateandsavethumb($sourcefile,$sourcefile);
							if ($playlistidtype!="auto_generate")
							{
								$query="SELECT thumb FROM playlist WHERE id={$playlistid}";
								if ($result=$mysqli->query($query))
								{
									if ($tuple=$result->fetch_array())
									{
										$playlistthumb=$filesystemroot."audiogallery/user_{$_SESSION['userid']}/playlist_{$playlistid}/".$tuple['thumb'];
										deletefile($playlistthumb);
									}
								}
							}
						}
						else throw new Exception(geterrormsg(106),106);										
					}
				}
			}
			else throw new Exception(geterrormsg(104),104);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch(Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}
}

function createplaylistentry($name,$description,$thumbname)
{
	global $mysqli;
	$cmd='createplaylistentry';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{
			if (checkprivilage('createplaylist'))
			{
				//write proper code that starts transaction and then commits
			$query="INSERT INTO playlist (name,description,thumb,user_id) VALUES ('{$name}','{$description}','{$thumbname}',{$_SESSION['userid']})";
			if (!($mysqli->query($query))) throw new Exception(geterrormsg(102),102);
			}
			else throw new Exception(geterrormsg(104),104);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}
}

function deleteplaylist($playlistid)
{
	global $mysqli;
	global $filesystemroot;
	$cmd='deleteplaylist';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{
			$directory=$filesystemroot."audiogallery/user_{$_SESSION['userid']}/playlist_{$playlistid}";
			removedirectory($directory);
			$query="DELETE FROM playlist WHERE id={$playlistid};";
			if (!($result=$mysqli->query($query))) throw new Exception(geterrormsg(109),109);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}	
}

function editplaylistentry($id,$name,$description,$thumbname)
{
	global $mysqli;
	$cmd='editplaylistentry';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{			
			if (checkprivilage('createplaylist'))
			{
				//write proper code that starts transaction and then commits			
				$query="UPDATE playlist SET name='{$name}',description='{$description}'";
				if ($thumbname!="null") $query=$query.",thumb='{$thumbname}'";
				$query=$query." WHERE id={$id}";
				if (!($mysqli->query($query))) throw new Exception(geterrormsg(102),102);
			}
			else throw new Exception(geterrormsg(104),104);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}
}

function getuseritems()
{
	global $mysqli;
	global $root;
	$cmd='getuseritems';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{
			if (checkprivilage('usermanagement'))
			{
				$query="SELECT * FROM ((user JOIN privilage ON user.id=privilage.user_id) JOIN quota ON user.id=quota.user_id) WHERE user.id<>{$_SESSION['userid']};";
				if ($result=$mysqli->query($query))
				{
				  $data=array();
				  while ($tuple=$result->fetch_array()) 
				  {				  
					  $id=$tuple["id"];
					  $username=$tuple["username"];
					  $name=$tuple["name"];
					  $description=$tuple["description"];
					  $avatarurl=$root."audiogallery/user_{$tuple['id']}/".$tuple["avatar"];
					  $privilage_createplaylist=$tuple["createplaylist"];
					  $privilage_createaudio=$tuple["createaudio"];
					  $privilage_uploadaudio=$tuple["uploadaudio"];
					  $privilage_usermanagement=$tuple["usermanagement"];
					  $quota_audio=$tuple["audio"];
					  $quota_maxaudiosize=$tuple["maxaudiosize"];			  
					  $item=array(
						"id"=>$id,
						"username"=>$username,
						"name"=>$name,
						"description"=>$description,
						"avatarurl"=>$avatarurl,
						"privilage_createplaylist"=>$privilage_createplaylist,
						"privilage_createaudio"=>$privilage_createaudio,
						"privilage_uploadaudio"=>$privilage_uploadaudio,
						"privilage_usermanagement"=>$privilage_usermanagement,
						"quota_audio"=>$quota_audio,
						"quota_maxaudiosize"=>$quota_maxaudiosize
					  );
					  $data[]=$item;
				  }
				}
				else throw new Exception(geterrormsg(102),102);
			}
			else throw new Exception(geterrormsg(104),104);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}	
}

function uploaduserfile($userid,$file)
{
	global $mysqli;
	global $filesystemroot;
	$cmd='uploaduserfile';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{
			$useridtype=$userid;
			$userid=$userid=="auto_generate"?getlastcreateduserid()+1:$userid;
			$directory=$filesystemroot."audiogallery/user_{$userid}/";
			mkdir($directory);
			if ($file["error"] > 0) throw new Exception(geterrormsg(106),106);
			else
			{									
				if (file_exists($directory.$file["name"])) throw new Exception(geterrormsg(108),108);
				else
				{
					if (move_uploaded_file($file["tmp_name"],$directory.$file["name"]))
					{
						$data=array("fileuploaded"=>TRUE);
						//resizing the image to thumb size
						$sourcefile=$directory.$file["name"];
						generateandsavethumb($sourcefile,$sourcefile);
						if ($useridtype!="auto_generate")
						{
							$query="SELECT avatar FROM user WHERE id={$userid};";
							if ($result=$mysqli->query($query))
							{
								if ($tuple=$result->fetch_array())
								{
									$userthumb=$filesystemroot."audiogallery/user_{$userid}/".$tuple['avatar'];
									deletefile($userthumb);
								}
							}
						}
					}
					else throw new Exception(geterrormsg(106),106);										
				}
			}
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch(Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}
}

function createuserentry($name,$username,$password,$description,$thumbname,$privilage_createplaylist,$privilage_createaudio,$privilage_uploadaudio,$privilage_usermanagement,$quota_audio,$quota_maxaudiosize)
{
	global $mysqli;
	$cmd='createuserentry';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{			
			//write proper code that starts transaction and then commits
			$query="INSERT INTO user (username,password,name,description,avatar,active) VALUES ('{$username}','{$password}','{$name}','{$description}','{$thumbname}',1)";
			if (!($mysqli->query($query))) throw new Exception(geterrormsg(102),102);
			
			$query="SELECT id FROM user WHERE username='{$username}'";
			$result=$mysqli->query($query);
			$tuple=$result->fetch_array();
			$userid=$tuple['id'];
			
			$query="INSERT INTO privilage (user_id,usermanagement,createplaylist,createaudio,uploadaudio) VALUES ({$userid},{$privilage_usermanagement},{$privilage_createplaylist},{$privilage_createaudio},{$privilage_uploadaudio})";
			if (!($mysqli->query($query))) throw new Exception(geterrormsg(102),102);
			
			$query="INSERT INTO quota (user_id,audio,maxaudiosize) VALUES ({$userid},{$quota_audio},{$quota_maxaudiosize})";
			if (!($mysqli->query($query))) throw new Exception(geterrormsg(102),102);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}
}

function deleteuser($userid)
{
	global $mysqli;
	global $filesystemroot;
	$cmd='deleteuser';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{
			$directory=$filesystemroot."audiogallery/user_{$userid}";
			removedirectory($directory);
			$query="DELETE FROM user WHERE id={$userid};";
			if (!($result=$mysqli->query($query))) throw new Exception(geterrormsg(109),109);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}	
}

function edituserentry($id,$name,$username,$password,$description,$thumbname,$privilage_createplaylist,$privilage_createaudio,$privilage_uploadaudio,$privilage_usermanagement,$quota_audio,$quota_maxaudiosize)
{
	global $mysqli;
	$cmd='edituserentry';
	$success=TRUE;
	$data=NULL;
	try
	{
		if (isauthenticated())
		{			
			//write proper code that starts transaction and then commits			
			$query="UPDATE user SET username='{$username}',name='{$name}',description='{$description}',active=1";
			if ($password!="null") $query=$query.",password='{$password}'";//write proper code here to send type safe data as real null and not as string null
			if ($thumbname!="null") $query=$query.",avatar='{$thumbname}'";
			$query=$query." WHERE id={$id}";
			if (!($mysqli->query($query))) throw new Exception(geterrormsg(102),102);
			
			$query="UPDATE privilage SET usermanagement={$privilage_usermanagement},createplaylist={$privilage_createplaylist},createaudio={$privilage_createaudio},uploadaudio={$privilage_uploadaudio} WHERE user_id={$id}";
			if (!($mysqli->query($query))) throw new Exception(geterrormsg(102),102);
			
			$query="UPDATE quota SET audio={$quota_audio},maxaudiosize={$quota_maxaudiosize} WHERE user_id={$id}";
			if (!($mysqli->query($query))) throw new Exception(geterrormsg(102),102);
		}
		else throw new Exception(geterrormsg(101),101);
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}
}

function logout()
{
	$cmd='logout';
	$success=TRUE;
	$data=NULL;
	try
	{
		session_destroy();
		output($cmd,$success,$data);
	}
	catch (Exception $e)
	{
		$success=FALSE;
		$data=array("errorcode"=>$e->getCode(),"errormsg"=>$e->getMessage());
		output($cmd,$success,$data);
	}
}

//helper functions

function isauthenticated()
{
	return isset($_SESSION['valid']);
}

function checkprivilage($privilage)
{
	global $mysqli;
	switch($privilage)
	{
		case 'createplaylist':
			$query="SELECT createplaylist FROM privilage WHERE user_id={$_SESSION['userid']};";
			if ($result=$mysqli->query($query))
			{
				if($tuple=$result->fetch_array())
			  	{
				 	 if ($tuple['createplaylist']==1) return true;
					 else return false;
			  	}
			}
			//add correct code here write throw statements here which will be catched by the functions calling it and the resultant exception will be sent to client side bubbling by bubbling
			break;
		case 'createaudio':
			$query="SELECT createaudio FROM privilage WHERE user_id={$_SESSION['userid']};";
			if ($result=$mysqli->query($query))
			{
				if($tuple=$result->fetch_array())
			  	{
				 	 if ($tuple['createaudio']==1) return true;
					 else return false;
			  	}
			}
			break;
		case 'uploadaudio':
			$query="SELECT uploadaudio FROM privilage WHERE user_id={$_SESSION['userid']};";
			if ($result=$mysqli->query($query))
			{
				if($tuple=$result->fetch_array())
			  	{
				 	 if ($tuple['uploadaudio']==1) return true;
					 else return false;
			  	}
			}
			break;
		case 'usermanagement':
			$query="SELECT usermanagement FROM privilage WHERE user_id={$_SESSION['userid']};";
			if ($result=$mysqli->query($query))
			{
				if($tuple=$result->fetch_array())
			  	{
				 	 if ($tuple['usermanagement']==1) return true;
					 else return false;
			  	}
			}
			break;
	}
}

function checkquota($quota,$value)
{
	global $mysqli;
	switch($quota)
	{
		case 'audio':
			$audioquota;
			$query="SELECT audio FROM quota WHERE user_id={$_SESSION['userid']};";
			$result=$mysqli->query($query);
			$tuple=$result->fetch_array();
			$audioquota=$tuple['audio'];
			$query="SELECT count(audio.id) AS numaudios FROM ((audio JOIN playlist ON audio.playlist_id=playlist.id) JOIN user ON playlist.user_id=user.id) WHERE user.id={$_SESSION['userid']};";
			$result=$mysqli->query($query);
			$tuple=$result->fetch_array();
			if ($tuple['numaudios']>=$audioquota) return false;
			else return true;
			break;
		case 'maxaudiosize':
			$maxaudiosize;
			$query="SELECT maxaudiosize FROM quota WHERE user_id={$_SESSION['userid']};";
			$result=$mysqli->query($query);
			$tuple=$result->fetch_array();
			$maxaudiosize=$tuple['maxaudiosize'];
			if ($value>$maxaudiosize) return false;
			else return true;
			break;
	}
}

function generateandsavethumb($imagelocation,$savelocation)
{
	$image = new SimpleImage();
   	$image->load($imagelocation);
   	$image->resize(164,123);
   	$image->save($savelocation);
}

function deletefile($file)
{
	unlink($file);	
}

function getlastcreatedplaylistid()
{
	global $mysqli;	
	$query="SHOW TABLE STATUS LIKE 'playlist';";
	$result=$mysqli->query($query);
	$tuple=$result->fetch_array();
	return ($tuple['Auto_increment']-1);
}

function getlastcreateduserid()
{
	global $mysqli;	
	$query="SHOW TABLE STATUS LIKE 'user';";
	$result=$mysqli->query($query);
	$tuple=$result->fetch_array();
	return ($tuple['Auto_increment']-1);
}

function removedirectory($directory, $empty=FALSE)
{
	if(substr($directory,-1) == '/')
	{
		$directory = substr($directory,0,-1);
	}
	if(!file_exists($directory) || !is_dir($directory))
	{
		return FALSE;
	}elseif(is_readable($directory))
	{
		$handle = opendir($directory);
		while (FALSE !== ($item = readdir($handle)))
		{
			if($item != '.' && $item != '..')
			{
				$path = $directory.'/'.$item;
				if(is_dir($path)) 
				{
					removedirectory($path);
				}else{
					unlink($path);
				}
			}
		}
		closedir($handle);
		if($empty == FALSE)
		{
			if(!rmdir($directory))
			{
				return FALSE;
			}
		}
	}
	return TRUE;
}

function geterrormsg($code)
{
	switch ($code)
	{
		case 101:
			return "You are not authenticated to perform that action";
			break;
		case 102:
			return "There was some problem accessing the database";
			break;
		case 103:
			return "Invalid input";
			break;
		case 104:
			return "You do not have the required privilages to perform that action";
			break;
		case 105:
			return "The usage quote assigned to you for this action has exhausted";
			break;
		case 106:
			return "Internal error";
			break;
		case 107:
			return "Data not ready. Please reload the page";
			break;
		case 108:
			return "Invalid input. A file with the similar name already exists";
			break;
		case 109:
			return "Delete operation was unsuccessful.";
			break;
	}
}

function output($cmd,$success,$data)
{
	$arr=array("cmd"=>$cmd,"success"=>$success,"data"=>$data);
	$output=json_encode($arr);
	echo $output;
}
?>