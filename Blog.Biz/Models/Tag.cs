using System.Collections.Generic;
using Leafing.Data.Definition;

namespace Blog.Biz.Models
{
    [Cacheable]
    public class Tag : DbObjectModel<Tag>
    {
        public string Name { get; set; }

        public int Count { get; set; }

        [HasMany]
        public IList<CrossArticleTag> Cross { get; private set; }
    }
}
