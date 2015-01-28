using Blog.Biz.Models;
using Leafing.Core;
using Leafing.Core.Logging;
using Leafing.Core.Text;
using Leafing.Data;
using System;
using System.IO;

namespace Blog
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Logger.System.Trace("Application Start");
            if(Category.GetCount(Condition.Empty) == 0)
            {
                var root = new Category { Name = "Article", Alias = "" }; // 必须存在
                var c1 = new Category { Name = "文章", Alias = "essay" };
                var c2 = new Category { Name = "作品", Alias = "product" };
                var c3 = new Category { Name = "转载", Alias = "reshipment" };
                root.Children.Add(c1);
                root.Children.Add(c2);
                root.Children.Add(c3);
                root.Save();

                var links = new Category { Name = "Links", Alias = "" }; // 必须存在
                var c5 = new Category { Name = "其它站点", Alias = "" }; 
                var c6 = new Category { Name = "友情链接", Alias = "" };
                links.Children.Add(c5);
                links.Children.Add(c6);
                links.Save();

                // 缺省用户，用它创建真正的管理员后，用新管理员登录，删除此缺省用户。
                var user = new User { Email = "test@test.com", Password = "123", ShowName = "创建管理员后删除", 
                    Role = UserRole.Administrator, SessionId = Guid.NewGuid().ToString() };
                user.Save();

                var readme = StringHelper.ReadToEnd(Path.Combine(SystemHelper.BaseDirectory, "README.md"));
                var s = new Statistic();
                var article = new Article
                {
                    Title = "欢迎使用",
                    Alias = "readme",
                    SummaryIsEmpty = true,
                    Format = ArticleFormat.Markdown,
                    Writer = "梁利锋",
                    Content = readme,
                    Category = c1,
                    Statistic = s,
                    User = user,
                };
                article.Save();
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Logger.Default.Error(Server.GetLastError().GetBaseException());
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Logger.System.Trace("Application End");
        }
    }
}
