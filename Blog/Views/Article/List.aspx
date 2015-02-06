<%@ Page Title="历史更新" Language="C#" MasterPageFile="~/main.master" %>

<script runat="server">
    public ItemList<Article> ItemList;
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<% this.RenderArticleList(ItemList.List); %>

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
