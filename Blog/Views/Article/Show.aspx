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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="content">

<div class="article_title">
    <h1><%= Item.Title %></h1>
</div>

<div class="article_content">
    <%= WriteContent() %>
</div>

<div class="article_footer">
    <%= Item.CreatedOn %> (阅读:<span class="read_count"><%= Item.Statistic.ViewCount %></span>)
    <br />作者：<%= Item.WriterName %>
    <%= Item.Reference == null ? "" : "<br/>引用自：" + Item.Reference %>
    <br />标签：<%= Item.GetTags()%>
    <%
        if (this.IsCurUserOrAdmin(Item.User))
        {
            Response.Write("<br />");
            Response.Write(LinkTo<ArticleController>(p => p.Edit(Item.Id)).Title("编辑"));
        }
    %>
</div>

<a id="comment" name="comment"></a>
<% if (Item.Statistic.CommentsCount > 0) {
      foreach (var comment in Item.Comments) {
%>
<div class="comment_title">
    <%= comment.WriterName %> <%= comment.SavedOn %>
    <div class="comment_action">
    <%
        if (this.IsCurUserOrAdmin(comment.User))
        {
            Response.Write(LinkTo<CommentController>(p => p.Destroy(comment.Id)).Title("删除"));
        }
    %>
    </div>
</div>
<div class="comment_content">
    <%= comment.Content %>
</div>
<% } } %>

<div class="comment_input">
    <a id="comment_input" name="comment_input"></a>
    <form action="<%= UrlTo<CommentController>(p => p.Create(Item.Id)) %>" method="post">
      <p><label for="comment_writer">Writer</label><br />
        <% if (User == null) { %>
          <input id="comment_writer" name="comment[writer]" type="text" maxlength="50" size="50" value="<%=Flash["Writer"]%>" />
        <% } else { %>
          <input id="comment_writer" name="comment[writer]" type="text" maxlength="50" size="50" value="<%= User.ShowName.ToHtml() %>" disabled="disabled" />
        <% } %>
      </p>
      <p><label for="comment_content">Content</label><br /><textarea id="comment_content" name="comment[content]" cols="50" rows="5"><%= Flash["Content"] %></textarea></p>
      <input name="commit" type="submit" value="提交" />
    </form>
</div>

<p style="color: Green"><%= Flash.Notice %></p><p style="color: Red"><%= Flash.Warning %></p>

</div>

</asp:Content>
