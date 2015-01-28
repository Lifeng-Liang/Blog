using Leafing.Data;
using Leafing.Web.Mvc;

namespace Blog.Biz.Controllers
{
    public class RssController : ControllerBase
    {
        [DefaultAction]
        public void Default()
        {
        }

        public void Article()
        {
            var list = Models.Article.Where(Condition.Empty)
                .OrderByDescending(p => p.Id).Range(1, 10).Select();
            this["List"] = list;
        }

        public void Comment()
        {
            var list = Models.Comment.Where(Condition.Empty)
                .OrderByDescending(p => p.Id).Range(1, 10).Select();
            this["List"] = list;
        }
    }
}
