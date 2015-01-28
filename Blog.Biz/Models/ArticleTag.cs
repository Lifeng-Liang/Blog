using System;
using Leafing.Data.Definition;

namespace Blog.Biz.Models
{
    [JoinOn(0, typeof(Article), "Id", typeof(CrossArticleTag), "Article_Id")]
    public class ArticleTag : IDbObject
    {
        [DbColumn("Articles.Id")]
        public long Id;

        public string Title;

        [AllowNull]
        public string Alias;

        [DbColumn("Tag_Id")]
        public long TagId;

        public DateTime CreatedOn;

        [AllowNull]
        public string Summary;

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
