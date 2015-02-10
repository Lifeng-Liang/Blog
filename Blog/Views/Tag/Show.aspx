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

<h2>标签：<%= Tag.Name %></h2>
<hr />

<% foreach (var o in ItemList.List)
   { %>
<div class="panel panel-default">
    <div class="panel-heading"><%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title(o.Title) %></div>
    <div class="panel-body">
    	<p><%= o.Summary %></p>
    	<p class="text-right">(<%= o.HowLong() %>) [<%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title("查看全文") %>]</p>
    </div>
</div>
<% } %>

<p class="text-right">
<%
   foreach (var i in ItemList.PageLinks(ListStyle.Hybird, 11))
   {
       Response.Write("&nbsp;");
       Response.Write(i < 0 ? "..." : i == ItemList.PageIndex ? (i == 0 ? "第一页" : i.ToString())
           : i == 0 ? LinkTo<TagController>(p => p.Show(Tag.Name, null)).Title("第一页")
           : LinkTo<TagController>(p => p.Show(Tag.Name, i)).Title(i.ToString()));
   }
%>
</p>

</asp:Content>
