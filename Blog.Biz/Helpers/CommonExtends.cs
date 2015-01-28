﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Leafing.Data.Definition;
using Leafing.Web.Mvc;
using Blog.Biz.Helpers;
using Blog.Biz.Models;
using Leafing.Web;
using Leafing.Web.Mvc.Core;
using Blog.Biz;

public static class CommonExtends
{
    public static User GetLoginUser(this MembershipProcessor page)
    {
        return GetLoginUser();
    }

    public static User GetLoginUser(this ControllerBase controller)
    {
        return GetLoginUser();
    }

    public static User GetLoginUser(this MasterPageBase page)
    {
        return GetLoginUser();
    }

    public static User GetLoginUser(this PageBase page)
    {
        return GetLoginUser();
    }

    private static User GetLoginUser()
    {
        var c = CookiesHandler.Instance[BlogSettings.LoginCookie];
        var user = User.DeserializeFromString(c);
        return user;
    }

    public static bool IsCurUserOrAdmin(this UserPageBase page, User user)
    {
        if (page.User == null)
        {
            return false;
        }
        if (page.User.Role == UserRole.Administrator)
        {
            return true;
        }
        if (user != null && page.User.Email == user.Email)
        {
            return true;
        }
        return false;
    }

    public static string ToHtml(this string s)
    {
        if (s.IsNullOrEmpty()) { return ""; }
        return HttpUtility.HtmlEncode(s);
    }

    public static string UbbEncode(this string s)
    {
        return new UbbEncoder().Encode(s);
    }

    public static string MarkdownEncode(this string s)
    {
        return new MarkdownSharp.Markdown().Transform(s);
    }

    private static readonly ContentEncoder TextEncoder = new TextEncoder();

    public static string TextEncode(this string s)
    {
        return TextEncoder.Encode(s);
    }

    public static bool LikeNull(this string s)
    {
        if(s == null)
        {
            return true;
        }
        if(s.Trim() == "")
        {
            return true;
        }
        return false;
    }

    public static void CheckNull(this Article obj)
    {
        if (obj.Alias.LikeNull())
        {
            obj.Alias = null;
        }
        if (obj.Summary.LikeNull())
        {
            obj.Summary = null;
            obj.SummaryIsEmpty = true;
        }
        else
        {
            obj.SummaryIsEmpty = false;
        }
        if (obj.Writer.LikeNull())
        {
            obj.Writer = null;
        }
        if (obj.Reference.LikeNull())
        {
            obj.Reference = null;
        }
    }

    public static IEnumerable<long> PageLinks<T>(this ItemList<T> itemList, ListStyle style, long linkCount) where T : class, IDbObject
    {
        return CommonHelper.PageLinks(itemList.PageCount, itemList.PageIndex, style, linkCount);
    }

    public static string GetUserInfo(this HttpRequest request)
    {
        var s = string.Format("{0},{1},{2},{3},{4}",
                              GetString(request.Url),
                              GetString(request.UrlReferrer),
                              GetString(request.UserAgent),
                              GetString(request.UserHostAddress),
                              GetString(request.UserLanguages));
        return s;
    }

    private static string GetString(object o)
    {
        if(o != null)
        {
            if(o is string[])
            {
                return ((IEnumerable) o).Cast<object>().Aggregate("", (current, obj) => current + ("{" + obj + "}"));
            }

            return o.ToString();
        }
        return "<null>";
    }

    public static bool WithAnalytics(this User user)
    {
        if(user != null && user.Role == UserRole.Administrator)
        {
            return false;
        }
        return true;
    }

    public static bool AdvancedSearch(this User user)
    {
        if (user != null && user.HasUserPermission())
        {
            return true;
        }
        return false;
    }

    public static string GetTags(this Article article)
    {
        var sb = new StringBuilder();
        foreach (var cross in article.Cross)
        {
            sb.Append(new LinkToInfo("tag").Action("show").Title(cross.Tag.Name).Parameters(cross.Tag.Name));
            sb.Append(", ");
        }
        if (sb.Length > 1)
        {
            sb.Length -= 2;
        }
        return sb.ToString();
    }

    public static string GetDescription(this Article article)
    {
        var sb = new StringBuilder(article.Title);
        foreach (var cross in article.Cross)
        {
            sb.Append(",");
            sb.Append(cross.Tag.Name);
        }
        return sb.ToString();
    }
}