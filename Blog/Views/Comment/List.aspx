<%@ Page Title="评论" Language="C#" MasterPageFile="~/main.master" %>

<script runat="server">
    public ItemList<Comment> ItemList;
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="content">

<p style="color: Green"><%= Flash.Notice %></p>

<h1>Listing Comments</h1>

<p><%= LinkTo<RssController>(p => p.Comment()).Title("订阅评论") %></p>

<table>
<% foreach (var o in ItemList.List) { %>
<tr>
  <td class="comment_title">
    <%= LinkTo<ArticleController>(p => p.Show(o.Article.UrlName)).Title(o.Article.Title) %>
    <%= o.WriterName %> (<%= o.SavedOn %>)
    <% if (this.IsCurUserOrAdmin(o.User)) { %>
    <div class="comment_action">
        <%= LinkTo<CommentController>(p => p.Destroy(o.Id)).Title("删除").Addon("onclick=\"if (confirm('Are you sure?')) { var f = document.createElement('form'); f.style.display = 'none'; this.parentNode.appendChild(f); f.method = 'POST'; f.action = this.href;f.submit(); };return false;\"") %>
    </div>
    <% } %>
  </td>
</tr>
<tr>
  <td class="comment_content"><%= o.Content %></td>
</tr>
<% } %>
<tr>
    <td class="page_bar">
<%
   foreach (var i in ItemList.PageLinks(ListStyle.Hybird, 11))
   {
       Response.Write("&nbsp;");
       Response.Write(i < 0 ? "..." : i == ItemList.PageIndex ? (i == 0 ? "第一页" : i.ToString())
           : i == 0 ? LinkTo<CommentController>(p => p.List(null)).Title("第一页")
           : LinkTo<CommentController>(p => p.List(i)).Title(i.ToString()));
   }
%>
    </td>
</tr>
</table>


<br />

</div>

</asp:Content>
