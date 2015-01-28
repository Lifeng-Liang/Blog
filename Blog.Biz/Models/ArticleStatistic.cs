using Leafing.Data.Definition;

namespace Blog.Biz.Models
{
    [JoinOn(0, typeof(Article), "Statistic_Id", typeof(Statistic), "Id")]
    public class ArticleStatistic : IDbObject
    {
        [DbColumn("Articles.Id")] public long Id;
        public string Title;
        [AllowNull] public string Alias;
        public int ViewCount;
        public int CommentsCount;

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
    }
}
