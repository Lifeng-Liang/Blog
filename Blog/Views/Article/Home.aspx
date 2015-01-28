<%@ Page Title="首页" Language="C#" MasterPageFile="~/main.master" %>

<script runat="server">
    public ItemList<Article> ItemList;
    public List<ArticleStatistic> CommentRankingList;
    public List<ArticleStatistic> ReadRankingList;
    public Category Link;
    public List<Article> Recommand;
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<p style="color: Green; margin-left:20px;"><%= Flash.Notice %></p>

<table class="home"><tr><td class="home_list" valign="top">

<table>
<tr>
  <th class="table_title">最新更新</th>
</tr>
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
    <%= LinkTo<ArticleController>(p => p.List(null, null)).Title("历史更新") %>
  </td>
</tr>
</table>

</td><td class="home_widget" valign="top">

<table>
<tr>
  <th class="table_title">搜索</th>
</tr>
<tr>
  <td class="tablecontent">
    <div class="search_bar">
        <form id="form1" action="<%= UrlTo<SearchController>(p => p.Article(null)) %>" method="post">
            <input id="q" name="q" type="text" size="18" maxlength="30" />
            <input type="submit" value="  搜索  " class="search_button" />
        </form>
		<form method="get" action="http://www.google.com/search" style="display:inline">
			<input type="hidden" name="sitesearch" value="http://llf.hanzify.org" />
			<input type="text" name="q" size="18" />
			<input type="submit" value=" Google " class="search_button" />
		</form>
    </div>
  </td>
</tr>
</table>

<table>
<tr>
  <th class="table_title"><%= Link.Name %></th>
</tr>
<tr>
  <td class="table_content">
    <ul>
        <% foreach (var o in Link.Links) { %>
            <li><a href="<%= o.Url %>" target="_blank"><%= o.Title %></a></li>
        <% } %>
    </ul>
  </td>
</tr>
</table>

<table>
<tr>
  <th class="table_title">推荐</th>
</tr>
<tr>
  <td class="table_content">
    <ul>
        <% foreach (var o in Recommand) { %>
            <li>
            <%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title(o.Title) %>
            </li>
        <% } %>
    </ul>
  </td>
</tr>
</table>

<table>
<tr>
  <th class="table_title">阅读排行</th>
</tr>
<tr>
  <td class="table_content">
    <ul>
        <% foreach (var o in ReadRankingList) { %>
            <li>
            <%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title(o.Title) %>
            (<span class="read_count"><%= o.ViewCount %></span>)
            </li>
        <% } %>
    </ul>
  </td>
</tr>
</table>

<table>
<tr>
  <th class="table_title">评论排行</th>
</tr>
<tr>
  <td class="table_content">
    <ul>
        <% foreach (var o in CommentRankingList) { %>
            <li>
            <%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title(o.Title) %>
            (<span class="comment_count"><%= o.CommentsCount %></span>)
            </li>
        <% } %>
    </ul>
  </td>
</tr>
</table>

</td></tr></table>

<br />

</asp:Content>
