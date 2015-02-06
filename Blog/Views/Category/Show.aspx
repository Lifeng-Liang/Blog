<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" %>

<script runat="server">
    public ItemList<Article> ItemList;
    public Category Category;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = Category.Name + "-" + Blog.Biz.BlogSettings.BlogName;
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h2><%= Category.Name %></h2>
<hr />

<% this.RenderArticleList(ItemList.List); %>

<p class="text-right">
<%
   foreach (var i in ItemList.PageLinks(ListStyle.Hybird, 11))
   {
       Response.Write("&nbsp;");
       Response.Write(i < 0 ? "..." : i == ItemList.PageIndex ? (i == 0 ? "第一页" : i.ToString())
           : i == 0 ? LinkTo<CategoryController>(p => p.Show(Category.UrlName, null)).Title("第一页")
           : LinkTo<CategoryController>(p => p.Show(Category.UrlName, i)).Title(i.ToString()));
   }
%>
</p>

</asp:Content>

