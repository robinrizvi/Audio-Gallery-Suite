-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.5.13-log


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema audiogallery
--

CREATE DATABASE IF NOT EXISTS audiogallery;
USE audiogallery;

--
-- Definition of table `audio`
--

DROP TABLE IF EXISTS `audio`;
CREATE TABLE `audio` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(256) NOT NULL,
  `title` varchar(100) NOT NULL,
  `description` varchar(500) DEFAULT NULL,
  `playlist_id` bigint(20) unsigned NOT NULL,
  `type` enum('url','file') NOT NULL DEFAULT 'file',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uniqueindex` (`name`,`playlist_id`),
  UNIQUE KEY `unique_title_playlist` (`title`,`playlist_id`) USING BTREE,
  KEY `audiotoplaylist` (`playlist_id`),
  CONSTRAINT `audiotoplaylist` FOREIGN KEY (`playlist_id`) REFERENCES `playlist` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `audio`
--

/*!40000 ALTER TABLE `audio` DISABLE KEYS */;
/*!40000 ALTER TABLE `audio` ENABLE KEYS */;


--
-- Definition of table `playlist`
--

DROP TABLE IF EXISTS `playlist`;
CREATE TABLE `playlist` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `description` varchar(500) DEFAULT NULL,
  `user_id` smallint(5) unsigned NOT NULL,
  `thumb` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `uniqueindex` (`name`,`user_id`),
  KEY `FK_playlist_user` (`user_id`),
  CONSTRAINT `FK_playlist_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `playlist`
--

/*!40000 ALTER TABLE `playlist` DISABLE KEYS */;
/*!40000 ALTER TABLE `playlist` ENABLE KEYS */;


--
-- Definition of table `privilage`
--

DROP TABLE IF EXISTS `privilage`;
CREATE TABLE `privilage` (
  `user_id` smallint(5) unsigned NOT NULL,
  `usermanagement` tinyint(1) DEFAULT '1',
  `uploadaudio` tinyint(1) DEFAULT '1',
  `createaudio` tinyint(1) DEFAULT '1',
  `createplaylist` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`user_id`) USING BTREE,
  CONSTRAINT `FK_privilage_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `privilage`
--

/*!40000 ALTER TABLE `privilage` DISABLE KEYS */;
INSERT INTO `privilage` (`user_id`,`usermanagement`,`uploadaudio`,`createaudio`,`createplaylist`) VALUES (0,1,1,1,1);
 
 

--
-- Definition of table `quota`
--

DROP TABLE IF EXISTS `quota`;
CREATE TABLE `quota` (
  `user_id` smallint(5) unsigned NOT NULL,
  `audio` int(10) unsigned NOT NULL DEFAULT '10000',
  `maxaudiosize` bigint(20) unsigned NOT NULL DEFAULT '1073741824',
  PRIMARY KEY (`user_id`),
  CONSTRAINT `quotaofuser` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `quota`
--

/*!40000 ALTER TABLE `quota` DISABLE KEYS */;
INSERT INTO `quota` (`user_id`,`audio`,`maxaudiosize`) VALUES (0,10,20000000);
 
 

--
-- Definition of table `settings`
--

DROP TABLE IF EXISTS `settings`;
CREATE TABLE `settings` (
  `ftpurl` varchar(100) NOT NULL,
  `ftpusername` varchar(100) NOT NULL,
  `ftppassword` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `settings`
--

/*!40000 ALTER TABLE `settings` DISABLE KEYS */;
INSERT INTO `settings` (`ftpurl`,`ftpusername`,`ftppassword`) VALUES 
 ('ftp://your_ftp_host/','ftp_username','ftp_password');
/*!40000 ALTER TABLE `settings` ENABLE KEYS */;


--
-- Definition of table `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `name` varchar(100) DEFAULT NULL,
  `description` varchar(200) DEFAULT NULL,
  `avatar` varchar(100) NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uniqueindex` (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`id`,`username`,`password`,`name`,`description`,`avatar`,`active`) VALUES 
 (0,'superuser','superuser','Super User','Superuser that creates and manages other users and can also create also create and manage his own playlists and audios','user.png',1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
