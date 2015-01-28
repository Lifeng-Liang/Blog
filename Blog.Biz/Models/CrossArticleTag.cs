using Leafing.Data.Definition;

namespace Blog.Biz.Models
{
    public class CrossArticleTag : DbObjectModel<CrossArticleTag>
    {
        [BelongsTo]
        public Article Article { get; set; }

        [BelongsTo]
        public Tag Tag { get; set; }
    }
}
