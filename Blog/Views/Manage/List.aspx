<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" %>

<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="content">

<%= LinkTo<ManageController>(p => p.RunSql()).Title("运行SQL语句") %><br />
<%= LinkTo<ManageController>(p => p.Memory()).Title("查询内存占用") %><br />
<%= LinkTo<ManageController>(p => p.Backup()).Title("备份数据库") %><br />
<%= LinkTo<UserController>(p => p.List(null, null)).Title("用户管理") %><br />

<p style="color: green;"><%= Flash.Notice %></p>
<p style="color: red;"><%= Flash.Warning %></p>

</div>

</asp:Content>
