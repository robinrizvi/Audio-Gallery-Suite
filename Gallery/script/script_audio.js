/*
 *  --------------------------------------------------------------------------------------------------------------------
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

$(document).ready(function(){						   
	//for the website preloader
	$("body").queryLoader2({
	backgroundColor:"#000",
	barColor:"#fff",
	deepSearch:true,
	percentage:true
	});
					
	//setting the correct dimensions according to resolution or browser size
	setdimensions();
	
	//initializing inital configuration for the addthis share button
	addthis.button
	(
	 	'#addthissharetool',
		{
			ui_click: true,
			ui_open_windows:true,
			services_custom: 
			{
				name: "SindhChatRoom",
				url: "http://www.sindhchatroom.com/bookmarks/add?address=http://sindhyatralive.com&title=SindhYatraLive",
				icon: "http://www.sindhchatroom.com/_graphics/toolbar_logo.png"
			}
		},
		{
			url:"http://robinrizvi.info",
			title:"Visit robinrizvi.info",
			description:"Visit robinrizvi.info"
		}
	);
	
	//declaring global variable to use during ajax calls when the audiolist is updated and jscrollpane modifies audiolist markup. And also calling jscrollpane initialization function
	var jscrollapi;
	var scrollableaudiolist;
	initjscrollpane();
	
	//Global variable for search xmlhttprequest, needed so that I can cancel multiple XHR requests during subsequent key presses.
	var searchxhr;
	
	//Global variables for playlist management
	var Playlist;
	var audioPlaylist;
	
	/*Defines Jplayer Playlist Class*/
	initjplayerplaylistfunctions();
	
	//Recalculating and setting dimensions when browser is resized					   
	$(window).resize(function(){setdimensions();});
	
	//Filling the playlist select box with values via XHR/ajax
	$("#playlistselect").load("php/audiogalleryengine.php",{playlistselect:1},function(){$("#playlistselect").change();$('select').selectmenu({style:'dropdown'});$("#playlistselect-menu").jScrollPane();});
	
	//Event handler for playlistselect change
	$("#playlistselect").change(function(){
		searchaudio();
		$("#currentplaylist span").text($("#playlistselect :selected").text());
	});	
		
	//Initializing the audio search button
	$("#searchbtnimg").click(function(){
		searchaudio();
	});
	
	//Calling search audio function each time when user presses a key
	$("#searchinput").keyup(function(){		
		searchaudio();
	});
	
	
	//addind currenttrackduration data to my player by listening to jplayer's loadedmetadata event. This event occures when a file is played at teh start it is fired
	$("#jquery_jplayer_2").bind($.jPlayer.event.loadedmetadata, function(event) {
		$("#player").data("currenttrackduration",event.jPlayer.status.duration);
	});
	//addind currenttime data to my player by listening to jplayer's timeupdate event. It is similar to HTML5 audio timeupdate event. This methods lags I'll probably change this to like I would read the currenttime from the div that displays the current time in my player.
	$("#jquery_jplayer_2").bind($.jPlayer.event.timeupdate, function(event) {
  		$("#player").data("currenttime",event.jPlayer.status.currentTime);
		$("#player").data("currenttrackduration",event.jPlayer.status.duration);
	});
	//adding audioplaying status data to my player and starting the disc
	$("#jquery_jplayer_2").bind($.jPlayer.event.play, function() { 
  		$("#player").data("audioplaying",true);
		startdisk();
	});
	//adding audioplaying status data to my player and stopping the disc
	$("#jquery_jplayer_2").bind($.jPlayer.event.pause, function() {
  		$("#player").data("audioplaying",false);
		stopdisk();
	});	
	
	//Attaching handlers for keypresses and calling appropriate methods for taking the steps required
	initplayerkeyboardsupport();
	
	//Create gray to blue hover effect for toolbar buttons
	$("#toolbar a").hover(function(){
			$(this).removeClass('button gray medium').addClass('button blue medium');
		},
		function(){
			$(this).removeClass('button blue medium').addClass('button gray medium');
		}
	);
	
	//setting checkbox status of the Help page depending on the localstorage value which was set earlier by the user
	if(localStorage.showhelpatstartup=="no")
	{
		$("#popupstartupcheck").checked=true;
		$("#popupstartupcheck").attr('value','tick');
	}
	else
	if(localStorage.showhelpatstartup=="yes")
	{
		$("#popupstartupcheck").checked=true;
		$("#popupstartupcheck").attr('value','notick');
	}
	else
	{
		$("#popupstartupcheck").checked=false;
		$("#popupstartupcheck").attr('value','notick');
	}
	
	//Associating prettyphoto litebox to my toolbar "Help" link
	$("a[rel^='prettyPhoto']").prettyPhoto({social_tools:false,show_title: true,allow_resize: true});
	
	//Using HTML5 localstorage method to store a variable almost indefiinitely to check whether to show popup at startup
	if (localStorage.showhelpatstartup=="yes")$("#helptool").click();
	
	//Attaching event hanfler for the Download button click and setting new download URL when new media has been set
	$("#downloadaudiotool").click(function(){
		var filedownloadpath=encodeURIComponent($("#player").data("currenttrackurl"));
		$("#downloadaudiotool").attr('href','php/downloadengine.php?file='+filedownloadpath);						   
	});
	
	//Attaching event handlers for the Share button
	$('#shareaudiotool').toggle(
	function(){
		$('.sharebar').fadeIn('slow');
		var shareurl=encodeURI($("#player").data("currenttrackurl"));
		$('.sharebar input').attr('value',shareurl).select();
	},
	function(){
		$('.sharebar').fadeOut('slow');
	}
	);
	
	/*$('#addthissharetool').click(function(){
		var shareurl=encodeURI($("#player").data("currenttrackurl"));
		shareupdate('#addthissharetool',shareurl,'testing');
	});*/
/*   $('.message').click(function(){
	  $('.sharebar-trigger').click();
	});*/
});

function shareupdate(url,title)
{
	$('#addthissharetool').remove();
	$('#toolbar').append('<a id="addthissharetool" class="sharebar-trigger button gray medium">SHARE</a>');
	url=encodeURI(url);
	var sindhchatroombookmarkurl=encodeURI("http://www.sindhchatroom.com/bookmarks/add?address="+url+"&title="+title);
	var sindhchatsharevalue={name: "SindhChatRoom",url:sindhchatroombookmarkurl ,icon: 'http://www.sindhchatroom.com/_graphics/toolbar_logo.png'};
	addthis.button(
		'#addthissharetool',
		{
			ui_click: true,
			ui_open_windows:true,
			services_custom: 
			{
				name: "SindhChatRoom",
				url: sindhchatroombookmarkurl,
				icon: "http://www.sindhchatroom.com/_graphics/toolbar_logo.png"
			}
		},
		{
			url:url,
			title:title,
			description:'Visit robinrizvi.info'
		}
	);
	
	//Create gray to blue hover effect for toolbar buttons
	$("#addthissharetool").hover(function(){
			$(this).removeClass('button gray medium').addClass('button blue medium');
		},
		function(){
			$(this).removeClass('button blue medium').addClass('button gray medium');
		}
	);
}

function setdimensions()
{
	var playlistselectboxwidth=$("#playlistselecttoolbar").width()-$("#playlistselecttoolbar label").width()-5-20;//-5 for padding and -20 for extra space
	$("#playlistselecttoolbar select").width(playlistselectboxwidth);
	var inputsearchboxwidth=$("#searchaudiotoolbar").width()-$("#searchaudiotoolbar label").width()-5-21-20;//-5 for padding, -21 for seargmagglass img,-20 for extra space
	$("#searchaudiotoolbar input").width(inputsearchboxwidth);
	var audiolistheight=$(document).height()-$("#playlistselecttoolbar").height()-$("#searchaudiotoolbar").height()-20;
	$("#audiolist").css("margin-top",$("#playlistselecttoolbar").height()+10);
	$("#audiolist").height(audiolistheight);
	window.jscrollapi!=undefined?window.jscrollapi.reinitialise():null;
	var playerwidth=$(document).width()-$("#left").width();
	$("#player").width(playerwidth);
	$("#player").css("bottom",$("#playerstatbar").height());
	var progressbarwidth=$("#player").width()-300;
	$(".jp-progress").width(progressbarwidth);
	var durationtimebarposition=parseInt($(".jp-progress").css("left"))+$(".jp-progress").width()-$(".jp-duration").width();
	$(".jp-duration").css("left",durationtimebarposition);
	$("#toolbar").css("bottom",$("#player").height()+$("#playerstatbar").height());	
	var toolbarwidth=$(document).width()-$("#left").width()-15;
	$("#toolbar").width(toolbarwidth);
	var playerstatbarwidth=$(document).width()-$("#left").width()-5;
	$("#playerstatbar").width(playerstatbarwidth);
	
	// Making the coverart image to the correct size while maintaining its aspect ratio
	var coverartdivwidth=$("#coverart").width();
	var coverartdivheight=$("#right").height()-$("#player").height()-$("#playerstatbar").height()-$("#toolbar").height();
	var coverartimgwidth=$("#coverart img").attr('oriwidth');
	var coverartimgheight=$("#coverart img").attr('oriheight');
	var aspectratio=coverartimgwidth/coverartimgheight;
	if(coverartimgwidth>=coverartdivwidth)
	{
		var newimgw=coverartdivwidth;
		var newimgh=newimgw/aspectratio;
		if (newimgh>coverartdivheight)
		{
			newimgh=coverartdivheight;
			newimgw=newimgh*aspectratio;
		}
		$("#coverart img").width(newimgw);
		$("#coverart img").height(newimgh);
	}
	else
	if (coverartimgheight>=coverartdivheight)
	{
		var newimgh=coverartdivheight;
		var newimgw=newimgh*aspectratio;
		if (newimgw>coverartdivwidth)
		{
			newimgw=coverartdivwidth;
			newimgh=newimgw/aspectratio;
		}
		$("#coverart img").width(newimgw);
		$("#coverart img").height(newimgh);
	}
	//coverart code end
	// Making the coverart rotatingdisc to the correct size and position
	$("#rotatingdisc").width($("#coverart img").width()/1.98);
	$("#rotatingdisc").height($("#rotatingdisc").width());
	var rotatingdiscleftposition=$("#coverart img").position().left+$("#coverart img").width()/2.240896;
	var rotatingdisctopposition=$("#coverart img").position().top+$("#coverart img").height()/4.65753;
	$("#rotatingdisc").css('left',rotatingdiscleftposition);
	$("#rotatingdisc").css('top',rotatingdisctopposition);
	//rotatingdisc code end
	
	//Positioning the sharebar
	var sharebarposition=$("#playerstatbar").height()+$("#player").height()+$("#toolbar").height()+10;
	$('.sharebar').css('bottom',sharebarposition);
	$('.sharebar').width($('#toolbar').width());
	var textboxwidth=$('.sharebar').width()-50;
	$('.sharebar input').css('width',textboxwidth);
	//sharebar positioning end
}

function initjscrollpane()
{
	var settings = {
		showArrows:true,
		maintainPosition: false,
		animateScroll: true
	};
	var pane = $('#audiolist');
	pane.jScrollPane(settings);
	jscrollapi = pane.data('jsp');
	scrollableaudiolist=jscrollapi.getContentPane();
}

function searchaudio()
{
	searchxhrabort();
	$("#audiolist").showLoading();
	window.searchxhr = $.ajax({
		type: "POST",
		url: "php/audiogalleryengine.php",
		data: "playlistid="+$('#playlistselect').val()+"&searchtext="+$('#searchinput').val(),
		success: function(html){
			//Adding the ajax returned audiotracks to the audiolist and reinitializing jscrollpane
		   window.scrollableaudiolist.html(html);
		   window.jscrollapi.reinitialise();
		   $("#audiolist").hideLoading();
		   
		   //Destroy the current jplayer instance and its playlist because new playlist is about to be created
		   //$("#jquery_jplayer_2").jPlayer("destroy");
		   
		   //Storing previous currentplaytime beforing creating a new playlist from the new search items because after creating the new playlist the currentplaytime would automatically get to zero so that I can seek properly if the old element is found in the new search
		   $("#player").data("previouscurrenttime",$("#player").data("currenttime"));
		   //Reinitializing the playlist for jplayer
		   createplaylist();
		   //Checking if the current playing track in the old playlist is also present in the new search list and taking actions accordingly
		   var trackidifany=currenttrackinnewsearch();
		   if (trackidifany===false)
		   {
			   window.audioPlaylist.playlistChange(0);
			   $("#jquery_jplayer_2").jPlayer("stop");
		   }
		   else
		   {
				window.audioPlaylist.playlistChange(trackidifany);
				$("#jquery_jplayer_2").jPlayer("pause",$("#player").data("previouscurrenttime"));   
		   }		   
		   
		   //Adding event hander for the newly loaded audiotracks
		   $(".audiotrack").click(function(){
				var audiotrackindex=$(this).index();
				window.audioPlaylist.playlistChange(audiotrackindex);
			});
		}
	});
}

function currenttrackinnewsearch()
{
	//if ($("#player").data("currenttrackurl");
	var flag=false;
	$(".audiotrack").each(function(){
		if ($("#player").data("currenttrackurl")==$(this).attr('url'))
		{
			flag=$(this).index();			
		}
	});
	return flag;
}

function searchxhrabort()
{
	if (window.searchxhr!=null)
	{
		window.searchxhr.abort();
		$("#audiolist").hideLoading();
	}
}

function initjplayerplaylistfunctions()
{
		Playlist = function(instance, playlist, options) {
		var self = this;

		this.instance = instance; // String: To associate specific HTML with this playlist
		this.playlist = playlist; // Array of Objects: The playlist
		this.options = options; // Object: The jPlayer constructor options for this playlist

		this.current = 0;

		this.cssId = {
			jPlayer: "jquery_jplayer_",
			interface: "jp_interface_",
			playlist: "jp_playlist_"
		};
		this.cssSelector = {};

		$.each(this.cssId, function(entity, id) {
			self.cssSelector[entity] = "#" + id + self.instance;
		});

		if(!this.options.cssSelectorAncestor) {
			this.options.cssSelectorAncestor = this.cssSelector.interface;
		}

		$(this.cssSelector.jPlayer).jPlayer(this.options);

		$(this.cssSelector.interface + " .jp-previous").click(function() {
			self.playlistPrev();
			$(this).blur();
			return false;
		});

		$(this.cssSelector.interface + " .jp-next").click(function() {
			self.playlistNext();
			$(this).blur();
			return false;
		});
	};

	Playlist.prototype = {
		playlistInit: function(autoplay) {
			if(autoplay) {
				this.playlistChange(this.current);
			} else {
				this.playlistConfig(this.current);
			}
		},
		playlistConfig: function(index) {
			$(this.cssSelector.playlist + "_item_" + this.current).removeClass("jp-playlist-current").parent().removeClass("jp-playlist-current");
			$(this.cssSelector.playlist + "_item_" + index).addClass("jp-playlist-current").parent().addClass("jp-playlist-current");
			this.current = index;
			$(this.cssSelector.jPlayer).jPlayer("setMedia", this.playlist[this.current]);
			$("#nowplaying span").text(this.playlist[this.current].name);
			$("#player").data("currenttrackurl",this.playlist[this.current].mp3);
			playlistuiupdate(1,$('#player').data("currenttrackindex"),index);
			$('#player').data("currenttrackindex",index);
			shareupdate($("#player").data("currenttrackurl"),this.playlist[this.current].name);
		},
		playlistChange: function(index) {
			this.playlistConfig(index);
			$(this.cssSelector.jPlayer).jPlayer("play");
		},
		playlistNext: function() {
			var index = (this.current + 1 < this.playlist.length) ? this.current + 1 : 0;
			this.playlistChange(index);
		},
		playlistPrev: function() {
			var index = (this.current - 1 >= 0) ? this.current - 1 : this.playlist.length - 1;
			this.playlistChange(index);
		}
	};	
}

function createplaylist()
{
	var objectarraystring="var myaudioplaylist=[";
	$(".audiotrack").each(function(){
		objectarraystring=objectarraystring+"{name:\""+$(this).text()+"\",mp3:\""+$(this).attr('url')+"\"},";						   
	});
	objectarraystring+="]";
	eval(objectarraystring);
	
	audioPlaylist = new window.Playlist("2",myaudioplaylist, {
	ready: function() {
		audioPlaylist.playlistInit(false); // Parameter is a boolean for autoplay.
	},
	ended: function() {
		audioPlaylist.playlistNext();
	},
	play: function() {
		$(this).jPlayer("pauseOthers");
	},
	swfPath: "flash",
	supplied: "mp3",
	solution: "html, flash",
	wmode: "window",
	preload: "auto"
});
}

function playlistuiupdate(playpause,oldtrackindex,newtrackindex)
{
	if (playpause==1)
	{
		if (typeof oldtrackindex!=undefined) $(".audiotrack").eq(oldtrackindex).removeClass("playing_track");
		var currenttrackdomelement=$(".audiotrack").eq(newtrackindex);
		currenttrackdomelement.addClass("playing_track");
		window.jscrollapi.scrollTo(0,currenttrackdomelement.position().top-$('.jspTrack').height()/2);
	}
}

function initplayerkeyboardsupport()
{
	$(document).keydown(function(event)
	{
		//for seeking
		if (event.shiftKey)
		{
			switch (event.keyCode) 
			{
				case 37: // left arrow
					$("#player").data("currenttime")-1>=0?$("#jquery_jplayer_2").jPlayer("playHead", $("#player").data("currenttime")-1):null;
					if (($("#player").data("currenttime")-1)>=0)
					{
						if ($("#player").data("audioplaying"))
						{
							$("#jquery_jplayer_2").jPlayer("play", $("#player").data("currenttime")-1);
							$("#jquery_jplayer_2").jPlayer("play");
						}
						else
						{
							$("#jquery_jplayer_2").jPlayer("pause", $("#player").data("currenttime")-1);	
						}
					}
					return;
					break;
				case 39: // right arrow
					if (($("#player").data("currenttime")+1)<=$("#player").data("currenttrackduration"))
					{
						if($("#player").data("audioplaying"))
						{
							$("#jquery_jplayer_2").jPlayer("play", $("#player").data("currenttime")+1);
							$("#jquery_jplayer_2").jPlayer("play");
						}
						else
						{
							$("#jquery_jplayer_2").jPlayer("pause", $("#player").data("currenttime")+1);
						}
					}
					return;
					break;				
			}	
		}
		switch (event.keyCode) 
		{
			case 37: // left arrow
			  	window.audioPlaylist!=undefined?window.audioPlaylist.playlistPrev():null;
			  	break;
			case 39: // right arrow
				window.audioPlaylist!=undefined?window.audioPlaylist.playlistNext():null;
				break;
			case 32: // space
				if ($("#player").data("audioplaying")===false || $("#player").data("audioplaying")===undefined)
				{
					$("#jquery_jplayer_2").jPlayer("play");
					$("#player").data("audioplaying",true);
				}
				else
				{
					$("#jquery_jplayer_2").jPlayer("pause");
					$("#player").data("audioplaying",false);	
				}
			  	break;
			case 77: // m
			  	if ($("#player").data("muted")===false || $("#player").data("muted")===undefined)
				{
					$("#jquery_jplayer_2").jPlayer("mute");
					$("#player").data("muted",true);
				}
				else
				{
					$("#jquery_jplayer_2").jPlayer("unmute");
					$("#player").data("muted",false);	
				}
			  	break;
			case 109: // M
			  	if ($("#player").data("muted")===false || $("#player").data("muted")===undefined)
				{
					$("#jquery_jplayer_2").jPlayer("mute");
					$("#player").data("muted",true);
				}
				else
				{
					$("#jquery_jplayer_2").jPlayer("unmute");
					$("#player").data("muted",false);	
				}
			  	break;
	  	}
	});
}

function showpopupchange()
{
	if ($("#popupstartupcheck").attr('value')===undefined || $("#popupstartupcheck").attr('value')=='tick') 
	{
		$("#popupstartupcheck").attr('value','notick');
		$("#popupstartupcheck").checked=false;
		localStorage.showhelpatstartup="yes";
	}
	else 
	{
		$("#popupstartupcheck").attr('value','tick');
		$("#popupstartupcheck").checked=true;
		localStorage.showhelpatstartup="no";
	}
}

function rotatedisk(element,angle) 
{
	stopdisk();
	element.stop().animate({rotate: '+=150deg'}, 800, 'easeInCubic', function() {
		var intervalid = setInterval(
		   function () {
			  element.animate({rotate: '+=' + angle + 'deg'}, 0);
		  },
		  25
		);
		element.data('intervalid', intervalid);
	});
}

function stopdisk()
{
	var intervalid = $('#rotatingdisc').data('intervalid');
	clearInterval(intervalid);
	$('#rotatingdisc').stop().animate({rotate: '+=150deg'}, 800, 'easeOutCubic');	
}

function startdisk()
{
	var randnum=Math.floor(Math.random() * (60 - 10 + 1) + 10);//remove this if it doesn't suite you
	rotatedisk($('#rotatingdisc'),randnum);
}