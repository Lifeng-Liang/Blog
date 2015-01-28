// menu
$(document).ready(function(){
	$("#menu li").hover(
		function(){ $("ul", this).fadeIn("fast"); }, 
		function() { } 
	);
	if (document.all) {
		$("#menu li").hoverClass("sfHover");
		$("#menu ul li").hoverClass("mhover");
	}
});
$.fn.hoverClass = function(c) {
	return this.each(function(){
		$(this).hover( 
			function() { $(this).addClass(c); },
			function() { $(this).removeClass(c); }
		);
	});
};	  
// remark
function checkRemark()
{
var form=document.all.remarkForm;
if (form.body.value=="")
{	alert("请填写评论内容");
	form.body.focus();
	return false;
}
if (form.username.value=="")
{	alert("请填写姓名");
	form.username.focus();
	return false;
}
if (form.body.value.length>200)
{	alert("评论内容不可以超过200字");
	form.body.focus();
	return false;
}
if (form.username.value.length>10)
{	alert("姓名不可以超过10个字");
	form.username.focus();
	return false;
}
form.submit.disabled=true;
return true;
}
function showLen(obj)
{
	document.all.bodyLen.innerText=obj.value.length;
}
// maxLenght for textarea
function checkMaxLen(obj, maxlength) {
    if (obj.value.length > maxlength) {
        obj.value = obj.value.substr(0, maxlength);
    }
}
