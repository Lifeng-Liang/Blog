using System.Collections.Generic;
using System.Text;
using Leafing.Web.Mvc;
using Blog.Biz.Models;
using Leafing.Web.Mvc.Core;

namespace Blog.Biz.Helpers
{
    public class UbbEncoder : ContentEncoder
    {
        public static readonly Dictionary<string, string> UbbDict;

        public static readonly string UbbCodeHighlighter = string.Format(@"
<link type=""text/css"" rel=""stylesheet"" href=""{0}/styles/shCore.css""/>
<link type=""text/css"" rel=""stylesheet"" href=""{0}/styles/shThemeDefault.css""/>
<script class=""javascript"" src=""{0}/scripts/shCore.js""></script>
<script class=""javascript"" src=""{0}/scripts/shBrushAll.js""></script>
<script class=""javascript"">SyntaxHighlighter.config.clipboardSwf = '{0}/scripts/clipboard.swf'; SyntaxHighlighter.all();</script>
", BlogSettings.SiteBase);

        static UbbEncoder()
        {
            UbbDict = new Dictionary<string, string>
            {
                {"[hr]", "<table width=92% align=center cellpadding=0 cellspacing=0><tr><td height=1 class=title></td></tr></table>"},
                {"[article]", ""},
                {"[h1]", "<h1>"},
                {"[/h1]", "</h1>"},
                {"[h2]", "<h2>"},
                {"[/h2]", "</h2>"},
                {"[h3]", "<h3>"},
                {"[/h3]", "</h3>"},
                {"[h4]", "<h4>"},
                {"[/h4]", "</h4>"},
                {"[i]", "<i>"},
                {"[/i]", "</i>"},
                {"[ul]", "<ul>"},
                {"[/ul]", "</ul>"},
                {"[ol]", "<ol>"},
                {"[/ol]", "</ol>"},
                {"[li]", "<li>"},
                {"[/li]", "</li>"},
                {"[u]", "<u>"},
                {"[/u]", "</u>"},
                {"[b]", "<b>"},
                {"[/b]", "</b>"},
                {"[img]", "<img src=\""},
                {"[/img]", "\" border=0>"},
                {"[quote]", "<div class=quote>"},
                {"[/quote]", "</div>"},
                {"[quotefit]", "<table><tr><td>"},
                {"[/quotefit]", "</td></tr></table>"},
                {"[size]", "<font size="},
                {"[/size]", "</font>"},
                {"size", ">"},
                {"[align]", "<div align="},
                {"[/align]", "</div>"},
                {"align", ">"},
                {"[face]", "<font face="},
                {"[/face]", "</font>"},
                {"face", ">"},
                {"[color]", "<font color="},
                {"[/color]", "</font>"},
                {"color", ">"},
                {"[down]", string.Format("<img src=\"{0}/images/down.gif\"> <a href=\"", BlogSettings.SiteBase)},
                {"[/down]", "</a>"},
                {"down", "\" target=\"_blank\">"},
                {"[url]", "<a href=\""},
                {"[/url]", "</a>"},
                {"url", "\" target=\"_blank\">"},
                {"[email]", string.Format("<img align=absmiddle src={0}/images/email1.gif><A HREF=\"mailto:", BlogSettings.SiteBase)},
                {"[/email]", "</a>"},
                {"email", "\" TARGET=_blank>"},
                {"[code]", "<pre class=\"brush: "},
                {"[/code]", "</pre>"},
                {"code", ";\">"},
            };
        }

        bool _needEncode = true;
        bool _haveCode;
        string _content;

        public override string Encode(string content)
        {
            _content = content;
            var result = new StringBuilder();
            var n = 0;
            while (true)
            {
                var i = _content.IndexOf("[", n);
                if (i >= 0)
                {
                    Write(_content.Substring(n, i - n), result);
                    n = i + 1;
                    i = _content.IndexOf("]", n);
                    if (i > 0)
                    {
                        var t = _content.Substring(n, i - n);
                        n = i + 1;
                        result.Append(this.ChangeToken(t, n));
                        if (!_needEncode)
                        {
                            var m = _content.IndexOf("[/code]", n);
                            Write(_content.Substring(n, m - n), result);
                            n = m;
                        }
                    }
                    else
                    {
                        Write(_content.Substring(n), result);
                        break;
                    }
                }
                else
                {
                    Write(_content.Substring(n), result);
                    break;
                }
            }
            if (_haveCode)
            {
                result.Append(UbbCodeHighlighter);
            }
            return result.ToString();
        }

	    private void Write(string s, StringBuilder sb)
	    {
	        s = _needEncode ? s.TextEncode() : s.ToHtml();
	        sb.Append(s);
	    }

        private string ChangeToken(string s, int index)
	    {
		    var n = s.IndexOf("=");
		    if (n > 0)
		    {
			    var left = s.Substring(0, n).ToLower();
			    var right = s.Substring(n+1);
			    var leftWithQuote = "[" + left + "]";
			    Check(leftWithQuote);
			    if (UbbDict.ContainsKey(leftWithQuote))
			    {
                    if (leftWithQuote == "[article]")
                    {
                        long id;
                        if (long.TryParse(right, out id))
                        {
                            var article = Article.FindById(id);
                            if (article != null)
                            {
                                return new LinkToInfo("article").Action("show").Title(article.Title).Parameters(article.UrlName);
                            }
                        }
                        return "文章未发现";
                    }
                    return UbbDict[leftWithQuote] + right + UbbDict[left];
			    }
		    }
		    else
		    {
                var left = s.ToLower();
			    var leftWithQuote = "[" + left + "]";
			    Check(leftWithQuote);
                if(leftWithQuote == "[url]")
                {
                    var right = GetInner("[/url]", index);
                    return UbbDict[leftWithQuote] + right + UbbDict[left];
                }
			    if (UbbDict.ContainsKey(leftWithQuote))
			    {
                    return UbbDict[leftWithQuote];
			    }
		    }
		    return "[" + s + "]";
	    }

        private string GetInner(string endQuote, int index)
        {
            var i = _content.IndexOf(endQuote, index);
            if (i > 0)
            {
                return _content.Substring(index, i - index);
            }
            return "错误的URL格式";
        }

	    private void Check(string s)
	    {
		    if (s == "[code]")
		    {
			    _haveCode = true;
			    _needEncode = false;
		    }
		    else if (s == "[/code]")
		    {
			    _needEncode = true;
		    }
	    }
    }
}
