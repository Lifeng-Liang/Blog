using System;
using Leafing.Data;
using Leafing.Data.Model.Linq;
using Leafing.Web;
using Leafing.Web.Mvc;
using Blog.Biz.Models;

namespace Blog.Biz.Controllers
{
    public class SearchController : ControllerBase
    {
        public string Article(long? pageIndex)
        {
            string keyword = Bind("q");
            if (keyword.LikeNull())
            {
                return UrlTo<ArticleController>(p => p.List(null, null));
            }
            keyword = keyword.Trim();
            this["Keyword"] = keyword;

            Condition condition;
            if(this.GetLoginUser().AdvancedSearch())
            {
                Condition conTitle = null;
                Condition conContent = null;
                var ss = keyword.Split(new[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var s in ss)
                {
                    conTitle &= ExpressionParser<Article>.Parse(p => p.Title.Contains(s));
                    conContent &= ExpressionParser<Article>.Parse(p => p.Content.Contains(s));
                }
                condition = conTitle || conContent;
            }
            else
            {
                condition = ExpressionParser<Article>.Parse(p => p.Title.Contains(keyword));
            }

            var ps = Models.Article.Where(condition)
                .OrderByDescending(p => p.CreatedOn)
                .PageSize(WebSettings.DefaultPageSize)
                .GetPagedSelector();
            this["List"] = ps.GetCurrentPage((pageIndex ?? 1) - 1);
            this["ListPageCount"] = ps.GetPageCount();
            this["ListPageIndex"] = pageIndex ?? 0;
            return null;
        }
    }
}
