<%@ Page Title="历史更新" Language="C#" MasterPageFile="~/main.master" %>

<script runat="server">
    public ItemList<Article> ItemList;
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<p></p>

<div class="category_list">
<table>
<% foreach (var o in ItemList.List) { %>
<tr>
  <td class="table_subtitle">
    [<%= LinkTo<CategoryController>(p => p.Show(o.Category.UrlName, null)).Title(o.Category.Name) %>]
    <%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title(o.Title) %></td>
</tr>
<tr>
  <td class="table_content"><%= o.Summary %></td>
</tr>
<tr>
  <td class="table_subfooter">(<%= o.CreatedOn.ToString("yyyy-MM-dd") %>, 阅读:<span class="read_count"><%= o.Statistic.ViewCount %></span>, 评论:<span class="comment_count"><%= o.Statistic.CommentsCount %></span>) [<%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title("查看全文") %>]</td>
</tr>
<% } %>
<tr>
  <td class="table_footer">
<%
   foreach (var i in ItemList.PageLinks(ListStyle.Static, 11))
   {
       Response.Write("&nbsp;");
       Response.Write(i < 0 ? "..." : i == ItemList.PageIndex ? i.ToString()
           : i == ItemList.PageCount ? LinkTo<ArticleController>(p => p.List(null, null)).Title(i.ToString()) : LinkTo<ArticleController>(p => p.List(i, null)).Title(i.ToString()));
   }
%>
  </td>
</tr>
</table>
</div>

<br />

</asp:Content>
