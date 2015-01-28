using System;
using Leafing.Web.Mvc;
using Blog.Biz.Models;

namespace Blog.Biz.Helpers
{
    public class UserPageBase : PageBase
    {
        protected internal new User User;

        protected override void OnLoad(EventArgs e)
        {
            User = this.GetLoginUser();
            base.OnLoad(e);
        }
    }
}
