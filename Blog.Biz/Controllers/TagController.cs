using Leafing.Data;
using Leafing.Web;
using Leafing.Web.Mvc;
using Blog.Biz.Helpers;
using Blog.Biz.Models;

namespace Blog.Biz.Controllers
{
    public class TagController : ControllerBase<Tag>
    {
        public void Show(string name, long? pageIndex)
        {
            if(name.LikeNull())
            {
                throw new NoPermissionException();
            }
            var tag = Tag.FindOne(p => p.Name.ToLower() == name.ToLower());
            if(tag == null)
            {
                throw new PageNotFoundException();
            }
            this["Tag"] = tag;
            this["ItemList"] = DbEntry.From<ArticleTag>()
                .Where(p => p.TagId == tag.Id)
                .OrderByDescending(p => p.Id)
                .PageSize(WebSettings.DefaultPageSize)
                .GetItemList(ListStyle.Hybird, pageIndex);
        }
    }
}
