using Leafing.Web;
using Leafing.Web.Mvc;
using Blog.Biz.Helpers;
using Blog.Biz.Models;

namespace Blog.Biz.Controllers
{
    public class CategoryController : ControllerBase<Category>
    {
        public void Show(string urlName, long? pageIndex)
        {
            if(urlName.LikeNull())
            {
                throw new NoPermissionException();
            }
            var category = Category.FindByUrlName(urlName);
            this["Category"] = category;
            this["ItemList"] = Article.Where(p => p.Category.Id == category.Id)
                .OrderByDescending(p => p.Id)
                .PageSize(WebSettings.DefaultPageSize)
                .GetItemList(ListStyle.Hybird, pageIndex);
        }
    }
}
