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

<h2>搜索：<%= Keyword %></h2>
<hr />

<% foreach (var o in List) { %>
<div class="panel panel-default">
    <div class="panel-heading"><%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title(o.Title) %></div>
    <div class="panel-body">
    	<p><%= o.Summary %></p>
    	<p class="text-right">(<%= o.CreatedOn.ToString("yyyy-MM-dd") %>, 阅读:<span class="label label-info"><%= o.Statistic.ViewCount %></span>, 评论:<span class="label label-info"><%= o.Statistic.CommentsCount %></span>) [<%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title("查看全文") %>]</p>
    </div>
</div>
<% } %>

<p class="text-right">
<%
   foreach (var i in CommonHelper.PageLinks(ListPageCount, ListPageIndex, ListStyle.Static, 11))
   {
       Response.Write("&nbsp;");
       Response.Write(i < 0 ? "..." : i == ListPageIndex ? i.ToString() 
           : LinkTo<SearchController>(p => p.Article(i)).Title(i.ToString()).UrlParam("q", Keyword));
   }
%>
</p>

<p style="color: Red"><%= Flash.Warning %></p>

<br />

</asp:Content>
