<%@ Page Title="登录" Language="C#" MasterPageFile="~/main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () { $("#email").focus().select(); });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-md-2">.</div>
    <div class="col-md-8">
    <h2 class="text-center">登录</h2>
    <hr />
    <form id="form1" action="<%= UrlTo<UserController>(p => p.Login()) %>" method="post" class="form-horizontal">
        <div class="form-group">
            <label for="email" class="col-sm-2 control-label">Email:</label>
            <div class="col-sm-10">
            <input id="email" name="email" class="form-control" size="20" type="text" maxlength="128" />
            </div>
        </div>
        <div class="form-group">
            <label for="email" class="col-sm-2 control-label">Password:</label>
            <div class="col-sm-10">
            <input id="password" name="password" class="form-control" size="20" type="password" maxlength="99" />
            </div>
        </div>
        <div class="form-group"><div class="col-sm-offset-2 col-sm-10"><div class="checkbox">
            <label><input type="checkbox" id="rememberme" name="rememberme" /> Remember me </label>
        </div></div></div>
        <div class="form-group"><div class="col-sm-offset-2 col-sm-10">
            <button type="submit" class="btn btn-default">登录</button>
        </div></div>
    </form>
    <% this.RenderFlash(); %>
    </div>
    <div class="col-md-2">.</div>
</asp:Content>
