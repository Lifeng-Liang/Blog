<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" %>

<script runat="server">
    public ItemList<ArticleTag> ItemList;
    public Tag Tag;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "标签：" + Tag.Name;
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<p></p>

<div class="category_list">
<table>
<tr>
  <th class="table_title">标签：<%= Tag.Name %></th>
</tr>
<% foreach (var o in ItemList.List) { %>
<tr>
  <td class="table_subtitle">
    <%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title(o.Title) %></td>
</tr>
<tr>
  <td class="table_content"><%= o.Summary %></td>
</tr>
<tr>
  <td class="table_subfooter">(<%= o.CreatedOn.ToString("yyyy-MM-dd") %>) [<%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title("查看全文") %>]</td>
</tr>
<% } %>
<tr>
  <td class="table_footer">
<%
   foreach (var i in ItemList.PageLinks(ListStyle.Hybird, 11))
   {
       Response.Write("&nbsp;");
       Response.Write(i < 0 ? "..." : i == ItemList.PageIndex ? (i == 0 ? "第一页" : i.ToString())
           : i == 0 ? LinkTo<TagController>(p => p.Show(Tag.Name, null)).Title("第一页")
           : LinkTo<TagController>(p => p.Show(Tag.Name, i)).Title(i.ToString()));
   }
%>
  </td>
</tr>
</table>
</div>

<br />

</asp:Content>
