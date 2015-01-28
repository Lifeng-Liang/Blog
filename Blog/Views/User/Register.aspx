<%@ Page Title="注册" Language="C#" MasterPageFile="~/main.master" %>

<script runat="server">
    public string RegTimeX;
    public string RegTimeY;
    public string RegTimeZ;
    public string RegValidate;
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
.title
{
	text-align: right;
	font-weight: bold;
	width:80px;
}
.inputbox
{
	text-align: left;
}
.ErrMsg
{
    color: red;
	font-weight: bold;
}
#center
{
    height:450px;
    width:auto;
    text-align:center;
    background-color:white;
    padding-top:100px;
    margin-left:20px;
    margin-right:20px;
}
#mainPanel
{
    margin:0 auto;
    text-align: center;
    width:480px;
    height: 272px;
    background-color:#f0f0f0;
    border:solid 1px #ccccff;
}
h3 { margin:30px; }
    </style>
    <script type="text/javascript">
        window.onload = function() {
            var c = document.getElementById("email");
            c.focus();
            c.select();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" action="<%= UrlTo<UserController>(p => p.Register()) %>" method="post">
        <div id="center">
        <div id="mainPanel">
            <h3>注册时使用Email作为帐号，系统会发送一封激活邮件到这个邮箱，名字只做显示之用，注册用户可以删除自己发表的评论，不过，只有激活用户一天之后才能发表带url的评论。普通用户（含未注册用户）只能使用一个关键词查询标题，只有激活用户一天后才能使用高级查询，使用多个关键词查询标题和内容。</h3>
            <table border="0" style="margin-left:130px; margin-right:auto; margin-top:0px">
                <tr><td class="title">Email:</td><td class="inputbox"><input id="email" name="email" size="20" type="text" maxlength="128" /></td></tr>
                <tr><td class="title">密码:</td><td class="inputbox"><input id="password" name="password" size="20" type="password" maxlength="99" /></td></tr>
                <tr><td class="title">名字:</td><td class="inputbox"><input id="showname" name="showname" size="20" type="text" maxlength="50" /></td></tr>
            </table>
            <script type="text/javascript">
                var t = <%= RegTimeY %> + <%= RegTimeZ %>;
                document.writeln('<input type="hidden" name="time" value="<%= RegTimeX %>' + t + '"/>');
            </script>
            <input type="hidden" name="validate" value="<%= RegValidate %>"/>
            <input name="commit" type="submit" value=" 注册 " />
        </div>
        <p style="color: Red"><%= Flash.Warning %></p>
        </div>
    </form>
</asp:Content>
