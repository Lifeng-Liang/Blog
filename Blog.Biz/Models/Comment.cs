using System;
using Leafing.Data.Definition;

namespace Blog.Biz.Models
{
    public class Comment : DbObjectModel<Comment>
    {
        [Length(1, 50), AllowNull]
        public string Writer { get; set; }

        public string Content { get; set; }

        [Length(7,15)]
        public string Ip { get; set; }

        [SpecialName]
        public DateTime SavedOn { get; set; }

        [BelongsTo]
        public User User { get; set; }

        [BelongsTo]
        public Article Article { get; set; }

        [Exclude]
        public string WriterName
        {
            get
            {
                if(string.IsNullOrEmpty(Writer))
                {
                    if(User != null)
                    {
                        return string.Format("<span class=\"register_user\">{0}</span>", User.ShowName.ToHtml());
                    }
                    return "<span class=\"unregister_user\">(未知用户)</span>";
                }
                return string.Format("<span class=\"unregister_user\">{0}(非注册用户)</span>", Writer.ToHtml());
            }
        }
    }
}
