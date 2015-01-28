using System;
using System.Text;
using System.Web;
using Leafing.Core;
using Leafing.Core.Logging;
using Leafing.Core.Text;
using Leafing.Web.Mvc;
using Blog.Biz.Helpers;
using Blog.Biz.Models;
using CommonHelper = Blog.Biz.Helpers.CommonHelper;
using Leafing.Web.Mvc.Core;

namespace Blog.Biz.Controllers
{
    public class UserController : ControllerBase<User>
    {
        public string Login()
        {
            var email = Bind("email");
            var password = Bind("password");
            bool rememberme = (Bind("rememberme") == "on") ? true : false;
            if (email == null || password == null)
            {
                return null;
            }
            var u = User.GetUserForLogin(email, password);
            if (u != null)
            {
                u.SessionId = Guid.NewGuid().ToString();
                u.Save();
                Cookies[BlogSettings.LoginCookie] = User.SerializeToString(u.SessionId);
                return UrlTo<ArticleController>();
            }
            Flash.Warning = "用户名或密码错误";
            return null;
        }

        public string Logout(string url)
        {
            var user = this.GetLoginUser();
            if(user != null)
            {
                user.SessionId = Guid.NewGuid().ToString();
                user.Save();
            }
            return url ?? UrlTo<ArticleController>();
        }

        public void Profile()
        {
            var password = Bind("password");
            var password1 = Bind("password1");
            var password2 = Bind("password2");
            if (!password.LikeNull())
            {
                if (CommonHelper.GetHashedPassword(password) != this.GetLoginUser().Password)
                {
                    Flash.Warning = "密码错误";
                    return;
                }
                if(password1.LikeNull() || password1 != password2)
                {
                    Flash.Warning = "新密码为空或不匹配";
                }
                if(password.Length >= 100)
                {
                    return;
                }
                var u = this.GetLoginUser();
                u.Password = password1;
                u.Save();
                Flash.Notice = "密码修改成功";
            }
            else
            {
                var showname = Bind("showname");
                if(showname.LikeNull())
                {
                    Flash.Warning = "请填写您要修改的部分";
                    return;
                }
                var u = this.GetLoginUser();
                u.ShowName = showname.Trim();
                u.Save();
                Flash.Notice = "用户名修改成功";
            }
        }

        public string SendActiveMail()
        {
            var u = this.GetLoginUser();
            if(u.Role == UserRole.NonactivatedUser)
            {
                var uv = UserValidate.FindOne(p => p.UserId == u.Id);
                if (uv == null)
                {
                    uv = new UserValidate().Init(u.Id);
                    uv.Save();
                }
                uv.Mode = 'a';
                SendMail(u, uv);
                Flash.Notice = "激活邮件发送成功";
            }
            return UrlTo<UserController>(p => p.Profile());
        }

        private void RegError(string msg, DateTime now)
        {
            Flash.Warning = msg;
            var t = now.Ticks.ToString();
            var x = t.Substring(0, BlogSettings.RegTimeXbits);
            var t2 = int.Parse(t.Substring(BlogSettings.RegTimeXbits));
            var z = Rand.Next(BlogSettings.RegTimeRandMax);
            var y = t2 - z;
            this["RegTimeX"] = x;
            this["RegTimeY"] = y.ToString();
            this["RegTimeZ"] = z.ToString();
            this["RegValidate"] = Base32StringCoding.Decode(StringHelper.HashMd5(BlogSettings.RegSolt + t));
        }

        public string Register()
        {
            var now = DateTime.Now;
            if (!BlogSettings.AllowRegister)
            {
                RegError("暂时不允许注册", now);
                return null;
            }
            var email = Bind("email");
            var password = Bind("password");
            var showname = Bind("showname");
            var time = Bind("time");
            var validate = Bind("validate");
            long ticks;
            if (email.LikeNull() || password.LikeNull() || showname.LikeNull() || email.IndexOf("@") < 0
                || !long.TryParse(time, out ticks) || validate.LikeNull())
            {
                RegError("Email密码以及显示名都是必填项", now);
                return null;
            }
            var regTime = new DateTime(ticks);
            if(Math.Abs((now - regTime).TotalSeconds) > BlogSettings.RegTimeDiffSecs)
            {
                RegError("注册超时，请刷新后重新注册", now);
                return null;
            }
            var realValidate = Base32StringCoding.Decode(StringHelper.HashMd5(BlogSettings.RegSolt + time));
            if(validate != realValidate)
            {
                Logger.Default.Trace("validate: " + validate + " - realValidate: " + realValidate + " - time: " + time);
                RegError("注册错误，请联系管理员", now);
                return null;
            }
            if(password.Length >= 100)
            {
                return null;
            }
            var u = new User
                        {
                            Email = email, 
                            Password = password, 
                            ShowName = showname, 
                            Role = UserRole.NonactivatedUser,
                            SessionId = Guid.NewGuid().ToString()
                        };
            var v = u.Validate();
            if(v.IsValid)
            {
                u.Save();
                var uv = new UserValidate().Init(u.Id);
                uv.Mode = 'a';
                uv.Save();

                SendMail(u, uv);

                Flash.Notice = "用户创建成功";
                return UrlTo<UserController>(p => p.Login());
            }
            var sb = new StringBuilder();
            foreach (var message in v.ErrorMessages)
            {
                sb.Append(message);
            }
            RegError(sb.ToString(), now);
            return null;
        }

        private void SendMail(User u, UserValidate uv)
        {

            string link = string.Format("http://{0}{1}", HttpContext.Current.Request.Url.Authority,
                                        UrlTo<UserController>(p => p.Active(uv.SerializeToString())));
            string mail = string.Format("请点击以下链接或复制链接到浏览器里以完成用户注册：<br/>\n<a href=\"{0}\">{0}</a>", link);

            CommonHelper.SendMail(u.Email, mail);
        }

        public string Active(string validateText)
        {
            var uv = UserValidate.DeserializeFromString(validateText);
            if(uv != null && uv.Mode == 'a')
            {
                uv.User.Role = UserRole.User;
                uv.User.Save();
                Flash.Notice = "用户激活成功";
            }
            else
            {
                Flash.Notice = "用户激活失败";
            }
            return UrlTo<ArticleController>();
        }
    }
}
