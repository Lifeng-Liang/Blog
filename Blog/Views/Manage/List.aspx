<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" %>

<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="content">

<p><%= LinkTo<ManageController>(p => p.RunSql()).Title("运行SQL语句") %></p>
<p><%= LinkTo<ManageController>(p => p.Memory()).Title("查询内存占用") %></p>
<p><%= LinkTo<ManageController>(p => p.Backup()).Title("备份数据库") %></p>
<p><%= LinkTo<UserController>(p => p.List(null, null)).Title("用户管理") %></p>

<% this.RenderFlash(); %>

</div>

</asp:Content>
