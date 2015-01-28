using Leafing.Data.Definition;

namespace Blog.Biz.Models
{
    [Cacheable]
    public class Statistic : DbObjectModel<Statistic>
    {
        public int ViewCount { get; set; }

        public int CommentsCount { get; set; }

        [HasOne]
        public Article Article { get; set; }
    }
}
