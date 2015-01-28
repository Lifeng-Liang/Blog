<%@ Page Language="C#" ContentType="text/xml" %><?xml version="1.0"?>
<?xml-stylesheet type='text/xsl' href='inc/rsspretty.xml' version='1.0'?>
<rss version="2.0" xmlns:slash="http://purl.org/rss/1.0/modules/slash/" xmlns:msn="http://schemas.microsoft.com/msn/spaces/2005/rss">

<script runat="server">
    public List<Comment> List;
    private const string Site = "http://llf.hanzify.org";
    private readonly string SiteBase = Site + Blog.Biz.BlogSettings.SiteBase;
    
    private void w(string text)
    {
        Response.Write(text);
    }

    private void w(string template, params object[] p)
    {
        Response.Write(string.Format(template, p));
    }
</script>

<channel>
<title><%= Blog.Biz.BlogSettings.BlogName %> - 评论</title>
<description>think twice</description>
<link><%= Site %></link>
<language>zh-CN</language>
<pubDate><%= DateTime.Now.ToString() %></pubDate>
<lastBuildDate><%= DateTime.Now.ToString() %></lastBuildDate>
<generator><%= Blog.Biz.BlogSettings.BlogName %> RSS 生成程序</generator>
<ttl>60</ttl>

<%
    foreach (var o in List)
    {
		w("<item>\n");
        w("<title>{0}</title>\n", o.Article.Title.ToHtml());
        w("<link>{0}/article/show/{1}</link>\n", SiteBase, o.Article.UrlName);
        w("<description>{0}......<br/><a target='_blank' href='{1}/article/show/{2}'>阅读全文</a></description>\n",
            o.Content.ToHtml(), SiteBase, o.Article.UrlName);
        w("<comments>{0}/article/show/{1}#comment</comments>\n", SiteBase, o.Article.UrlName);
        w("<guid isPermaLink=\"true\">{0}/article/show/{1}</guid>\n", SiteBase, o.Article.UrlName);
        w("<pubDate>{0}</pubDate>\n", o.SavedOn);
        w("<author>{0}</author>\n", o.Writer.ToHtml());
        w("</item>\n\n");
    }
%>

</channel></rss>