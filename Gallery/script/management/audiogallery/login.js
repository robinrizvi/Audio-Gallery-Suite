$(document).ready(function(){
	
	$("body").width(window.innerWidth);
	$("body").height(window.innerHeight);
	
	$("#username").click(function(e){
		e.stopPropagation();
		if (this.value=='Username') this.value='';
		else this.select();
		//$("body").css("background-image","url(../../image/management_images/stripe.png)");
		$("body").css("background-color","#171717");
	});
		
	$("#username").blur(function(){
		if (this.value=='') this.value='Username';
	});
	
	$("#password").click(function(e){
		e.stopPropagation();
		if (this.value=='Password') this.value='';
		else this.select();
		//$("body").css("background-image","url(../../image/management_images/stripe.png)");
		$("body").css("background-color","#171717");
	});
		
	$("#password").blur(function(){
		if (this.value=='') this.value='Password';
	});
	
	$("#login-box,body").click(function(){
		//$("body").css("background-image","url(../../image/management_images/pattern.png)");
		$("body").css("background-color","#333");
	});
	
	$("#loginbtn").click(function(){
		if (validateinput($("#username").val(),$("#password").val()))
		{
			changestatus(2);
			var data="command=auth&username="+$("#username").val()+"&password="+$("#password").val();
			$.ajax({
			type: "POST",
			url: "../../php/audiogallery_managementengine.php",
			data: data,
			success: 
				function(json)
				{
					output = JSON && JSON.parse(json) || $.parseJSON(json);//eval could be used here, it would be fast..but due to security
					if (output.cmd=="auth")
						if (output.success==true)
							if (output.data.valid==true)
							{
								changestatus(4);
								setTimeout("window.location='management.html'",2000);	
							}
							else changestatus(3);
						else changestatus(3);
					else changestatus(3);
				}
			});
		}
		else
			changestatus(3);
	});
	
	function validateinput(username,password)
	{
		var valid=false;
		if ((username!='Username' && username!='') && (password!='Passsword' && password!='')) valid=true;
		return valid;
	}

	function changestatus(current)
	{
		switch (current)
		{
			case 1:
				$(".locked").show();
				$(".loading").hide();
				$(".invalid").hide();
				break;
			case 2:
				$(".locked").hide();
				$(".loading").show();
				$(".invalid").hide();
				break;
			case 3:
				$(".locked").hide();
				$(".loading").hide();
				$(".invalid").show();
				$(".invalid").fadeTo(
					3000,
					0,
					function(){						
						$(".invalid").css("opacity","1");
						changestatus(1);
					}
				);
				break;
			case 4:
				$(".locked").hide();
				$(".loading").hide();
				$(".invalid").hide();
				$(".unlocked").show();
				break;
		}
	}
});