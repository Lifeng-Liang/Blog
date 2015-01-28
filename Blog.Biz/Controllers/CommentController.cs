using Leafing.Data;
using Leafing.Core.Logging;
using Leafing.Web;
using Leafing.Web.Mvc;
using Blog.Biz.Helpers;
using Blog.Biz.Models;
using Leafing.Web.Mvc.Core;
using System.Web;

namespace Blog.Biz.Controllers
{
    public class CommentController : ControllerBase<Comment>
    {
        public string Create(long articleId)
        {
            var article = Article.FindById(articleId);
            CheckPermission(article);
            var obj = new Comment();

            var writer = Bind("comment[writer]");
            var content = Bind("comment[content]");

            Flash["Writer"] = writer;
            var user = this.GetLoginUser();

            if (user == null)
            {
                if (writer.LikeNull())
                {
                    Flash["Content"] = content;
                    Flash.Warning = "用户名不能为空";
                    goto end;
                }
                obj.Writer = writer.Trim();
            }
            else
            {
                obj.User = user;
            }

            if(content.LikeNull())
            {
                Flash.Warning = "内容不能为空";
            }
            else
            {
                if((user == null || !user.HasUserPermission()) && CommonHelper.IllegalComment(content))
                {
                    Flash.Warning = "内容不能含有url信息";
                    Flash["Content"] = content;
                }
                else
                {
                    obj.Content = content.TextEncode();
                    obj.Ip = HttpContextHandler.Instance.UserHostAddress;
                    obj.Article = article;
                    obj.Article.Statistic.CommentsCount++;
                    obj.Article.Statistic.Save();
                    obj.Save();
                    Logger.System.Trace(HttpContext.Current.Request.GetUserInfo());
                    Flash.Notice = "Comment was successfully created";
                }
            }

            end:
            return UrlTo<ArticleController>(p => p.Show(article.UrlName)) + "#comment_input";
        }

        private void CheckPermission(Article article)
        {
            if (article == null)
            {
                throw new PageNotFoundException();
            }
            if(HttpContextHandler.Instance.UrlReferrer != null)
            {
                var realUrl = HttpContextHandler.Instance.UrlReferrer.ToString().ToLower();
                var expUrl = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + UrlTo<ArticleController>(p => p.Show(article.UrlName));
                if (realUrl == expUrl || realUrl == expUrl + "#comment_input")
                {
                    return;
                }
            }
            throw new NoPermissionException();
        }

        public void List(long? pageIndex)
        {
            if (pageIndex <= 0)
            {
                throw new DataException("The pageIndex out of supported range.");
            }
            this.ItemList = DbEntry.From<Comment>().Where(Condition.Empty).OrderBy("Id DESC")
                .PageSize(WebSettings.DefaultPageSize).GetItemList(ListStyle.Hybird, pageIndex);
        }

        public override string Destroy(long n)
        {
            var c = Comment.FindById(n);
            var u = this.GetLoginUser();
            if (c != null && u != null)
            {
                if(u.Role == UserRole.Administrator || (c.User != null && u.Id == c.User.Id))
                {
                    return base.Destroy(n);
                }
            }
            Flash.Notice = "错误请求";
            return UrlTo<ArticleController>();
        }
    }
}
