<%@ Page Title="新文章" Language="C#" MasterPageFile="~/main.master" %>

<script runat="server">
    public IList<Category> List;
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="<%= Blog.Biz.BlogSettings.SiteBase %>/scripts/common.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="content">

<div class="edit_title">
  <h1>新文章</h1>
</div>

<form action="<%= UrlTo<ArticleController>(p => p.Create()) %>" method="post" class="edit_content">
  <p><label for="article_category">分类：</label>
    <select id="article_category" name="article[category]">
    <%
        var options = HtmlBuilder.New;
        foreach (var category in List)
        {
            options.tag("option").value(category.Id).text(category.Name).end.over();
        }
        Response.Write(options.ToString());
    %>
    </select>
    <label for="article_format">格式：</label><select id="article_format" name="article[format]"><option value="Html">Html</option><option value="Ubb">Ubb</option><option value="Markdown" selected="selected">Markdown</option></select>
    <label for="article_recommend">推荐：</label><input id="article_recommend" name="article[recommend]" type="checkbox" />
    <label for="article_writer">作者：</label><input id="article_writer" name="article[writer]" type="text" maxlength="50" size="20" />
  </p>
  <p><label for="article_reference">引用：</label><input id="article_reference" name="article[reference]" type="text" maxlength="255" size="94" /></p>

  <p><label for="article_title">标题：</label><br /><input id="article_title" name="article[title]" type="text" maxlength="255" size="100" /></p>
  <p><label for="article_alias">别名：</label><br /><input id="article_alias" name="article[alias]" type="text" maxlength="255" size="100" /></p>

  <p><label for="article_content">内容：</label><br /><textarea id="article_content" name="article[content]" cols="100" rows="20"></textarea></p>
  <p><label for="article_summary">摘要：</label><br /><textarea id="article_summary" name="article[summary]" cols="100" rows="5" onchange="checkMaxLen(this,20);"></textarea></p>

  <p><label for="article_tags">标签：</label><br /><input id="article_tags" name="article[tags]" type="text" maxlength="255" size="100" /></p>

  <div class="edit_action">
    <input name="commit" type="submit" value="创建" /> | 
    <%= LinkTo<ArticleController>(p => p.List(null, null)).Title("Back") %>
  </div>
</form>
<br />

</div>

</asp:Content>
