<%@ Page Title="注册" Language="C#" MasterPageFile="~/main.master" %>

<script runat="server">
    public string RegTimeX;
    public string RegTimeY;
    public string RegTimeZ;
    public string RegValidate;
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        window.onload = function() {
            var c = document.getElementById("email");
            c.focus();
            c.select();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="row">
    <div class="col-md-2">.</div>
    <div class="col-md-8">
    <h2 class="text-center">注册</h2>
    <hr />
    <div class="alert alert-warning" role="alert">注册时使用Email作为帐号，系统会发送一封激活邮件到这个邮箱，名字只做显示之用，注册用户可以删除自己发表的评论，不过，只有激活用户一天之后才能发表带url的评论。普通用户（含未注册用户）只能使用一个关键词查询标题，只有激活用户一天后才能使用高级查询，使用多个关键词查询标题和内容。</div>
    <form id="form1" action="<%= UrlTo<UserController>(p => p.Register()) %>" method="post" class="form-horizontal">
        <div class="form-group">
            <label for="email" class="col-sm-2 control-label">Email:</label>
            <div class="col-sm-10">
            <input id="email" name="email" class="form-control" size="20" type="text" maxlength="128" />
            </div>
        </div>
        <div class="form-group">
            <label for="password" class="col-sm-2 control-label">密码:</label>
            <div class="col-sm-10">
            <input id="password" name="password" class="form-control" size="20" type="password" maxlength="99" />
            </div>
        </div>
        <div class="form-group">
            <label for="showname" class="col-sm-2 control-label">名字:</label>
            <div class="col-sm-10">
            <input id="showname" name="showname" class="form-control" size="20" type="text" maxlength="50" />
            </div>
        </div>
        <script type="text/javascript">
            var t = <%= RegTimeY %> + <%= RegTimeZ %>;
            document.writeln('<input type="hidden" name="time" value="<%= RegTimeX %>' + t + '"/>');
        </script>
        <input type="hidden" name="validate" value="<%= RegValidate %>"/>
        <div class="form-group"><div class="col-sm-offset-2 col-sm-10">
        <button type="submit" class="btn btn-default">注册</button>
        </div></div>
    </form>
    <% this.RenderFlash(); %>
    </div>
    <div class="col-md-2">.</div>
</div>
</asp:Content>
