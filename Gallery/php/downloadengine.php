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
if (isset($_GET['file']))

{

  $fullfilename=$_GET['file'];

  $filename=basename($fullfilename);
  
  $relativefilename=substr($fullfilename,strlen($host));

  header('Content-Type: application/octet-stream');

  header("Content-Disposition: attachment; filename=\"{$filename}\""); 

  header('Content-Transfer-Encoding: binary');

  // load the file to send:

  readfile('../'.$relativefilename);

}

?>