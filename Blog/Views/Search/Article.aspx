<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" %>

<script runat="server">
    public List<Article> List;
    public long ListPageCount;
    public long ListPageIndex;
    public string Keyword;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "搜索：" + Keyword;
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<p></p>

<div class="category_list">
<table>
<tr>
  <th class="table_title">搜索：<%= Keyword %></th>
</tr>
<% foreach (var o in List) { %>
<tr>
  <td class="table_subtitle">
    [<%= LinkTo<CategoryController>(p => p.Show(o.Category.UrlName, null)).Title(o.Category.Name) %>]
    <%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title(o.Title) %></td>
</tr>
<tr>
  <td class="table_content"><%= o.Summary %></td>
</tr>
<tr>
  <td class="table_subfooter">(<%= o.CreatedOn.ToString("yyyy-MM-dd") %>, <span class="read_count"><%= o.Statistic.ViewCount %></span>) [<%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title("查看全文") %>]</td>
</tr>
<% } %>
<tr>
  <td class="table_footer">
<%
   foreach (var i in CommonHelper.PageLinks(ListPageCount, ListPageIndex, ListStyle.Static, 11))
   {
       Response.Write("&nbsp;");
       Response.Write(i < 0 ? "..." : i == ListPageIndex ? i.ToString() 
           : LinkTo<SearchController>(p => p.Article(i)).Title(i.ToString()).UrlParam("q", Keyword));
   }
%>
  </td>
</tr>
</table>

<p style="color: Red"><%= Flash.Warning %></p>

</div>

<br />

</asp:Content>
