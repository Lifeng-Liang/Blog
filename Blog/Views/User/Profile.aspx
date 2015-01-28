<%@ Page Title="个人资料" Language="C#" MasterPageFile="~/main.master" %>

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
h3 { margin:30px 40px 20px 40px; }
    </style>
    <script type="text/javascript">
        window.onload = function() {
            var c = document.getElementById("username");
            c.focus();
            c.select();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" action="<%= UrlTo<UserController>(p => p.Profile()) %>" method="post">
        <div id="center">
        <div id="mainPanel">
            <p><%= this.GetLoginUser().Role.EnumToString() %></p>
            <h3>不填写密码部分，则不会修改密码，不填写名字部分，则不会修改名字，两者不能同时进行。</h3>
            <table border="0" style="margin-left:130px; margin-right:auto; margin-top:10px">
                <tr><td class="title">老密码:</td><td class="inputbox"><input id="password" name="password" size="20" type="password" maxlength="99" /></td></tr>
                <tr><td class="title">新密码:</td><td class="inputbox"><input id="password1" name="password1" size="20" type="password" maxlength="99" /></td></tr>
                <tr><td class="title">确认密码:</td><td class="inputbox"><input id="password2" name="password2" size="20" type="password" maxlength="99" /></td></tr>
                <tr><td class="title">名字:</td><td class="inputbox"><input id="showname" name="showname" size="20" type="text" maxlength="50" /></td></tr>
            </table>
            <input name="commit" type="submit" value=" 修改 " />
            <%= this.GetLoginUser().Role == UserRole.NonactivatedUser ? " | " + LinkTo<UserController>(p =>p.SendActiveMail()).Title("重新发送激活邮件") : "" %>
        </div>
        <p style="color: Red"><%= Flash.Warning %></p><p style="color: Green"><%= Flash.Notice %></p>
        </div>
    </form>
</asp:Content>
