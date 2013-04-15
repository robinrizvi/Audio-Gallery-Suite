//window.load instead of document.ready because I want the initial animations to start when all images and everything are fully loaded.
$(window).load(function(){

//Main function...execution starts from here
function main()
{
	user.checkauthentication();
	ui.initstart();
	ui.initcomplete();	
}

var user={
	playlistitems:null,
	useritems:null,
	items:null,
	info:null,//I should rather combine privilage and quota as well in info that would speed up excution and would require only 1http req rather than 3
	privilages:null,
	quota:null,
	authenticationready:false,
	infoready:false,//remove these just check direct variables if they are null or not
	playlistitemsready:false,
	itemsready:false,
	useritemsready:false,//this signifies the users items listed in users module
	checkauthentication:function(){
		var data="command=checkauth";
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						output = JSON && JSON.parse(json) || $.parseJSON(json);					
						if (output.cmd=="checkauth")
							if (output.success==true)
								if (output.data.authenticated==false)
								{									
									ui.handleerror(101);
								}
								else user.authenticationready=true;
							else ui.redirect("login.html");
						else ui.redirect("login.html");
					}
		});	
	},
	getinfo:function(){
		this.infoready=false;
		var data="command=userinfo";
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="userinfo")
						{
							if (output.success==true)
							{
								user.info=output.data;
								user.infoready=true;
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});
	},
	getprivilages:function(){
		var data="command=getprivilages";
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="getprivilages")
						{
							if (output.success==true)
							{
								user.privilages=output.data;						
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});
	},
	getquota:function(){
		var data="command=getquota";
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="getquota")
						{
							if (output.success==true)
							{
								user.quota=output.data;						
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});
	},
	getplaylistitems:function(){
		user.playlistitemsready=false;
		var data="command=getplaylistitems";
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="getplaylistitems")
						{
							if (output.success==true)
							{
								user.playlistitems=output.data;
								user.playlistitemsready=true;							
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});
	},
	getitems:function(playlistid){
		this.itemsready=false;
		var data="command=getitems&playlistid="+playlistid;
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="getitems")
						{
							if (output.success==true)
							{
								user.items=output.data;
								user.itemsready=true;							
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});
	},
	createitem:function(playlistid,title,description,itemtype,item,itemprogress,itempercentage,itemxhr){
		var uploadinprogress=false;
		if (itemtype=='file')
		{
			if (itemxhr==null) itemxhr=ui.uploaditemfile(playlistid,item,itemprogress,itempercentage);
			if (itemxhr.readyState==4) 
			{
				if (itemxhr.successresult==false)
				{
					ui.showloading('#playlistaudiomodule_audio .createdialog .commandbar','hide');
					return;
				}
				uploadinprogress=false;
			}
			else uploadinprogress=true;	
		}
		if (uploadinprogress) setTimeout(function(){user.createitem(playlistid,title,description,itemtype,item,itemprogress,itempercentage,itemxhr)},500);	
		else
		{
			//make entries in the database
			var db_name=itemtype=='file'?item.name:item;			
			var db_title=title;
			var db_description=description;
			var db_type=itemtype;
			var db_playlistid=playlistid;
			user.createitementry(db_name,db_title,db_description,db_type,db_playlistid);
		}
	},
	createitementry:function(db_name,db_title,db_description,db_type,db_playlistid){
		var data="command=createitementry&name="+db_name+"&title="+db_title+"&description="+db_description+"&type="+db_type+"&playlistid="+db_playlistid;
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="createitementry")
						{
							if (output.success==true)
							{
								ui.showmessage("Audio has been created");//after creating im getting all data back from server and showing it all again i can improve this to only set the new item client side.
								ui.showloading('#playlistaudiomodule_audio .createdialog .commandbar','hide');
								user.getitems(ui.selectedplaylistitem.attr("data-id"));
								ui.showitems();
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});	
	},
	deleteitem:function(item){
		var data="command=deleteitem&itemid="+item.attr('data-id');
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						ui.showloading(item,'hide');
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="deleteitem")
						{
							if (output.success==true)
							{								
								ui.removeitem(item);
								ui.showmessage("The audio has been deleted");							
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});
	},
	getuseritems:function(){
		user.useritemsready=false;
		var data="command=getuseritems";
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="getuseritems")
						{
							if (output.success==true)
							{
								user.useritems=output.data;
								user.useritemsready=true;							
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});
	},
	createplaylist:function(name,description,thumb,thumbprogress,thumbpercentage,thumbxhr){
		var uploadinprogress=false;
		if (thumbxhr==null) thumbxhr=ui.uploadplaylistfile(null,thumb,thumbprogress,thumbpercentage);
		if (thumbxhr.readyState==4) 
		{
			if (thumbxhr.successresult==false)
			{
				ui.showloading('#playlistaudiomodule_playlist .createdialog .commandbar','hide');
				return;
			}
			uploadinprogress=false;
		}
		else uploadinprogress=true;	
		if (uploadinprogress) setTimeout(function(){user.createplaylist(name,description,thumb,thumbprogress,thumbpercentage,thumbxhr)},500);	
		else
		{
			//make entries in the database
			user.createplaylistentry(name,description,thumb.name);
		}		
	},
	createplaylistentry:function(name,description,thumbname){
		var data="command=createplaylistentry&name="+name+"&username="+"&description="+description+"&thumbname="+thumbname;
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="createplaylistentry")
						{
							if (output.success==true)
							{
								ui.showmessage("Playlist has been created");
								ui.showloading('#playlistaudiomodule_playlist .createdialog .commandbar','hide');
								user.getplaylistitems();
								ui.showplaylistitems();
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});	
	},
	deleteplaylist:function(playlist){
		var data="command=deleteplaylist&playlistid="+playlist.attr('data-id');
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						ui.showloading(playlist,'hide');
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="deleteplaylist")
						{
							if (output.success==true)
							{								
								ui.removeplaylistitem(playlist);
								ui.showmessage("The playlist has been deleted");							
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});
	},
	editplaylist:function(playlistid,name,description,thumb,thumbprogress,thumbpercentage,thumbxhr){
		if (thumb!=null)
		{
			//delete the old thumb here
			var uploadinprogress=false;
			if (thumbxhr==null) thumbxhr=ui.uploadplaylistfile(playlistid,thumb,thumbprogress,thumbpercentage);			
			if (thumbxhr.readyState==4) 
			{
				if (thumbxhr.successresult==false)
				{
					ui.showloading('#playlistaudiomodule_playlist .editdialog .commandbar','hide');
					return;
				}
				uploadinprogress=false;
			}
			else uploadinprogress=true;				
			if (uploadinprogress) 
			{
				setTimeout(function(){user.editplaylist(playlistid,name,description,thumb,thumbprogress,thumbpercentage,thumbxhr)},500);
				return;
			}
		}
		//make entries in the database
		user.editplaylistentry(playlistid,name,description,thumb==null?null:thumb.name);	
	},
	editplaylistentry:function(playlistid,name,description,thumbname){
		var data="command=editplaylistentry&id="+playlistid+"&name="+name+"&description="+description+"&thumbname="+thumbname;
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="editplaylistentry")
						{
							if (output.success==true)
							{
								ui.showmessage("Playlist has been edited");								
								user.getplaylistitems();
								ui.showplaylistitems();
								//ui.resetdialog('playlistaudio','editdialog','#playlistaudiomodule_playlist .editdialog');
								ui.showloading('#playlistaudiomodule_playlist .editdialog .commandbar','hide');
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});	
	},
	createuser:function(name,username,password,description,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize,thumb,thumbprogress,thumbpercentage,thumbxhr){
		var uploadinprogress=false;
		if (thumbxhr==null) thumbxhr=ui.uploaduserfile(null,thumb,thumbprogress,thumbpercentage);		
		if (thumbxhr.readyState==4) 
		{
			if (thumbxhr.successresult==false)
			{
				ui.showloading('#usersmodule .createdialog .commandbar','hide');
				return;
			}
			uploadinprogress=false;
		}
		else uploadinprogress=true;
		if (uploadinprogress) setTimeout(function(){user.createuser(name,username,password,description,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize,thumb,thumbprogress,thumbpercentage,thumbxhr)},500);	
		else
		{
			//make entries in the database
			user.createuserentry(name,username,password,description,thumb.name,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize);
		}		
	},
	createuserentry:function(name,username,password,description,thumbname,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize){
		var data="command=createuserentry&name="+name+"&username="+username+"&password="+password+"&description="+description+"&thumbname="+thumbname+"&privilage_createplaylist="+privilage_createplaylist+"&privilage_createaudio="+privilage_createaudio+"&privilage_uploadaudio="+privilage_uploadaudio+"&privilage_usermanagement="+privilage_usermanagement+"&quota_audio="+quota_audio+"&quota_maxaudiosize="+quota_maxaudiosize;
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="createuserentry")
						{
							if (output.success==true)
							{
								ui.showmessage("User has been created");
								ui.showloading('#usersmodule .createdialog .commandbar','hide');
								user.getuseritems();
								ui.showuseritems();
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});	
	},
	deleteuser:function(user){
		var data="command=deleteuser&userid="+user.attr('data-id');
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						ui.showloading(user,'hide');
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="deleteuser")
						{
							if (output.success==true)
							{								
								ui.removeuseritem(user);
								ui.showmessage("The user has been deleted");							
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});
	},
	edituser:function(userid,name,username,password,description,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize,thumb,thumbprogress,thumbpercentage,thumbxhr){
		if (thumb!=null)
		{
			//delete the old thumb here
			var uploadinprogress=false;
			if (thumbxhr==null) thumbxhr=ui.uploaduserfile(userid,thumb,thumbprogress,thumbpercentage);
			if (thumbxhr.readyState==4) uploadinprogress=false;
			else uploadinprogress=true;
			if (uploadinprogress) 
			{
				setTimeout(function(){user.edituser(userid,name,username,password,description,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize,thumb,thumbprogress,thumbpercentage,thumbxhr)},500);
				return;
			}
		}
		//make entries in the database
		user.edituserentry(userid,name,username,password,description,thumb==null?null:thumb.name,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize);	
	},
	edituserentry:function(userid,name,username,password,description,thumbname,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize){
		var data="command=edituserentry&id="+userid+"&name="+name+"&username="+username+"&password="+password+"&description="+description+"&thumbname="+thumbname+"&privilage_createplaylist="+privilage_createplaylist+"&privilage_createaudio="+privilage_createaudio+"&privilage_uploadaudio="+privilage_uploadaudio+"&privilage_usermanagement="+privilage_usermanagement+"&quota_audio="+quota_audio+"&quota_maxaudiosize="+quota_maxaudiosize;
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="edituserentry")
						{
							if (output.success==true)
							{
								ui.showmessage("User has been edited");								
								user.getuseritems();
								ui.showuseritems();
								//ui.resetdialog('users','editdialog','#usersmodule .editdialog');
								ui.showloading('#usersmodule .editdialog .commandbar','hide');
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});	
	},
	editprofileinfo:function(userid,name,username,password,description,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize,thumb,thumbprogress,thumbpercentage,thumbxhr){
		if (thumb!=null)
		{
			//delete the old thumb here
			var uploadinprogress=false;
			if (thumbxhr==null) thumbxhr=ui.uploaduserfile(userid,thumb,thumbprogress,thumbpercentage);
			if (thumbxhr.readyState==4) uploadinprogress=false;
			else uploadinprogress=true;
			if (uploadinprogress) 
			{
				setTimeout(function(){user.editprofileinfo(userid,name,username,password,description,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize,thumb,thumbprogress,thumbpercentage,thumbxhr)},500);
				return;
			}
		}
		//make entries in the database
		user.editprofileinfoentry(userid,name,username,password,description,thumb==null?null:thumb.name,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize);	
	},
	editprofileinfoentry:function(userid,name,username,password,description,thumbname,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize){
		var data="command=edituserentry&id="+userid+"&name="+name+"&username="+username+"&password="+password+"&description="+description+"&thumbname="+thumbname+"&privilage_createplaylist="+privilage_createplaylist+"&privilage_createaudio="+privilage_createaudio+"&privilage_uploadaudio="+privilage_uploadaudio+"&privilage_usermanagement="+privilage_usermanagement+"&quota_audio="+quota_audio+"&quota_maxaudiosize="+quota_maxaudiosize;
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="edituserentry")
						{
							if (output.success==true)
							{
								ui.showmessage("Profile information has been edited");								
								ui.showloading('#profilemodule .userinfo .commandbar','hide');
								user.getinfo();
								ui.showuserinfo();
							}
							else ui.handleerror(output.data.errorcode);
						}
						else ui.handleerror(106);
					}
		});	
	},
	logout:function(){
		var data="command=logout";
		$.ajax({
				type: "POST",
				url: "../../php/audiogallery_managementengine.php",
				data: data,
				success: 
					function(json)
					{
						output = JSON && JSON.parse(json) || $.parseJSON(json);
						if (output.cmd=="logout")
						{
							if (output.success==true)
							{								
								ui.redirect('login.html');							
							}
							else ui.handleerror(output.data.errorcode,output.data.errormsg);
						}
						else ui.handleerror(106);
					}
		});
	}
}

var ui={
	jscrollapi_itemcontainer:null,
	jscrollapi_useritemscontainer:null,
	jscrollapi_playlistitemscontainer:null,
	ready:false,
	selectedplaylistitem:null,
	selecteditem:null,
	selecteduseritem:null,
	itemfileinputfilters:null,
	activemodule:null,
	playlistitemtemplate:
		'<div class="item" data-id="{id}" data-name="{name}" data-description="{description}" data-thumburl="{thumburl}"">\
      					<div class="thumb"><img src="{thumb}" width="164" height="123" /></div>\
      					<div class="title">{title}</div>\
						<div class="minicommandbar">\
							<div class="minicommand editminibtn icon-edit"></div>\
							<div class="minicommand viewminibtn icon-eye-open"></div>\
							<div class="minicommand deleteminibtn icon-trash"></div>\
						</div>\
					</div>\
		',
	itemtemplate:
		'<div class="item" data-id="{id}" data-url="{url}" data-description="{description}" data-title="{title}">\
      					<div class="thumb"><img src="../../image/management_images/audiogallery/audio.png" width="164" height="123" /></div>\
      					<div class="title">{titletext}</div>\
						<div class="minicommandbar">\
							<div class="minicommand editminibtn icon-edit"></div>\
							<div class="minicommand viewminibtn icon-eye-open"></div>\
							<div class="minicommand deleteminibtn icon-trash"></div>\
						</div>\
					</div>\
		',
	useritemtemplate:
		'<div class="item" data-id="{id}" data-username="{username}" data-name="{name}" data-description="{description}" data-avatarurl="{avatarurl}" data-privilage_createplaylist="{privilage_createplaylist}" data-privilage_createaudio="{privilage_createaudio}" data-privilage_uploadaudio="{privilage_uploadaudio}" data-privilage_usermanagement="{privilage_usermanagement}" data-quota_audio="{quota_audio}" data-quota_maxaudiosize="{quota_maxaudiosize}">\
      					<div class="thumb"><img src="{thumburl}" width="164" height="123" /></div>\
      					<div class="title">{title}</div>\
						<div class="minicommandbar">\
							<div class="minicommand editminibtn icon-edit"></div>\
							<div class="minicommand viewminibtn icon-eye-open"></div>\
							<div class="minicommand deleteminibtn icon-trash"></div>\
						</div>\
					</div>\
		',
	initstart:function(){
		$.prettyLoader({loader: '../../image/prettyLoader/ajax-loader.gif', /* Path to your loader gif */});
		this.overlay('show');
		this.showloading('.overlay','show');
		ui.init();
	},
	init:function(){
		if (user.authenticationready)
		{
			user.getinfo();
			user.getprivilages();
			user.getquota();
			ui.showuserinfo();
			ui.attacheventhandlers('generic');
		}
		else setTimeout(function(){ui.init()},500);
	},
	showuserinfo:function(){
		if (user.infoready)
		{
			$('#user #name').text(user.info.name);
			$('#user #username').text(user.info.username);
			$('#user #avatar img').attr('src',user.info.avatar);
			this.ready=true;
		}
		else setTimeout(function(){ui.showuserinfo()},500);
	},
	initcomplete:function(){
		if (this.ready)
		{
			this.overlay('hide');
			this.showloading('.overlay','hide');
			ui.changemodule('playlistaudio');						
			ui.modulepanelclickanimation();
		}
		else setTimeout(function(){ui.initcomplete()},500);
	},
	redirect:function(url,querystring){
		querystring=!querystring && '?error=You need to login before accessing that page.';
		window.location=url+querystring;
	},
	changemodule:function(module){
		ui.activemodule=module;
		$('#playlistaudiomodule_audio,#playlistaudiomodule_playlist,#usersmodule,#profilemodule').hide();
		$('#playlistaudiomodulebtn,#usersmodulebtn,#profilemodulebtn').removeClass('active');
		switch(module)
		{
			case 'playlistaudio':
				$('#playlistaudiomodulebtn').addClass('active');
				$('#playlistaudiomodule_playlist').show();
				$('#playlistaudiomodule_audio').show();
				ui.jscrollapi_playlistitemscontainer=ui.initjscrollpane('#playlistaudiomodule_playlist .contentbox');
				ui.jscrollapi_itemcontainer=ui.initjscrollpane('#playlistaudiomodule_audio .contentbox');
				ui.resetdialog('playlistaudio','createdialog','#playlistaudiomodule_playlist .createdialog');
				ui.resetdialog('playlistaudio','viewdialog','#playlistaudiomodule_playlist .viewdialog');
				ui.resetdialog('playlistaudio','editdialog','#playlistaudiomodule_playlist .editdialog');
				ui.resetdialog('playlistaudio','createdialog','#playlistaudiomodule_audio .createdialog');
				ui.resetdialog('playlistaudio','viewdialog','#playlistaudiomodule_audio .viewdialog');
				ui.resetdialog('playlistaudio','editdialog','#playlistaudiomodule_audio .editdialog');			
				ui.attacheventhandlers('playlistaudio');
				user.getplaylistitems();
				ui.showplaylistitems();				
				break;
			case 'users':
				if (user.privilages.usermanagement==1)
				{
					$('#usersmodulebtn').addClass('active');
					$('#usersmodule').show();					
					ui.jscrollapi_useritemscontainer=ui.initjscrollpane('#usersmodule .contentbox');
					ui.resetdialog('users','createdialog','#usersmodule .createdialog');
					ui.resetdialog('users','editdialog','#usersmodule .editdialog');
					ui.resetdialog('users','viewdialog','#usersmodule .viewdialog');
					ui.attacheventhandlers('users');
					user.getuseritems();
					ui.showuseritems();
				}
				else 
				{
					ui.changemodule('playlistaudio');
					ui.handleerror(104);
				}
				break;
			case 'profile':
				$('#profilemodulebtn').addClass('active');
				$('#profilemodule').show();
				ui.renderprofile($('#profilemodule .privilageinfo'),user.privilages,$('#profilemodule .quotainfo'),user.quota,$('#profilemodule .userinfo'),user.info);
				ui.attacheventhandlers('profile');
				break;	
		}
	},
	showplaylistitems:function(){
		ui.showloading('#playlistaudiomodule_playlist .contentbox','show');
		if (user.playlistitemsready)
		{
			if (user.playlistitems!=null)
			{				
				var items=user.playlistitems;
				var itembox=ui.jscrollapi_playlistitemscontainer!=undefined?ui.jscrollapi_playlistitemscontainer.getContentPane():'#playlistaudiomodule_playlist .contentbox';
				$(itembox).empty();
				for (i=0;i<items.length;i++)
				{
					var html=this.playlistitemtemplate;
					var item=items[i];
					html=html.replace('{id}',item.id);
					html=html.replace('{name}',item.name);
					html=html.replace('{description}',item.description);
					html=html.replace('{thumburl}',item.thumburl);
					html=html.replace('{thumb}',item.thumburl);
					html=html.replace('{title}',item.name);					
					$(itembox).append(html);
				}
				ui.jscrollapi_playlistitemscontainer!=undefined?ui.jscrollapi_playlistitemscontainer.reinitialise():null;
				user.playlistitems=null;user.playlistitemsready=false;//clearing the items from the variable when they have been shown so that i don't hold up unnecessary memory when even I am not making use of that variable at any point later indeed always im getting fresh data from server when i need that.
				ui.selectfirstitem('#playlistaudiomodule_playlist','playlist');
			}
			ui.showloading('#playlistaudiomodule_playlist .contentbox','hide');
		}
		else setTimeout(function(){ui.showplaylistitems()},500);
	},
	removeplaylistitem:function(item){
		item.remove();
		ui.selectfirstitem('#playlistaudiomodule_playlist','playlist');
	},
	showitems:function(){
		ui.showloading('#playlistaudiomodule_audio .contentbox','show');
		if (user.itemsready)
		{
			if (user.items!=null)
			{				
				var items=user.items;
				var itembox=ui.jscrollapi_itemcontainer!=undefined?ui.jscrollapi_itemcontainer.getContentPane():'#playlistaudiomodule_audio .contentbox';
				$(itembox).empty();
				for (i=0;i<items.length;i++)
				{
					var html=this.itemtemplate;
					var item=items[i];
					html=html.replace('{id}',item.id);
					html=html.replace('{url}',item.url);
					html=html.replace('{description}',item.description);
					html=html.replace('{title}',item.title);
					html=html.replace('{titletext}',item.title);				
					$(itembox).append(html);
				}
				ui.jscrollapi_itemcontainer!=undefined?ui.jscrollapi_itemcontainer.reinitialise():null;
				user.items=null;user.itemsready=false;//clearing the items from the variable when they have been shown so that i don't hold up unnecessary memory when even I am not making use of that variable at any point later indeed always im getting fresh data from server when i need that.
			}
			ui.showloading('#playlistaudiomodule_audio .contentbox','hide');
		}
		else setTimeout(function(){ui.showitems()},500);
	},
	removeitem:function(item){
		item.remove();
	},
	showuseritems:function(){
		ui.showloading('#usersmodule .contentbox','show');
		if (user.useritemsready)
		{
			if (user.useritems!=null)
			{				
				var items=user.useritems;
				var itembox=ui.jscrollapi_useritemscontainer!=undefined?ui.jscrollapi_useritemscontainer.getContentPane():'#usersmodule .contentbox';
				$(itembox).empty();
				for (i=0;i<items.length;i++)
				{
					var html=this.useritemtemplate;
					var item=items[i];
					html=html.replace('{id}',item.id);
					html=html.replace('{username}',item.username);
					html=html.replace('{name}',item.name);
					html=html.replace('{description}',item.description);
					html=html.replace('{avatarurl}',item.avatarurl);
					html=html.replace('{privilage_createplaylist}',item.privilage_createplaylist);
					html=html.replace('{privilage_createaudio}',item.privilage_createaudio);
					html=html.replace('{privilage_uploadaudio}',item.privilage_uploadaudio);
					html=html.replace('{privilage_usermanagement}',item.privilage_usermanagement);
					html=html.replace('{quota_audio}',item.quota_audio);
					html=html.replace('{quota_maxaudiosize}',item.quota_maxaudiosize);
					html=html.replace('{thumburl}',item.avatarurl);
					html=html.replace('{title}',item.name);			
					$(itembox).append(html);
				}
				ui.jscrollapi_useritemscontainer!=undefined?ui.jscrollapi_useritemscontainer.reinitialise():null;
				user.useritems=null;user.useritemsready=false;//clearing the items from the variable when they have been shown so that i don't hold up unnecessary memory when even I am not making use of that variable at any point later indeed always im getting fresh data from server when i need that.
			}
			ui.showloading('#usersmodule .contentbox','hide');
		}
		else setTimeout(function(){ui.showuseritems()},500);
	},
	removeuseritem:function(item){
		item.remove();
	},
	overlay:function(status){
		status=='show'?$('.overlay').show():$('.overlay').hide();
	},
	showloading:function(element,status,text){
		status=='show'?$(element).showLoading():$(element).hideLoading();
	},
	initjscrollpane:function(domelement){
		var settings = {
			showArrows:true,
			maintainPosition: false,
			hideFocus:true,
			animateScroll: true
		};
		return $(domelement).jScrollPane(settings).data('jsp');
	},
	selectfirstitem:function(module,type){
		switch (type)
		{
			case 'playlist':
				var firstitem=module+' .container .item:nth-child(1)';
				if ($(firstitem).get(0)==undefined) ui.selectitem(null,'playlist');
				else $(firstitem).click();
				break;
			case 'item':
				break;
			case 'user':
				break;
		}
	},
	selectitem:function(item,type){
		switch(type)
		{
			case 'item':
				ui.selecteditem=item;
				break;
			case 'playlist':
				ui.selectedplaylistitem=item;
				if (item==null)
				{
					$('#playlistaudiomodule_audio .container #topbar .moduletitle').text("No Playlist Selected");
					var itembox=ui.jscrollapi_itemcontainer!=undefined?ui.jscrollapi_itemcontainer.getContentPane():'#playlistaudiomodule_audio .contentbox';
					$(itembox).empty();	
				}
				else
				{
					$('#playlistaudiomodule_audio .container #topbar .moduletitle').text(item.attr("data-name"));
					user.getitems(item.attr("data-id"));
					ui.showitems();
				}
				break;
			case 'user':
				ui.selecteduseritem=item;
				break;	
		}
		if (item!=null) ui.highlightitem(item);
	},
	highlightitem:function(item){
		item.siblings().removeAttr('style');
		item.css('box-shadow','0px 0px 10px #069');		
	},
	renderprofile:function(privilageinfo,privilages,quotainfo,quota,userinfo,info)
	{
		privilages.createplaylist==1?privilageinfo.find('.privilage_createplaylist .value').addClass('icon-ok-sign'):privilageinfo.find('.privilage_createplaylist .value').addClass('icon-remove-sign');
		privilages.createaudio==1?privilageinfo.find('.privilage_createaudio .value').addClass('icon-ok-sign'):privilageinfo.find('.privilage_createaudio .value').addClass('icon-remove-sign');
		privilages.uploadaudio==1?privilageinfo.find('.privilage_uploadaudio .value').addClass('icon-ok-sign'):privilageinfo.find('.privilage_uploadaudio .value').addClass('icon-remove-sign');
		privilages.usermanagement==1?privilageinfo.find('.privilage_usermanagement .value').addClass('icon-ok-sign'):privilageinfo.find('.privilage_usermanagement .value').addClass('icon-remove-sign');
		
		quotainfo.find('.quota_audio .value').text(quota.audio);
		quotainfo.find('.quota_maxaudiosize .value').text(quota.maxaudiosize);
		
		userinfo.find('.name .value').val(info.name);
		userinfo.find('.username .value').val(info.username);
		userinfo.find('.password .value').val("");
		userinfo.find('.description .value').val(info.description);
		var avatarsplitarray=info.avatar.split('/');
		var avatarname=avatarsplitarray[avatarsplitarray.length-1];
		userinfo.find('.thumbnail .filename').text(avatarname);
		
		ui.initjscrollpane('#profilemodule .privilageinfo .body');
		ui.initjscrollpane('#profilemodule .quotainfo .body');
		ui.initjscrollpane('#profilemodule .userinfo .body');
	},
	renderdialog:function(module,dialogtype,dialog){
		switch (module)
		{
			case 'playlistaudio':
				var moduletypeid=$(dialog).parent('div').attr('id');
				var submodule=(moduletypeid=='playlistaudiomodule_playlist')?'playlist':'audio';
				switch (submodule)
				{
					case 'playlist':
						if (dialogtype=='createdialog')
						{
							var rendersuccess=true;
							try
							{
								if (user.privilages==null) throw(107);
								if (user.privilages.createplaylist!=1) throw(104);
							}
							catch (err)
							{
								ui.handleerror(err);
								rendersuccess=false;
							}
							return rendersuccess;
						}
						if (dialogtype=='editdialog')
						{
							var rendersuccess=true;
							try
							{
								if (ui.selectedplaylistitem!=null)
								{
									var playlistid=ui.selectedplaylistitem.attr('data-id');
									var thumbsplitarray=ui.selectedplaylistitem.attr('data-thumburl').split('/');
									var thumbname=thumbsplitarray[thumbsplitarray.length-1];
									var name=ui.selectedplaylistitem.attr('data-name');
									var description=ui.selectedplaylistitem.attr('data-description');									
									$(dialog).find('.element.id').val(playlistid);
									$(dialog).find('.element.thumbnail .filename').text(thumbname);
									$(dialog).find('.element .name').val(name);
									$(dialog).find('.element .description').val(description);														
								}
								else throw("No playlist is selected");
							}
							catch(err)
							{
								ui.handleerror(111,err);
								rendersuccess=false;	
							}
							return rendersuccess;
						}
						if (dialogtype=='viewdialog')
						{
							var rendersuccess=true;
							try
							{								
								if (ui.selectedplaylistitem!=null)
								{
									var thumburl=ui.selectedplaylistitem.attr('data-thumburl');
									var name=ui.selectedplaylistitem.attr('data-name');
									var description=ui.selectedplaylistitem.attr('data-description');
									$(dialog).find('.element.thumb img').attr('src',thumburl);
									$(dialog).find('.element.name label:nth-child(2)').text(name);
									$(dialog).find('.element.description label:nth-child(2)').text(description);					
								}
								else throw("No playlist is selected");
							}
							catch(err)
							{
								ui.handleerror(111,err);
								rendersuccess=false;	
							}
							return rendersuccess;
						}
						break;
					case 'audio':
						if (dialogtype=='createdialog')
						{
							var rendersuccess=true;
							try
							{							
								$(dialog).find(".fileradioset").buttonset();
								if (ui.selectedplaylistitem!=null)
								{
									if (user.privilages!=null)
									{
										if (user.privilages.createaudio==1)
										{
											ui.itemfileinputfilters=new Array('audio/mp3');						
											if (user.privilages.uploadaudio==0)
											{
												$(dialog).find('.fileradioset').children('input[value="file"]').next('label').hide();
												$(dialog).find('.fileradioset').children('input[value="file"]').hide();								
											}
											else
											{
												$(dialog).find('.fileradioset').children('input[value="file"]').next('label').show();
												$(dialog).find('.fileradioset').children('input[value="file"]').show();
											}
										}
										else throw(104);
									}
									else throw(107);
								}
								else throw(106);//correct this here it should show that no playlist selected	
							}
							catch (err)
							{
								ui.handleerror(err);
								rendersuccess=false;
							}
							return rendersuccess;
						}
						if (dialogtype=='editdialog')
						{
							var rendersuccess=true;
							try
							{
								/*if (ui.selectedplaylistitem!=null)
								{
									var playlistid=ui.selectedplaylistitem.attr('data-id');
									var thumbsplitarray=ui.selectedplaylistitem.attr('data-thumburl').split('/');
									var thumbname=thumbsplitarray[thumbsplitarray.length-1];
									var name=ui.selectedplaylistitem.attr('data-name');
									var description=ui.selectedplaylistitem.attr('data-description');									
									$(dialog).find('.element.id').val(playlistid);
									$(dialog).find('.element.thumbnail .filename').text(thumbname);
									$(dialog).find('.element .name').val(name);
									$(dialog).find('.element .description').val(description);														
								}
								else throw("No playlist is selected");*/
							}
							catch(err)
							{
								ui.handleerror(111,err);
								rendersuccess=false;	
							}
							return rendersuccess;
						}
						if (dialogtype=='viewdialog')
						{
							var rendersuccess=true;
							try
							{
								if (ui.selecteditem!=null)
								{
									var dataurl=ui.selecteditem.attr('data-url');
									ui.setupaudioplayer("#jquery_jplayer_1",dataurl);
								}
								else throw("No item is selected");	
							}
							catch(err)
							{
								ui.handleerror(111,err);
								rendersuccess=false;	
							}
							return rendersuccess;
						}
						break;	
				}
				break;
			case 'users':
				if (dialogtype=='createdialog')
				{
					var rendersuccess=true;
					$(dialog).find(".thumbradioset").buttonset();
					return rendersuccess;
				}
				if (dialogtype=='editdialog')
				{
					var rendersuccess=true;
					try
					{
						if (ui.selecteduseritem!=null)
						{
							var userid=ui.selecteduseritem.attr('data-id');
							var avatarsplitarray=ui.selecteduseritem.attr('data-avatarurl').split('/');
							var avatarname=avatarsplitarray[avatarsplitarray.length-1];
							var username=ui.selecteduseritem.attr('data-username');
							var name=ui.selecteduseritem.attr('data-name');
							var description=ui.selecteduseritem.attr('data-description');
							var privilage_createplaylist=ui.selecteduseritem.attr('data-privilage_createplaylist');
							var privilage_createaudio=ui.selecteduseritem.attr('data-privilage_createaudio');
							var privilage_uploadaudio=ui.selecteduseritem.attr('data-privilage_uploadaudio');
							var privilage_usermanagement=ui.selecteduseritem.attr('data-privilage_usermanagement');
							var quota_audio=ui.selecteduseritem.attr('data-quota_audio');
							var quota_maxaudiosize=ui.selecteduseritem.attr('data-quota_maxaudiosize');
							$(dialog).find('.element.id').val(userid);
							$(dialog).find('.element.thumbnail .filename').text(avatarname);
							$(dialog).find('.element .username').val(username);
							$(dialog).find('.element .name').val(name);
							$(dialog).find('.element .description').val(description);
							$(dialog).find('.element .privilage_createplaylist').get(0).checked=privilage_createplaylist==1?true:false;
							$(dialog).find('.element .privilage_createaudio').get(0).checked=privilage_createaudio==1?true:false;
							$(dialog).find('.element .privilage_uploadaudio').get(0).checked=privilage_uploadaudio==1?true:false;
							$(dialog).find('.element .privilage_usermanagement').get(0).checked=privilage_usermanagement==1?true:false;
							$(dialog).find('.element .quota_audio').val(quota_audio);
							$(dialog).find('.element .quota_maxaudiosize').val(quota_maxaudiosize);							
						}
						else throw("No user is selected");
					}
					catch(err)
					{
						ui.handleerror(111,err);
						rendersuccess=false;	
					}
					return rendersuccess;
				}
				if (dialogtype=='viewdialog')
				{
					var rendersuccess=true;
					try
					{
						if (ui.selecteduseritem!=null)
						{
							var avatarurl=ui.selecteduseritem.attr('data-avatarurl');
							var username=ui.selecteduseritem.attr('data-username');
							var name=ui.selecteduseritem.attr('data-name');
							var description=ui.selecteduseritem.attr('data-description');							
							var privilage_createplaylist=ui.selecteduseritem.attr('data-privilage_createplaylist')==1?'Yes':'No';
							var privilage_createaudio=ui.selecteduseritem.attr('data-privilage_createaudio')==1?'Yes':'No';
							var privilage_uploadaudio=ui.selecteduseritem.attr('data-privilage_uploadaudio')==1?'Yes':'No';
							var privilage_usermanagement=ui.selecteduseritem.attr('data-privilage_usermanagement')==1?'Yes':'No';
							var quota_audio=ui.selecteduseritem.attr('data-quota_audio');
							var quota_maxaudiosize=ui.selecteduseritem.attr('data-quota_maxaudiosize');
							$(dialog).find('.element.avatar img').attr('src',avatarurl);
							$(dialog).find('.element.username label:nth-child(2)').text(username);
							$(dialog).find('.element.name label:nth-child(2)').text(name);
							$(dialog).find('.element.description label:nth-child(2)').text(description);
							$(dialog).find('.element.privilage_createplaylist label:nth-child(2)').text(privilage_createplaylist);
							$(dialog).find('.element.privilage_createaudio label:nth-child(2)').text(privilage_createaudio);
							$(dialog).find('.element.privilage_uploadaudio label:nth-child(2)').text(privilage_uploadaudio);
							$(dialog).find('.element.privilage_usermanagement label:nth-child(2)').text(privilage_usermanagement);
							$(dialog).find('.element.quota_audio label:nth-child(2)').text(quota_audio);
							$(dialog).find('.element.quota_maxaudiosize label:nth-child(2)').text(quota_maxaudiosize);							
						}
						else throw("No user is selected");
					}
					catch(err)
					{
						ui.handleerror(111,err);
						rendersuccess=false;	
					}
					return rendersuccess;
				}
				break;	
		}
	},
	resetdialog:function(module,dialogtype,dialog){
		$(dialog).css(
				{
					top:($(window).height()/2)-($(dialog).height()/2)-40+'px',
					left:($(window).width()/2)-($(dialog).width()/2)+'px'
				}
		);
		switch (module)
		{
			case 'playlistaudio':
				var moduletypeid=$(dialog).parent('div').attr('id');
				var submodule=(moduletypeid=='playlistaudiomodule_playlist')?'playlist':'audio';
				switch(submodule)
				{
					case 'playlist':
						if (dialogtype=='createdialog')
						{
							//do other stuff
						}
						if (dialogtype=='editdialog')
						{
							$(dialog).find('.thumbnail .filename').html("");
							$(dialog).find('.thumbnail .filesize').html("");
							$(dialog).find('.thumbnail .percentage').html("");
							$(dialog).find('.thumbnail').children('.fileinput').find('.browsebtn').html('<div class="icon-file"></div><input type="file" name="file">');
						}
						if (dialogtype=='viewdialog')
						{
							//editcode
						}
						break;
					case 'audio':
						if (dialogtype=='createdialog')
						{
							//do other stuff
						}
						if (dialogtype=='editdialog')
						{
							//do some stuff
						}
						if (dialogtype=='viewdialog')
						{
							ui.removeaudioplayer("#jquery_jplayer_1");							
						}
						break;
					}
					break;
			case 'users':
				if (dialogtype=='createdialog')
				{
					//do other stuff
				}
				if (dialogtype=='editdialog')
				{
					$(dialog).find('.thumbnail .filename').html("");
					$(dialog).find('.thumbnail .filesize').html("");
					$(dialog).find('.thumbnail .percentage').html("");
					$(dialog).find('.thumbnail').children('.fileinput').find('.browsebtn').html('<div class="icon-file"></div><input type="file" name="file">');
					$(dialog).find('.thumbnail').children('.fileinput').find('input[type="file"]').get(0).files=[];
					$(dialog).find('.password').val("");
				}
				if (dialogtype=='viewdialog')
				{
					//do some stuff if you eant
				}
				break;
		}
	},
	dialog:function(module,dialogtype,dialog,operation){
		switch (module)
		{
			case 'playlistaudio':
				switch(operation)
				{
					case 'open':
						if (dialogtype=='createdialog') 
						{
							if (ui.renderdialog(module,'createdialog',dialog))
							{
								$(dialog).show();
								ui.dialogiconblink(dialog,-1);
							}
						}
						if (dialogtype=='editdialog')
						{
							if (ui.renderdialog(module,'editdialog',dialog))
							{								
								$(dialog).show();
								ui.dialogiconblink(dialog,-1);
							}
						}
						if (dialogtype=='viewdialog')
						{
							if (ui.renderdialog(module,'viewdialog',dialog))
							{
								$(dialog).show();
								ui.dialogiconblink(dialog,-1);
							}
						}
						break;
					case 'close':
						if (dialogtype=='createdialog') 
						{
							ui.resetdialog(module,'createdialog',dialog);
						}
						if (dialogtype=='editdialog')
						{
							ui.resetdialog(module,'editdialog',dialog);
						}
						if (dialogtype=='viewdialog')
						{
							ui.resetdialog(module,'viewdialog',dialog);
						}
						$(dialog).hide();			
						break;
					case 'minimize':
						$(dialog).hide();
						break;
					}
				break;
			case 'users':
				switch(operation)
				{
					case 'open':
						if (dialogtype=='createdialog') 
						{
							if (ui.renderdialog(module,'createdialog',dialog))
							{
								$(dialog).show();
								ui.dialogiconblink(dialog,-1);
								ui.initjscrollpane('#usersmodule .createdialog .body');
							}
						}
						if (dialogtype=='editdialog')
						{
							if (ui.renderdialog(module,'editdialog',dialog))
							{								
								$(dialog).show();
								ui.dialogiconblink(dialog,-1);
								ui.initjscrollpane('#usersmodule .editdialog .body');
							}
						}
						if (dialogtype=='viewdialog')
						{
							if (ui.renderdialog(module,'viewdialog',dialog))
							{
								$(dialog).show();
								ui.dialogiconblink(dialog,-1);
								ui.initjscrollpane('#usersmodule .viewdialog .body');
							}
						}
						break;
					case 'close':
						if (dialogtype=='createdialog') 
						{
							ui.resetdialog(module,'createdialog',dialog);
						}
						if (dialogtype=='editdialog')
						{
							ui.resetdialog(module,'editdialog',dialog);
						}
						if (dialogtype=='viewdialog')
						{
							ui.resetdialog(module,'viewdialog',dialog);
						}
						$(dialog).hide();			
						break;
					case 'minimize':
						$(dialog).hide();
						break;
					}
				break;
		}
	},
	dialogiconblink:function (dialog,i)
	{
		i++;
		$(dialog).data("blinkcount",i);
		if (i<2)
		{
			if (i%2!=0) $(dialog+' .icon').css("background-color","rgba(0,133,204,1)");//Here I am using i%2!=0 as opposed to i%2==0 because of the fact that that jquery css function adds style as inline style attribute to dom elements so 1st time since in the inline style there is no bakcgorundcolor so browser does not do a transition for that..from the next time it recognized tht there is change in backgroundcolor property and thus does the transition.
			else $(dialog+' .icon').css("background-color","#505050");
			setTimeout(function(){ui.dialogiconblink(dialog,$(dialog).data("blinkcount"));},500);
		}
		else $(dialog+' .icon').removeAttr("style");
	},
	toggledialogelements:function (value,element)
	{
		switch(value)
		{
			case 'auto':
				$(element).children('.auto').show();
				$(element).children('.url').hide();
				$(element).children('.fileinput').hide();
				break;
			case 'url':
				$(element).children('.auto').hide();
				$(element).children('.url').show();
				$(element).children('.fileinput').hide();
				break;
			case 'file':
				$(element).children('.auto').hide();
				$(element).children('.url').hide();
				$(element).children('.fileinput').show();
				break;
		}
	},	
	rotatetoplogo:function (status,direction){
		clearInterval($("#logo img").data('intervalid'));
		if (status==1)
		{
			switch (direction)
			{
				case 'clockwise':
					var intervalid = setInterval(
					function () {
					  $("#logo img").stop().animate({rotate: '+=3deg'}, 0);
					},
					25
					);
					$("#logo img").data('intervalid', intervalid);
					break;	
				case 'anticlockwise':
					var intervalid = setInterval(
					function () {
					  $("#logo img").stop().animate({rotate: '-=3deg'}, 0);
					},
					25
					);
					$("#logo img").data('intervalid', intervalid);
					break;	
			}
		}
	},
	modulepanelclickanimation:function(){
		$('#cursor').show();
		var modulebottom=(window.innerHeight-$('#title p').position().top)-(($('#title p').height()/2)+($('#cursor').height()/2)+50)+'px';
		var moduleleft=$('#title').position().left+50+'px';
		$('#cursor').animate({bottom:modulebottom,left:moduleleft},4000,function(){
			$('#cursor').hide();
			ui.modulepanelslide('down');
		});
	},
	modulepanelslide:function (direction){
		switch (direction)
		{
			case 'up':
				$("#modulepanel").stop().animate({top:'-60px',opacity:0.9},1500);
				$(".container").stop().animate({top:'-50px'},1500);
				$("#footer").stop().animate({top:'0px'},1500);
				ui.rotatetoplogo(1,'anticlockwise');
				setTimeout(function(){ui.rotatetoplogo(0)},1500);
				break;
			case 'down':
				$(".module.active .icon").css("background-color","#333");//setting the active module highlight to deactivate while down animation is happening
				$("#modulepanel").stop().animate({top:'150px',opacity:1},1500,function(){ui.modulepanelblink(-1)});			
				$(".container").stop().animate({top:'150px'},1500);
				$("#footer").stop().animate({top:'200px'},1500);
				ui.rotatetoplogo(1,'clockwise');
				setTimeout(function(){ui.rotatetoplogo(0)},1500);
				break; 
		}
	},
	modulepanelblink:function (i){
		i++;
		$(".module .icon").css({		
			"-webkit-transition":"background-color 0.5s",
			"-moz-transition":"background-color 0.5s",
			"-o-transition":"background-color 0.5s",
			"transition":"background-color 0.5s"
		});
		$(".module .icon").data("blinkcount",i);
		if (i<6)
		{
			if (i%2==0) $(".module .icon").css("background-color","rgba(12,90,126, 1)");
			else $(".module .icon").css("background-color","#333");
			setTimeout(function(){ui.modulepanelblink($(".module .icon").data("blinkcount"));},600);
		}
		else $(".module .icon").removeAttr("style");
	},
	uploaditemfile:function(playlistid,file,progresselement,percentageelement)
	{
		var xhr=new XMLHttpRequest();
		xhr.open('POST', '../../php/audiogallery_managementengine.php', true);
		
		var postdata = new FormData();
		postdata.append("command","uploaditemfile");
		postdata.append("file",file);
		postdata.append("playlistid",playlistid);
		
		xhr.onload = function(e){
			var output = JSON && JSON.parse(this.response) || $.parseJSON(this.response);
			if (output.cmd=="uploaditemfile")
			{
				if (output.success==true)
				{
					ui.showmessage("File uploaded successfully");
					this.successresult=true;
				}
				else
				{
					ui.handleerror(output.data.errorcode);
					this.successresult=false;
				}
			}
			else ui.handleerror(106);	
		};
		xhr.onerror = function(e){
			 alert("File could not be uploaded");
		};
		xhr.upload.onprogress = function(e) {
		if (e.lengthComputable) {
		  percentage = Math.round((e.loaded / e.total) * 100);
		  progresselement.css("width",percentage+'%');
		  percentageelement.text(' '+percentage+'%');
		}
	  };	  
	  xhr.send(postdata);	  
	  return xhr;
	},
	uploadplaylistfile:function(playlistid,file,progresselement,percentageelement)
	{
		var xhr=new XMLHttpRequest();
		xhr.open('POST', '../../php/audiogallery_managementengine.php', true);
		
		var postdata = new FormData();
		postdata.append("command","uploadplaylistfile");
		postdata.append("playlistid",playlistid==null?"auto_generate":playlistid);
		postdata.append("file",file);
		
		xhr.onload = function(e){
			var output = JSON && JSON.parse(this.response) || $.parseJSON(this.response);
			if (output.cmd=="uploadplaylistfile")
			{
				if (output.success==true)
				{
					ui.showmessage("File uploaded successfully");
					this.successresult=true;
				}
				else
				{
					ui.handleerror(output.data.errorcode);
					this.successresult=false;
				}
			}
			else ui.handleerror(106);	
		};
		xhr.onerror = function(e){
			 alert("File could not be uploaded");
		};
		xhr.upload.onprogress = function(e) {
		if (e.lengthComputable) {
		  percentage = Math.round((e.loaded / e.total) * 100);
		  progresselement.css("width",percentage+'%');
		  percentageelement.text(' '+percentage+'%');
		}
	  };	  
	  xhr.send(postdata);	  
	  return xhr;
	},
	uploaduserfile:function(userid,file,progresselement,percentageelement)
	{
		var xhr=new XMLHttpRequest();
		xhr.open('POST', '../../php/audiogallery_managementengine.php', true);
		
		var postdata = new FormData();
		postdata.append("command","uploaduserfile");
		postdata.append("userid",userid==null?"auto_generate":userid);
		postdata.append("file",file);
		
		xhr.onload = function(e){
			var output = JSON && JSON.parse(this.response) || $.parseJSON(this.response);
			if (output.cmd=="uploaduserfile")
			{
				if (output.success==true)
				{
					ui.showmessage("File uploaded successfully");
					this.successresult=true;
				}
				else
				{
					ui.handleerror(output.data.errorcode);
					this.successresult=false;
				}
			}
			else ui.handleerror(106);	
		};
		xhr.onerror = function(e){
			 alert("File could not be uploaded");
		};
		xhr.upload.onprogress = function(e) {
		if (e.lengthComputable) {
		  percentage = Math.round((e.loaded / e.total) * 100);
		  progresselement.css("width",percentage+'%');
		  percentageelement.text(' '+percentage+'%');
		}
	  };	  
	  xhr.send(postdata);	  
	  return xhr;
	},
	scalesite:function ()
	{
		//write code for scaling site and make it a fluid layout website.
	},	
	setupaudioplayer:function(playerselector,audiourl){
		var myCirclePlayer = new CirclePlayer(playerselector,
			{
				mp3: audiourl
			},
			{
				supplied: "mp3",
    			cssSelectorAncestor: "#cp_container_1",
    			swfPath: "../../flash",
    			wmode: "window"
			}
		);
	},
	removeaudioplayer:function(playerselector){
		$(playerselector).jPlayer("destroy");
	},
	attacheventhandlers:function(module){
		switch (module)
		{
			case 'generic':
				$(window).resize(ui.scalesite);
				
				$("#logo img").hover(
					function(){ui.rotatetoplogo(1,'clockwise')},
					function(){ui.rotatetoplogo(0);}
				);
				
				$("#title p").toggle(
					function(){ui.modulepanelslide('up')},
					function(){ui.modulepanelslide('down')}
				);
				
				$('.dialog').draggable(
					{
						handle:'.titlebar',
						containment:'window'
					}
				);
				
				//module panel event handlers
				$('#playlistaudiomodulebtn').click(function(){
					ui.changemodule('playlistaudio');
				});
				
				$('#usersmodulebtn').click(function(){
					ui.changemodule('users');
				});
				
				$('#profilemodulebtn').click(function(){
					ui.changemodule('profile');
				});
				
				$('#logoutbtn').click(function(){
					user.logout();
				});
				//module panel event handlers end
				break;
				
			case 'playlistaudio':
				//playlist section handlers
				//item event handlers------------------------------
				$('#playlistaudiomodule_playlist .container .item').die().live('click',function(){
					ui.selectitem($(this),'playlist');
				});
				
				$('#playlistaudiomodule_playlist .container .item .viewminibtn').die().live('click',function(){
					ui.selectitem($(this).parents('.item'),'playlist');
					$('#playlistaudiomodule_playlist .viewitemdialogbtn').click();
				});
				
				$('#playlistaudiomodule_playlist .container .item .editminibtn').die().live('click',function(){
					ui.selectitem($(this).parents('.item'),'playlist');
					$('#playlistaudiomodule_playlist .edititemdialogbtn').click();
				});
				
				$('#playlistaudiomodule_playlist .container .item .deleteminibtn').die().live('click',function(){
					ui.selectitem($(this).parents('.item'),'playlist');
					$('#playlistaudiomodule_playlist .deleteitemdialogbtn').click();
				});
				//item event handlers end------------------------------
				
				//container bottombar buttons event handlers------------------------------
				$('#playlistaudiomodule_playlist .viewitemdialogbtn').off('click').on('click',function(){
					if ($('#playlistaudiomodule_playlist .viewdialog').css('display')=='none') ui.dialog('playlistaudio','viewdialog','#playlistaudiomodule_playlist .viewdialog','open');
					else ui.dialog('playlistaudio','viewdialog','#playlistaudiomodule_playlist .viewdialog','close')
				});
				
				$('#playlistaudiomodule_playlist .createitemdialogbtn').off('click').on('click',function(){
					if ($('#playlistaudiomodule_playlist .createdialog').css('display')=='none') ui.dialog('playlistaudio','createdialog','#playlistaudiomodule_playlist .createdialog','open');
					else ui.dialog('playlistaudio','createdialog','#playlistaudiomodule_playlist .createdialog','minimize')
				});
				
				$('#playlistaudiomodule_playlist .edititemdialogbtn').off('click').on('click',function(){
					if ($('#playlistaudiomodule_playlist .editdialog').css('display')=='none') ui.dialog('playlistaudio','editdialog','#playlistaudiomodule_playlist .editdialog','open');
					else ui.dialog('playlistaudio','editdialog','#playlistaudiomodule_playlist .editdialog','close');
				});
				
				$('#playlistaudiomodule_playlist .deleteitemdialogbtn').off('click').on('click',function(){
					ui.showloading(ui.selectedplaylistitem,'show');
					user.deleteplaylist(ui.selectedplaylistitem);
				});
				//container bottombar buttons event handlers end------------------------------
				
				//event handlers of the buttons of the dialog
				
				//event handlers of the buttons of the create dialog
				$('#playlistaudiomodule_playlist .createdialog .minimizebtn').off('click').on('click',function(){
					ui.dialog('playlistaudio','createdialog','#playlistaudiomodule_playlist .createdialog','minimize');//write full qualified selector above
				});
				
				$('#playlistaudiomodule_playlist .createdialog .closebtn').off('click').on('click',function(){
					ui.dialog('playlistaudio','createdialog','#playlistaudiomodule_playlist .createdialog','close');//write full qualified selector above
				});				
				
				$('#playlistaudiomodule_playlist .createdialog .element.thumbnail .fileinput .browsebtn input').off('change').on('change',function(evt){
					var file=evt.target.files[0];
					if (!(file.type=='image/jpeg' || file.type=='image/jpg' || file.type=='image/png' || file.type=='image/gif'))
					{
						ui.handleerror(111,"Invalid file selected. Please select a valid image(jpg,png,gif) file.");
						$(this).val(null);
					}
					else
					{
						var convertedfilesize=Math.round((file.size/1024>1024)?(file.size/1048576>1024?file.size/1073741824:file.size/1048576):(file.size/1024));
						var convertedfileunit=file.size/1024>1024?(file.size/1048576>1024?'Gb':'Mb'):'Kb';
						$(this).parent().siblings('.file').children('.filename').html(file.name);
						$(this).parent().siblings('.file').children('.filesize').html(' ('+convertedfilesize+convertedfileunit+')');		
						$(this).parent().siblings('.file').children('.percentage').html(null);
					}
				});
				
				$('#playlistaudiomodule_playlist .createdialog .createbtn').off('click').on('click',function(){
					var dialog=$(this).parents('.dialog');					
					var thumbfileuploadprogresselement=dialog.find('.thumbnail').find('.progressbar');
					var thumbfileuploadpercentageelement=dialog.find('.thumbnail').find('.percentage');
					var name=dialog.find('.name').val();
					var description=dialog.find('.descriptiontext').val();					
					var thumb=dialog.find('.thumbnail').children('.fileinput').find('input[type="file"]').get(0).files[0];
					var thumbxhr=null;
					ui.showloading('#playlistaudiomodule_playlist .createdialog .commandbar','show');
					user.createplaylist(name,description,thumb,thumbfileuploadprogresselement,thumbfileuploadpercentageelement,thumbxhr);
				});				
				//event handlers of the buttons of the create dialog end
				
				//event handlers of the buttons of the edit dialog				
				$('#playlistaudiomodule_playlist .editdialog .minimizebtn').off('click').on('click',function(){
					ui.dialog('playlistaudio','editdialog','#playlistaudiomodule_playlist .editdialog','minimize');
				});
				
				$('#playlistaudiomodule_playlist .editdialog .closebtn').off('click').on('click',function(){
					ui.dialog('playlistaudio','editdialog','#playlistaudiomodule_playlist .editdialog','close');
				});			
				
				$('#playlistaudiomodule_playlist .editdialog .element.thumbnail .fileinput .browsebtn input').off('change').on('change',function(evt){
					var file=evt.target.files[0];
					if (!(file.type=='image/jpeg' || file.type=='image/jpg' || file.type=='image/png' || file.type=='image/gif'))
					{
						ui.handleerror(111,"Invalid file selected. Please select a valid image(jpg,png,gif) file.");
						$(this).val(null);
					}
					else
					{
						var convertedfilesize=Math.round((file.size/1024>1024)?(file.size/1048576>1024?file.size/1073741824:file.size/1048576):(file.size/1024));
						var convertedfileunit=file.size/1024>1024?(file.size/1048576>1024?'Gb':'Mb'):'Kb';
						$(this).parent().siblings('.file').children('.filename').html(file.name);
						$(this).parent().siblings('.file').children('.filesize').html(' ('+convertedfilesize+convertedfileunit+')');		
						$(this).parent().siblings('.file').children('.percentage').html(null);
					}
				});
				
				$('#playlistaudiomodule_playlist .editdialog .editbtn').off('click').on('click',function(){
					var dialog=$(this).parents('.dialog');
					var thumbfileuploadprogresselement=dialog.find('.thumbnail').find('.progressbar');
					var thumbfileuploadpercentageelement=dialog.find('.thumbnail').find('.percentage');
					var playlistid=dialog.find('.id').val();
					var name=dialog.find('.name').val();
					var description=dialog.find('.description').val();									
					var thumb=dialog.find('.thumbnail').children('.fileinput').find('input[type="file"]').get(0).files[0];
					thumb=thumb==undefined?null:thumb;
					var thumbxhr=null;
					ui.showloading('#playlistaudiomodule_playlist .editdialog .commandbar','show');
					user.editplaylist(playlistid,name,description,thumb,thumbfileuploadprogresselement,thumbfileuploadpercentageelement,thumbxhr);
				});
				//event handlers of the buttons of the edit dialog end
				
				//event handlers of the buttons of the view dialog
				$('#playlistaudiomodule_playlist .viewdialog .closebtn').off('click').on('click',function(){
					ui.dialog('playlistaudio','viewdialog','#playlistaudiomodule_playlist .viewdialog','close');//write full qualified selector above
				});
				//event handlers of the buttons of the view dialog end
				
				//event handlers of the buttons of the dialog end
				//event handlers of playlist section end
				
				//audio section event handlers
				//item event handlers------------------------------
				$('#playlistaudiomodule_audio .container .item').die().live('click',function(){
					ui.selectitem($(this),'item');
				});
				
				$('#playlistaudiomodule_audio .container .item .viewminibtn').die().live('click',function(){
					ui.selectitem($(this).parents('.item'),'item');
					$('#playlistaudiomodule_audio .viewitemdialogbtn').click();
				});
				
				$('#playlistaudiomodule_audio .container .item .editminibtn').die().live('click',function(){
					ui.selectitem($(this).parents('.item'),'item');
					$('#playlistaudiomodule_audio .edititemdialogbtn').click();
				});
				
				$('#playlistaudiomodule_audio .container .item .deleteminibtn').die().live('click',function(){
					ui.selectitem($(this).parents('.item'),'item');
					$('#playlistaudiomodule_audio .deleteitemdialogbtn').click();
				});
				//item event handlers end------------------------------
				
				//container bottombar buttons event handlers------------------------------			
				$('#playlistaudiomodule_audio .viewitemdialogbtn').off('click').on('click',function(){
					if ($('#playlistaudiomodule_audio .viewdialog').css('display')=='none') ui.dialog('playlistaudio','viewdialog','#playlistaudiomodule_audio .viewdialog','open');
					else ui.dialog('playlistaudio','viewdialog','#playlistaudiomodule_audio .viewdialog','close')
				});
				
				$('#playlistaudiomodule_audio .createitemdialogbtn').off('click').on('click',function(){
					if ($('#playlistaudiomodule_audio .createdialog').css('display')=='none') ui.dialog('playlistaudio','createdialog','#playlistaudiomodule_audio .createdialog','open');
					else ui.dialog('playlistaudio','createdialog','#playlistaudiomodule_audio .createdialog','minimize')
				});
				
				$('#playlistaudiomodule_audio .edititemdialogbtn').off('click').on('click',function(){
					ui.showmessage("Please delete the audio and create audio again to make the changes.");
				});
				
				$('#playlistaudiomodule_audio .deleteitemdialogbtn').off('click').on('click',function(){
					if (ui.selecteditem!=null)
					{
						ui.showloading(ui.selecteditem,'show');
						user.deleteitem(ui.selecteditem);
					}
					else ui.handleerror(111,"No item selected");
				});
				//container bottombar buttons event handlers end------------------------------
				
				//event handlers of the buttons of the dialog
				
				//event handlers of the buttons of the create dialog
				$('#playlistaudiomodule_audio .createdialog .minimizebtn').off('click').on('click',function(){
					ui.dialog('playlistaudio','createdialog','#playlistaudiomodule_audio .createdialog','minimize');//write full qualified selector above
				});
				
				$('#playlistaudiomodule_audio .createdialog .closebtn').off('click').on('click',function(){
					ui.dialog('playlistaudio','createdialog','#playlistaudiomodule_audio .createdialog','close');//write full qualified selector above
				});				
				
				$('#playlistaudiomodule_audio .createdialog .element.item .fileinput .browsebtn input').off('change').on('change',function(evt){
					var file=evt.target.files[0];
					var validfileselected=false;
					for (i=0;i<ui.itemfileinputfilters.length;i++)
					{
						if (file.type==ui.itemfileinputfilters[i])
						{
							validfileselected=true;
							break;
						}
					}
					if (!validfileselected)
					{
						var extensionlist='-';

						for (i=0;i<ui.itemfileinputfilters.length;i++)
						{
							switch(ui.itemfileinputfilters[i])
							{
								case 'audio/mpeg':
									extensionlist+='mp3-';
									break;					
							}
						}
						ui.handleerror(111,"Invalid file selected. Please select a valid audio("+extensionlist+") file.");
						$(this).val(null);
					}
					else
					{
						var convertedfilesize=Math.round((file.size/1024>1024)?(file.size/1048576>1024?file.size/1073741824:file.size/1048576):(file.size/1024));
						var convertedfileunit=file.size/1024>1024?(file.size/1048576>1024?'Gb':'Mb'):'Kb';
						$(this).parent().siblings('.file').children('.filename').html(file.name);
						$(this).parent().siblings('.file').children('.filesize').html(' ('+convertedfilesize+convertedfileunit+')');		
						$(this).parent().siblings('.file').children('.percentage').html(null);
					}
				});
				
				$('#playlistaudiomodule_audio .createdialog .element.item .fileradioset').off('click').on('click',function(){
					ui.toggledialogelements($(this).children(':checked').val(),'#playlistaudiomodule_audio .createdialog .element.item');
				});
				
				$('#playlistaudiomodule_audio .createdialog .createbtn').off('click').on('click',function(){
					var dialog=$(this).parents('.dialog');
					var itemfileuploadprogresselement=dialog.find('.item').find('.progressbar');
					var itemfileuploadpercentageelement=dialog.find('.item').find('.percentage');
					var title=dialog.find('.titletext').val();
					var description=dialog.find('.descriptiontext').val();
					var itemtype=dialog.find('.fileradioset').children(':checked').val();
					var item=null;
					if (itemtype=='url') item=dialog.find('.item').children('.url').val();
					else if (itemtype=='file') item=dialog.find('.item').children('.fileinput').find('input[type="file"]').get(0).files[0];
					var itemxhr=null;
					if (ui.selectedplaylistitem!=null)
					{
						var playlistid=ui.selectedplaylistitem.attr('data-id');
						ui.showloading('#playlistaudiomodule_audio .createdialog .commandbar','show');
						user.createitem(playlistid,title,description,itemtype,item,itemfileuploadprogresselement,itemfileuploadpercentageelement,itemxhr);
					}
					else ui.handleerror(111,'No playlist is selected');
				});				
				//event handlers of the buttons of the create dialog end
				
				//event handlers of the buttons of the view dialog
				$('#playlistaudiomodule_audio .viewdialog .closebtn').off('click').on('click',function(){
					ui.dialog('playlistaudio','viewdialog','#playlistaudiomodule_audio .viewdialog','close');//write full qualified selector above
				});
				//event handlers of the buttons of the view dialog end
				
				//event handlers of the buttons of the dialog end
				//event handlers of audio section end
				break;
			
			case 'users':
				//item event handlers
				$('#usersmodule .container .item').die().live('click',function(){
					ui.selectitem($(this),'user');
				});
				
				$('#usersmodule .container .item .viewminibtn').die().live('click',function(){
					ui.selectitem($(this).parents('.item'),'user');
					$('#usersmodule .viewitemdialogbtn').click();
				});
				
				$('#usersmodule .container .item .editminibtn').die().live('click',function(){
					ui.selectitem($(this).parents('.item'),'user');
					$('#usersmodule .edititemdialogbtn').click();
				});
				
				$('#usersmodule .container .item .deleteminibtn').die().live('click',function(){
					ui.selectitem($(this).parents('.item'),'user');
					$('#usersmodule .deleteitemdialogbtn').click();
				});
				//item event handlers end
				
				//container bottombar buttons event handlers
				$('#usersmodule .viewitemdialogbtn').off('click').on('click',function(){
					if ($('#usersmodule .viewdialog').css('display')=='none') ui.dialog('users','viewdialog','#usersmodule .viewdialog','open');
					else ui.dialog('users','viewdialog','#usersmodule .viewdialog','close');
				});
				
				$('#usersmodule .createitemdialogbtn').off('click').on('click',function(){
					if ($('#usersmodule .createdialog').css('display')=='none') ui.dialog('users','createdialog','#usersmodule .createdialog','open');
					else ui.dialog('users','createdialog','#usersmodule .createdialog','minimize');
				});
				
				$('#usersmodule .edititemdialogbtn').off('click').on('click',function(){
					if ($('#usersmodule .editdialog').css('display')=='none') ui.dialog('users','editdialog','#usersmodule .editdialog','open');
					else ui.dialog('users','editdialog','#usersmodule .editdialog','close');
				});
				
				$('#usersmodule .deleteitemdialogbtn').off('click').on('click',function(){
					ui.showloading(ui.selecteduseritem,'show');
					user.deleteuser(ui.selecteduseritem);
				});
				//container bottombar buttons event handlers end
				
				//event handlers of the buttons of the dialog				
				//event handlers of the buttons of the create dialog				
				$('#usersmodule .createdialog .minimizebtn').off('click').on('click',function(){
					ui.dialog('users','createdialog','#usersmodule .createdialog','minimize');//write full qualified selector above
				});
				
				$('#usersmodule .createdialog .closebtn').off('click').on('click',function(){
					ui.dialog('users','createdialog','#usersmodule .createdialog','close');//write full qualified selector above
				});			
				
				$('#usersmodule .createdialog .element.thumbnail .fileinput .browsebtn input').off('change').on('change',function(evt){
					var file=evt.target.files[0];
					if (!(file.type=='image/jpeg' || file.type=='image/jpg' || file.type=='image/png' || file.type=='image/gif'))
					{
						ui.handleerror(111,"Invalid file selected. Please select a valid image(jpg,png,gif) file.");
						$(this).val(null);
					}
					else
					{
						var convertedfilesize=Math.round((file.size/1024>1024)?(file.size/1048576>1024?file.size/1073741824:file.size/1048576):(file.size/1024));
						var convertedfileunit=file.size/1024>1024?(file.size/1048576>1024?'Gb':'Mb'):'Kb';
						$(this).parent().siblings('.file').children('.filename').html(file.name);
						$(this).parent().siblings('.file').children('.filesize').html(' ('+convertedfilesize+convertedfileunit+')');		
						$(this).parent().siblings('.file').children('.percentage').html(null);
					}
				});
				
				$('#usersmodule .createdialog .createbtn').off('click').on('click',function(){					
					var dialog=$(this).parents('.dialog');
					var thumbfileuploadprogresselement=dialog.find('.thumbnail').find('.progressbar');
					var thumbfileuploadpercentageelement=dialog.find('.thumbnail').find('.percentage');
					var name=dialog.find('.name').val();
					var username=dialog.find('.username').val();
					var password=dialog.find('.password').val();
					var description=dialog.find('.descriptiontext').val();
					var privilage_createplaylist=dialog.find('.privilage_createplaylist').get(0).checked?1:0;
					var privilage_createaudio=dialog.find('.privilage_createaudio').get(0).checked?1:0;
					var privilage_uploadaudio=dialog.find('.privilage_uploadaudio').get(0).checked?1:0;
					var privilage_usermanagement=dialog.find('.privilage_usermanagement').get(0).checked?1:0;
					var quota_audio=dialog.find('.quota_audio').val();
					var quota_maxaudiosize=dialog.find('.quota_maxaudiosize').val();					
					var thumb=dialog.find('.thumbnail').children('.fileinput').find('input[type="file"]').get(0).files[0];
					var thumbxhr=null;
					ui.showloading('#usersmodule .createdialog .commandbar','show');
					user.createuser(name,username,password,description,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize,thumb,thumbfileuploadprogresselement,thumbfileuploadpercentageelement,thumbxhr);
				});
				//event handlers of the buttons of the create dialog end
				
				//event handlers of the buttons of the edit dialog				
				$('#usersmodule .editdialog .minimizebtn').off('click').on('click',function(){
					ui.dialog('users','editdialog','#usersmodule .editdialog','minimize');
				});
				
				$('#usersmodule .editdialog .closebtn').off('click').on('click',function(){
					ui.dialog('users','editdialog','#usersmodule .editdialog','close');
				});			
				
				$('#usersmodule .editdialog .element.thumbnail .fileinput .browsebtn input').off('change').on('change',function(evt){
					var file=evt.target.files[0];
					if (!(file.type=='image/jpeg' || file.type=='image/jpg' || file.type=='image/png' || file.type=='image/gif'))
					{
						ui.handleerror(111,"Invalid file selected. Please select a valid image(jpg,png,gif) file.");
						$(this).val(null);
					}
					else
					{
						var convertedfilesize=Math.round((file.size/1024>1024)?(file.size/1048576>1024?file.size/1073741824:file.size/1048576):(file.size/1024));
						var convertedfileunit=file.size/1024>1024?(file.size/1048576>1024?'Gb':'Mb'):'Kb';
						$(this).parent().siblings('.file').children('.filename').html(file.name);
						$(this).parent().siblings('.file').children('.filesize').html(' ('+convertedfilesize+convertedfileunit+')');		
						$(this).parent().siblings('.file').children('.percentage').html(null);
					}
				});
				
				$('#usersmodule .editdialog .editbtn').off('click').on('click',function(){
					var dialog=$(this).parents('.dialog');
					var thumbfileuploadprogresselement=dialog.find('.thumbnail').find('.progressbar');
					var thumbfileuploadpercentageelement=dialog.find('.thumbnail').find('.percentage');
					var userid=dialog.find('.id').val();
					var name=dialog.find('.name').val();
					var username=dialog.find('.username').val();
					var password=dialog.find('.password').val()==""?null:dialog.find('.password').val();
					var description=dialog.find('.description').val();
					var privilage_createplaylist=dialog.find('.privilage_createplaylist').get(0).checked?1:0;
					var privilage_createaudio=dialog.find('.privilage_createaudio').get(0).checked?1:0;
					var privilage_uploadaudio=dialog.find('.privilage_uploadaudio').get(0).checked?1:0;
					var privilage_usermanagement=dialog.find('.privilage_usermanagement').get(0).checked?1:0;
					var quota_audio=dialog.find('.quota_audio').val();
					var quota_maxaudiosize=dialog.find('.quota_maxaudiosize').val();					
					var thumb=dialog.find('.thumbnail').children('.fileinput').find('input[type="file"]').get(0).files[0];
					thumb=thumb==undefined?null:thumb;
					var thumbxhr=null;
					ui.showloading('#usersmodule .editdialog .commandbar','show');
					user.edituser(userid,name,username,password,description,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize,thumb,thumbfileuploadprogresselement,thumbfileuploadpercentageelement,thumbxhr);
				});
				//event handlers of the buttons of the edit dialog end
				
				//event handlers of the buttons of the view dialog
				$('#usersmodule .viewdialog .closebtn').off('click').on('click',function(){
					ui.dialog('users','viewdialog','#usersmodule .viewdialog','close');//write full qualified selector above
				});
				//event handlers of the buttons of the view dialog end				
				//event handlers of the buttons of the dialog end
				break;
			case 'profile':
				//event handlers of the buttons of the profile edit section
				$('#profilemodule .userinfo .element.thumbnail .fileinput .browsebtn input').off('change').on('change',function(evt){
					var file=evt.target.files[0];
					if (!(file.type=='image/jpeg' || file.type=='image/jpg' || file.type=='image/png' || file.type=='image/gif'))
					{
						ui.handleerror(111,"Invalid file selected. Please select a valid image(jpg,png,gif) file.");
						$(this).val(null);
					}
					else
					{
						var convertedfilesize=Math.round((file.size/1024>1024)?(file.size/1048576>1024?file.size/1073741824:file.size/1048576):(file.size/1024));
						var convertedfileunit=file.size/1024>1024?(file.size/1048576>1024?'Gb':'Mb'):'Kb';
						$(this).parent().siblings('.file').children('.filename').html(file.name);
						$(this).parent().siblings('.file').children('.filesize').html(' ('+convertedfilesize+convertedfileunit+')');		
						$(this).parent().siblings('.file').children('.percentage').html(null);
					}
				});
				
				$('#profilemodule .editbtn').off('click').on('click',function(){
					var dialog=$(this).parents('.userinfo');
					var thumbfileuploadprogresselement=dialog.find('.thumbnail').find('.progressbar');
					var thumbfileuploadpercentageelement=dialog.find('.thumbnail').find('.percentage');
					var userid=user.info.id;
					var name=dialog.find('.name .value').val();
					var username=dialog.find('.username .value').val();
					var password=dialog.find('.password .value').val()==""?null:dialog.find('.password .value').val();
					var description=dialog.find('.description .value').val();					
					var privilage_createplaylist=user.privilages.createplaylist;
					var privilage_createaudio=user.privilages.createaudio;
					var privilage_uploadaudio=user.privilages.uploadaudio;
					var privilage_usermanagement=user.privilages.usermanagement;
					var quota_audio=user.quota.audio;
					var quota_maxaudiosize=user.quota.maxaudiosize;
					var thumb=dialog.find('.thumbnail').children('.fileinput').find('input[type="file"]').get(0).files[0];
					thumb=thumb==undefined?null:thumb;
					var thumbxhr=null;
					ui.showloading('#profilemodule .userinfo .commandbar','show');
					user.editprofileinfo(userid,name,username,password,description,privilage_createplaylist,privilage_createaudio,privilage_uploadaudio,privilage_usermanagement,quota_audio,quota_maxaudiosize,thumb,thumbfileuploadprogresselement,thumbfileuploadpercentageelement,thumbxhr);
				});
				//event handlers of the buttons of the profile edit section
				break;
		}
	},
	showmessage:function(msg){
		alert(msg);
	},
	handleerror:function(code,msg){ 
		switch (code)
		{
			case 101:
				alert ("(101) You are not authenticated. Please login to continue.");
				this.redirect('login.html');
				break;
			case 102:
				alert("(102) There was some problem accessing the database");
				break;
			case 103:
				alert("(103) Invalid input");
				break;
			case 104:
				alert("(104) You do not have the required privilages to perform that action");
				break;
			case 105:
				alert("(105) The usage quote assigned to you for this action has exhausted");
				break;
			case 106:
				alert("(106) Internal error");
				break;
			case 107:
				alert("(107) Data not ready. Please reload the page");
				break;
			default:
				//here i use custom code and custom message...mostly i use 111 as custom/misc errors
				alert('('+code+') '+msg);
				break;	
		}
	}
}

main();

});