using System.Collections.Generic;
using Leafing.Data.Definition;

namespace Blog.Biz.Models
{
    [Cacheable]
    public class Category : DbObjectModelAsTree<Category>
    {
        [Length(1, 255)]
        public string Name { get; set; }

        [Length(1, 255), AllowNull]
        public string Alias { get; set; }

        [HasMany]
        public IList<Article> Articles { get; private set; }

        [HasMany]
        public IList<Link> Links { get; private set; }

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

        public static Category FindByUrlName(string urlName)
        {
            long n;
            var category = long.TryParse(urlName, out n) ? FindById(n) : FindOne(p => p.Alias == urlName);
            return category;
        }
    }
}
