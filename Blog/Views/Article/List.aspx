﻿<%@ Page Title="历史更新" Language="C#" MasterPageFile="~/main.master" %>

<script runat="server">
    public ItemList<Article> ItemList;
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<% foreach (var o in ItemList.List) { %>
<div class="panel panel-default">
    <div class="panel-heading"><%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title(o.Title) %></div>
    <div class="panel-body">
    	<p><%= o.Summary %></p>
    	<p class="text-right">(<%= o.CreatedOn.ToString("yyyy-MM-dd") %>, 阅读:<span class="label label-info"><%= o.Statistic.ViewCount %></span>, 评论:<span class="comment_count"><%= o.Statistic.CommentsCount %></span>) [<%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title("查看全文") %>]</p>
    </div>
</div>
<% } %>

<p class="text-right">
<%
   foreach (var i in ItemList.PageLinks(ListStyle.Static, 11))
   {
       Response.Write("&nbsp;");
       Response.Write(i < 0 ? "..." : i == ItemList.PageIndex ? i.ToString()
           : i == ItemList.PageCount ? LinkTo<ArticleController>(p => p.List(null, null)).Title(i.ToString()) : LinkTo<ArticleController>(p => p.List(i, null)).Title(i.ToString()));
   }
%>
</p>

</asp:Content>
