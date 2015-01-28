using System;
using System.Collections.Generic;
using System.Web;
using Leafing.Core.Logging;
using Leafing.Web;
using Leafing.Web.Mvc;
using Blog.Biz.Models;
using Leafing.Core.Ioc;
using Leafing.Web.Mvc.Core;

namespace Blog.Biz.Helpers
{
    [Implementation(2)]
    public class MembershipProcessor : MvcProcessor
    {
        private readonly static Dictionary<string, List<string>> UnregUserPages;

        static MembershipProcessor()
        {
            UnregUserPages = new Dictionary<string, List<string>>
                                 {
                                     {"default", new List<string> {"list"}},
                                     {"rss", new List<string> {"article", "comment"}},
                                     {"user", new List<string> {"login", "register", "active"}},
                                     {"category", new List<string> {"show"}},
                                     {"comment", new List<string> {"create", "list", "destroy"}},
                                     {"article", new List<string> {"show", "home", "list"}},
                                     {"search", new List<string> {"article"}},
                                     {"tag", new List<string> {"show"}},
                                 };
        }

        protected override object InvokeAction(ControllerBase ctl)
        {
            if (HttpContextHandler.Instance.UserAgent == null 
                || HttpContextHandler.Instance.UserAgent.StartsWith("hitcrawler"))
            {
                OnNoPermission();
                return null;
            }

            var user = User.DeserializeFromString(ctl.Cookies[BlogSettings.LoginCookie]);
            if (user != null)
            {
                if (user.Role == UserRole.Administrator)
                {
                    return base.InvokeAction(ctl);
                }
                if (user.Role == UserRole.User || user.Role == UserRole.NonactivatedUser)
                {
                    if(Controller.LowerName == "user")
                    {
                        if (Action.LowerName == "profile" 
                            || Action.LowerName == "logout" 
                            || Action.LowerName == "sendactivemail")
                        {
                            return base.InvokeAction(ctl);
                        }
                    }
                }
            }

            if(UnregUserPages.ContainsKey(Controller.LowerName))
            {
                var actions = UnregUserPages[Controller.LowerName];
                if (actions != null && actions.Contains(Action.LowerName))
                {
                    return base.InvokeAction(ctl);
                }
            }

            OnNoPermission();
            return null;
        }

        protected virtual void OnNoPermission()
        {
            Logger.System.Warn("错误的请求: " + HttpContextHandler.Instance.RawUrl);
            HttpContextHandler.Instance.Write("错误的请求");
            HttpContextHandler.Instance.StatusCode = 403;
        }

        protected override void OnException(Exception exception, ControllerBase controller)
        {
            Logger.System.Error(exception);
            if (exception.InnerException != null && exception.InnerException is PageNotFoundException)
            {
                HttpContextHandler.Instance.Write("你访问的页面不存在");
                HttpContextHandler.Instance.StatusCode = 404;
            }
            else if (exception.InnerException != null && exception.InnerException is NoPermissionException)
            {
                HttpContextHandler.Instance.Write("权限不够");
                HttpContextHandler.Instance.StatusCode = 403;
            }
            else
            {
                HttpContextHandler.Instance.Write("发生异常，已经记录，请稍后访问");
                HttpContextHandler.Instance.StatusCode = 500;
            }
        }
    }
}