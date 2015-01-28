using Leafing.Web.Mvc;

namespace Blog.Biz.Controllers
{
    public class DefaultController : ControllerBase
    {
        public string List()
        {
            return UrlTo<ArticleController>();
        }
    }
}
