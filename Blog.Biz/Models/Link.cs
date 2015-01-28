using Leafing.Data.Definition;

namespace Blog.Biz.Models
{
    public class Link : DbObjectModel<Link>
    {
        [Length(1, 255)]
        public string Title { get; set; }

        [Length(1, 255)]
        public string Url { get; set; }

        [BelongsTo]
        public Category Category { get; set; }
    }
}
