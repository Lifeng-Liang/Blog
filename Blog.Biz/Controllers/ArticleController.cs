using System;
using Leafing.Data;
using Leafing.Web.Mvc;
using Blog.Biz.Helpers;
using Blog.Biz.Models;
using System.Web;

namespace Blog.Biz.Controllers
{
    [ListStyle(ListStyle.Static)]
    public class ArticleController : ControllerBase<Article>
    {
        public string Show(string alias)
        {
            if(alias.LikeNull())
            {
                throw new NoPermissionException();
            }

            long n;
            Article article;
            if(long.TryParse(alias, out n))
            {
                article = Article.FindById(n);
                if(article != null && !string.IsNullOrEmpty(article.Alias))
                {
                    return UrlTo<ArticleController>(p => p.Show(article.Alias));
                }
            }
            else
            {
                article = Article.FindOne(p => p.Alias.ToLower() == alias.ToLower());
            }
            if (article == null)
            {
                throw new PageNotFoundException("can not found the page [{0}]", alias);
            }
            article.Statistic.ViewCount++;
            if(article.Statistic.CommentsCount > 0)
            {
                article.Statistic.CommentsCount = article.Comments.Count;
            }
            article.Statistic.Save();
            this.Item = article;
            var login = this.GetLoginUser();
            if(login != null)
            {
                Flash["Write"] = login.ShowName.ToHtml();
            }
            return null;
        }

        [DefaultAction]
        public void Home()
        {
            ProcessList(null, null, ListStyle.Default);
            this["CommentRankingList"] = DbEntry
                .From<ArticleStatistic>()
                .Where(Condition.Empty)
                .OrderByDescending(p => p.CommentsCount)
                .Range(1, 10)
                .Select();
            this["ReadRankingList"] = DbEntry
                .From<ArticleStatistic>()
                .Where(Condition.Empty)
                .OrderByDescending(p => p.ViewCount)
                .Range(1, 10)
                .Select();
            this["Link"] = Category.FindOne(p => p.Name == "其它站点");
            this["Recommand"] = Article.Where(p => p.Recommend)
                .OrderByDescending(p => p.CreatedOn).Range(1, 5).Select();
        }

        public override void List(long? pageIndex, int? pageSize)
        {
            ProcessList(pageIndex, pageSize, ListStyle.StaticLite);
        }

        public override void New()
        {
            this["List"] = Category.FindOne(p => p.Name == "Article").Children;
        }

        public override string Create()
        {
            var obj = new Article();

            var categoryid = long.Parse(Bind("article[category]"));
            var category = Category.FindById(categoryid);
            obj.Category = category;
            GetColumns(obj);
            obj.CheckNull();

            obj.User = this.GetLoginUser();
            var hit = new Statistic();
            hit.Save();
            obj.Statistic = hit;

            DbEntry.UsingTransaction(delegate
            {
                CommonHelper.ProcessTagNew(obj, Bind("article[tags]"));
                obj.Save();
            });

            Flash.Notice = "Article was successfully created";
            return UrlTo<ArticleController>();
        }

        public override void Edit(long n)
        {
            base.Edit(n);
            this["List"] = Category.FindOne(p => p.Name == "Article").Children;
        }

        public override string Update(long n)
        {
            var obj = Article.FindById(n);

            var categoryid = long.Parse(Bind("article[category]"));
            if(obj.Category.Id != categoryid)
            {
                var category = Category.FindById(categoryid);
                obj.Category = category;
            }
            GetColumns(obj);
            obj.CheckNull();

            DbEntry.UsingTransaction(delegate
            {
                CommonHelper.ProcessTagEdit(obj, Bind("article[tags]"));
                obj.Save();
            });

            Flash.Notice = "Article was successfully updated";
            return UrlTo<ArticleController>(p => p.Show(n));
        }

        private void GetColumns(Article obj)
        {
            obj.Title = Bind("article[title]");
            obj.Alias = Bind("article[alias]");
            obj.Summary = Bind("article[summary]");
            obj.Recommend = (Bind("article[recommend]") == "on") ? true : false;
            obj.Format = (ArticleFormat)Enum.Parse(typeof(ArticleFormat), Bind("article[format]"));
            obj.Writer = Bind("article[writer]");
            obj.Reference = Bind("article[reference]");
            obj.Content = Bind("article[content]");
        }
    }
}
