<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" %>

<script runat="server">
    public Article Item;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = string.Format("{0}-{1}-{2}", Item.Title, Item.Category.Name, Blog.Biz.BlogSettings.BlogName);
    }

    private string WriteContent()
    {
        switch(Item.Format)
        {
            case ArticleFormat.Html:
                return Item.Content;
            case ArticleFormat.Ubb:
                return Item.Content.UbbEncode();
            case ArticleFormat.Markdown:
                return Item.Content.MarkdownEncode();
            default:
                throw new Exception("系统错误");
        }
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <% if(Item.Format == ArticleFormat.Markdown) { %>
    <link href="<%= Blog.Biz.BlogSettings.SiteBase %>/prettify/my.css" type="text/css" rel="stylesheet" />
    <script src="<%= Blog.Biz.BlogSettings.SiteBase %>/prettify/prettify.js" type="text/javascript"></script>
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="page-header">
<h1><%= Item.Title %></h1>
</div>

<blockquote class="bs-callout-danger">
    <p>作者：<%= Item.WriterName %>
    <%= Item.GetTags("标签：")%></p>
    <%= Item.Reference == null ? "" : "<div class=\"alert alert-warning\" role=\"alert\">引用自：" + Item.Reference + "</div>" %>
    <%
        if (this.IsCurUserOrAdmin(Item.User))
        {
            Response.Write(LinkTo<ArticleController>(p => p.Edit(Item.Id)).Title("编辑"));
        }
    %>
    <footer><%= Item.HowLong() %> (阅读:<span class="text-danger"><%= Item.Statistic.ViewCount %></span>)</footer>
</blockquote>

<%= WriteContent() %>

<hr />

<a id="comment" name="comment"></a>
<% if (Item.Statistic.CommentsCount > 0) {
      foreach (var comment in Item.Comments) {
%>
<div class="panel panel-default">
    <div class="panel-heading"><%= comment.WriterName %> <%= comment.SavedOn %>
    <%
        if (this.IsCurUserOrAdmin(comment.User))
        {
            Response.Write(LinkTo<CommentController>(p => p.Destroy(comment.Id)).Title("删除"));
        }
    %>
    </div>
    <div class="panel-body">
    	<p><%= comment.Content %></p>
    </div>
</div>
<% } } %>

<hr />

<a id="comment_input" name="comment_input"></a>
<form action="<%= UrlTo<CommentController>(p => p.Create(Item.Id)) %>" method="post">
    <div class="form-group">
        <label for="comment_writer">Writer</label>
        <% if (User == null) { %>
            <input id="comment_writer" name="comment[writer]" type="text" class="form-control" maxlength="50" size="50" value="<%=Flash["Writer"]%>" />
        <% } else { %>
            <input id="comment_writer" name="comment[writer]" type="text" class="form-control" maxlength="50" size="50" value="<%= User.ShowName.ToHtml() %>" disabled="disabled" />
        <% } %>
    </div>
    <div class="form-group">
        <label for="comment_content">Content</label>
        <textarea id="comment_content" name="comment[content]" class="form-control" cols="50" rows="5"><%= Flash["Content"] %></textarea>
    </div>
    <button name="commit" type="submit" class="btn btn-default">提交</button>
</form>

<% this.RenderFlash(); %>

<% if(Item.Format == ArticleFormat.Markdown) { %>
<script type="text/javascript">prettyPrint();</script>
<% } %>

</asp:Content>
