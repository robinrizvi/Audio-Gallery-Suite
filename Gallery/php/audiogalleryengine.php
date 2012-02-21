<?php
/*
 *  --------------------------------------------------------------------------------------------------------------------
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

	include("config.php");
  //for getting all the playlist and returning them
  if (isset($_POST['playlistselect']))

  {

	$query="SELECT * FROM playlist";

	$result=$mysqli->query($query);

	$responsehtml='<option value="0">All</option>';

	while($tuple=$result->fetch_array())

	{

		$responsehtml.="<option value={$tuple['id']}>{$tuple['name']}</option>";

	}

	$result->close();

	echo $responsehtml;

	exit();

  }
  
  
  
  if (isset($_POST['playlistid']) && isset($_POST['searchtext']))
  {
	  $playlistid=$_POST['playlistid'];
	  $searchtext=$_POST['searchtext'];
	  
	  if ($searchtext=="") $searchtext="%"; else $searchtext="%".$searchtext."%";
	
	  if ($playlistid==0) $query="SELECT audio.name AS name,audio.title AS title,audio.playlist_id AS playlistid,playlist.user_id AS userid FROM (audio JOIN playlist ON audio.playlist_id=playlist.id) WHERE audio.title LIKE '{$searchtext}'";
	
	  else $query="SELECT audio.name AS name,audio.title AS title,audio.playlist_id AS playlistid,playlist.user_id AS userid FROM (audio JOIN playlist ON audio.playlist_id=playlist.id) WHERE audio.playlist_id={$playlistid} AND audio.title LIKE '{$searchtext}'";
	
	  $result=$mysqli->query($query);
	
	  $html="<ul>";
	  
	  while($tuple=$result->fetch_array())
	
	  {
	
		$userid=$tuple['userid'];
	
		$playlistid=$tuple['playlistid'];
	
		$name=$tuple['name'];
	
		$title=$tuple['title'];
	
		$audiourl="{$root}audiogallery/user_{$userid}/playlist_{$playlistid}/audios/{$name}";
	
		$html.="<li class=\"audiotrack\" url=\"{$audiourl}\">{$title}</li>";	
	  }
	  
	  $html.="</ul>";

	  echo $html;
  }

	
?>