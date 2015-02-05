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

<h2>最新更新</h2>
<hr />
<div class="row">
  <div class="col-md-8">
    <% foreach (var o in ItemList.List) { %>
    <div class="panel panel-default">
        <div class="panel-heading"><%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title(o.Title) %></div>
        <div class="panel-body">
    	    <p><%= o.Summary %></p>
    	    <p class="text-right">(<%= o.CreatedOn.ToString("yyyy-MM-dd") %>, 阅读:<span class="read_count"><%= o.Statistic.ViewCount %></span>, 评论:<span class="comment_count"><%= o.Statistic.CommentsCount %></span>) [<%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title("查看全文") %>]</p>
        </div>
    </div>
    <% } %>
    <p class="text-right"><%= LinkTo<ArticleController>(p => p.List(null, null)).Title("历史更新") %></p>
  </div>
  <div class="col-md-4">
    <div class="panel panel-default">
      <div class="panel-heading">置顶</div>
      <div class="panel-body">
        <ul>
            <% foreach (var o in Recommand) { %>
                <li>
                <%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title(o.Title) %>
                </li>
            <% } %>
        </ul>
      </div>
    </div>
    <div class="panel panel-default">
      <div class="panel-heading">阅读排行</div>
      <div class="panel-body">
        <ul>
            <% foreach (var o in ReadRankingList) { %>
                <li>
                <%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title(o.Title) %>
                (<span class="read_count"><%= o.ViewCount %></span>)
                </li>
            <% } %>
        </ul>
      </div>
    </div>
    <div class="panel panel-default">
      <div class="panel-heading">评论排行</div>
      <div class="panel-body">
        <ul>
            <% foreach (var o in CommentRankingList) { %>
                <li>
                <%= LinkTo<ArticleController>(p => p.Show(o.UrlName)).Title(o.Title) %>
                (<span class="comment_count"><%= o.CommentsCount %></span>)
                </li>
            <% } %>
        </ul>
      </div>
    </div>
    <div class="panel panel-default">
      <div class="panel-heading"><%= Link.Name %></div>
      <div class="panel-body">
        <ul>
            <% foreach (var o in Link.Links) { %>
                <li><a href="<%= o.Url %>" target="_blank"><%= o.Title %></a></li>
            <% } %>
        </ul>
      </div>
    </div>
  </div>
</div>

</asp:Content>
