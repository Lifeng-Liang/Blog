using System;
using System.Collections.Generic;
using System.Text;
using Leafing.Data.Definition;
using Leafing.Core.Text;

namespace Blog.Biz.Models
{
    public enum ArticleFormat
    {
        Html,
        Ubb,
        Markdown,
        Link,
    }

    [Cacheable, SoftDelete]
    public class Article : DbObjectModel<Article>
    {
        [Length(1, 255)]
        public string Title { get; set; }

        [Length(1, 255), Index(UNIQUE = true), AllowNull]
        public string Alias { get; set; }

        [LazyLoad]
        public string Content { get; set; }

        [Length(1, 200), AllowNull]
        public string Summary { get; set; }

        public bool SummaryIsEmpty { get; set; }

        public bool Recommend { get; set; }

        public ArticleFormat Format { get; set; }

        [Length(1, 50), AllowNull]
        public string Writer { get; set; }

        [Length(1, 255), AllowNull]
        public string Reference { get; set; }

        [SpecialName]
        public DateTime CreatedOn { get; set; }

        [SpecialName]
        public DateTime? UpdatedOn { get; set; }

        [BelongsTo]
        public Category Category { get; set; }

        [BelongsTo]
        public Statistic Statistic { get; set; }

        [HasMany]
        public IList<CrossArticleTag> Cross { get; private set; }

        [BelongsTo]
        public User User { get; set; }

        [HasMany(OrderBy = "SavedOn, Id")]
        public IList<Comment> Comments { get; private set; }

        [Exclude]
        public string UrlName
        {
            get
            {
                if (string.IsNullOrEmpty(Alias))
                {
                    return Id.ToString();
                }
                return Alias;
            }
        }

        [Exclude]
        public string WriterName
        {
            get
            {
                if (string.IsNullOrEmpty(Writer))
                {
                    return string.Format("<span class=\"register_user\">{0}</span>", User.ShowName);
                }
                return string.Format("<span class=\"unregister_user\">{0}</span>", Writer);
            }
        }

        [Exclude]
        public string TagNames
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var cross in this.Cross)
                {
                    sb.Append(cross.Tag.Name).Append(",");
                }
                if(sb.Length > 1)
                {
                    sb.Length--;
                }
                return sb.ToString();
            }
        }

        protected override void OnInserting()
        {
            RebuildSummary();
        }

        protected override void OnUpdating()
        {
            RebuildSummary();
        }

        public void RebuildSummary()
        {
            if (SummaryIsEmpty && m_UpdateColumns.ContainsKey("Content"))
            {
                string s;
                switch (Format)
                {
                    case ArticleFormat.Html:
                        s = Content.Replace("<br>", "\n");
                        s = StringHelper.ProcessSymbol(s, "<", ">", text => "");
                        break;
                    case ArticleFormat.Ubb:
                        s = StringHelper.ProcessSymbol(Content, "[", "]", text => "").ToHtml();
                        break;
                    case ArticleFormat.Markdown:
                        s = Content.Replace("\r\n", "\n").Replace("\n\n", "\n");
                        s = StringHelper.ProcessSymbol(s, "<", ">", text => "");
                        break;
                    case ArticleFormat.Link:
                        s = Content.Replace("\r\n", "\n").Replace("\n\n", "\n");
                        s = StringHelper.ProcessSymbol(s, "<", ">", text => "");
                        break;
                    default:
                        throw new Exception("系统错误");
                }
                s = s.Replace("\r\n", "<br>");
                s = s.Replace("\n", "<br>");

                if (s.Length > 197)
                {
                    Summary = s.Substring(0, 197) + "...";
                }
                else
                {
                    Summary = s;
                }
            }
        }
    }
}
