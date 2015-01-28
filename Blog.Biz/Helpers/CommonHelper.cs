using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using Leafing.Core;
using Leafing.Core.Logging;
using Leafing.Core.Text;
using Leafing.Web.Mvc;
using Blog.Biz.Models;

namespace Blog.Biz.Helpers
{
    public static class CommonHelper
    {
        public static readonly string Copyright = string.Format(BlogSettings.CopyrightTemplate, Date.Now.Year);

        private static readonly SHA512 Hash = SHA512.Create();

        public static string GetHashedPassword(string password)
        {
            var bytes = Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            var ret = Base32StringCoding.Decode(bytes);
            return ret;
        }

        public static List<string> GetTagNameList(string tags)
        {
            var list = new List<string>();
            if (!tags.LikeNull())
            {
                var taglist = tags.Split(',');
                foreach (var s in taglist)
                {
                    string t = s.Replace(".", " ").Trim();
                    if (t == "") continue;
                    list.Add(t);
                }
            }
            return list;
        }

        public static void ProcessTagNew(Article obj, string tags)
        {
            List<string> list = GetTagNameList(tags);
            foreach (var t in list)
            {
                AddNewTag(t, obj);
            }
        }

        public static void AddNewTag(string t, Article obj)
        {
            var tag = Tag.FindOne(p => p.Name == t);
            if (tag != null)
            {
                var c = new CrossArticleTag {Tag = tag};
                tag.Count++;
                obj.Cross.Add(c);
                tag.Save();
            }
            else
            {
                var c = new CrossArticleTag();
                var tt = new Tag {Name = t, Count = 1};
                tt.Save();
                c.Tag = tt;
                obj.Cross.Add(c);
            }
        }

        public static void ProcessTagEdit(Article obj, string tags)
        {
            List<string> list = GetTagNameList(tags);
            var olist = new List<string>();
            var rclist = new List<CrossArticleTag>();
            foreach (var cross in obj.Cross)
            {
                var tag = cross.Tag;
                olist.Add(tag.Name);
                if (!list.Contains(tag.Name))
                {
                    tag.Count--;
                    tag.Cross.Remove(cross);
                    rclist.Add(cross);
                    tag.Save();
                }
            }
            foreach (var cross in rclist)
            {
                obj.Cross.Remove(cross);
                cross.Delete();
            }
            foreach (var s in list)
            {
                if (!olist.Contains(s))
                {
                    AddNewTag(s, obj);
                }
            }
        }

        public static IEnumerable<long> PageLinks(long pageCount, long pageIndex, ListStyle style, long linkCount)
        {
            if (pageIndex > pageCount || pageIndex < 0)
            {
                throw new ArgumentOutOfRangeException("pageIndex");
            }
            if (linkCount < 3)
            {
                throw new ArgumentOutOfRangeException("linkCount");
            }

            if (style == ListStyle.Hybird)
            {
                if (pageIndex == 0)
                {
                    pageIndex = pageCount;
                }
                //pageCount++;
            }

            if(linkCount > pageCount)
            {
                linkCount = pageCount;
            }

            if(pageCount < 3)
            {
                switch (style)
                {
                    case ListStyle.Default:
                        for (long i = 1; i <= pageCount; i++)
                        {
                            yield return i;
                        }
                        break;
                    case ListStyle.Static:
                        for (long i = pageCount; i >= 1; i--)
                        {
                            yield return i;
                        }
                        break;
                    case ListStyle.Hybird:
                        yield return 0;
                        if(pageCount >= 2)
                        {
                            for (long i = pageCount - 1; i >= 1; i--)
                            {
                                yield return i;
                            }
                        }
                        break;
                }
            }
            else
            {
                long diff = linkCount - 3;
                long less = pageIndex - diff / 2;
                long more = pageIndex + diff / 2;
                if(diff % 2 == 1)
                {
                    less--;
                }
                if (less <= 1)
                {
                    more = more + (2 - less);
                    less = 2;
                }
                else if ((pageCount - more) < 1)
                {
                    var n = pageCount - more - 1;
                    less = less + n;
                    more = more + n;
                }
                switch (style)
                {
                    case ListStyle.Default:
                        yield return 1;
                        if (less != 2)
                        {
                            yield return -1;
                        }
                        for (long i = less; i <= more; i++)
                        {
                            yield return i;
                        }
                        if (pageCount != 1 + more)
                        {
                            yield return -1;
                        }
                        yield return pageCount;
                        break;
                    case ListStyle.Static:
                        yield return pageCount;
                        if (pageCount != 1 + more)
                        {
                            yield return -1;
                        }
                        for (long i = more; i >= less; i--)
                        {
                            yield return i;
                        }
                        if (less != 2)
                        {
                            yield return -1;
                        }
                        yield return 1;
                        break;
                    case ListStyle.Hybird:
                        yield return 0;
                        if (pageCount != more + 1)
                        {
                            yield return -1;
                        }
                        for (long i = more; i >= less; i--)
                        {
                            yield return i;
                        }
                        if (less != 2)
                        {
                            yield return -1;
                        }
                        yield return 1;
                        break;
                }
            }
        }

        private readonly static string[] NotAllowList = new[] { "http://", "[url=", "<a href" };

        public static bool IllegalComment(string content)
        {
            foreach (var s in NotAllowList)
            {
                if(IllegalCommentForSingleWord(content, s))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IllegalCommentForSingleWord(string content, string word)
        {
            if (content.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }
            return false;
        }

        public static void SendMail(string email, string content)
        {
            var m = new MailMessage
                        {
                            Subject = BlogSettings.BlogName + "用户激活",
                            SubjectEncoding = Encoding.UTF8,
                            Body = content,
                            BodyEncoding = Encoding.UTF8,
                            IsBodyHtml = true,
                            From = new MailAddress(BlogSettings.EmailAddress),
                        };
            m.To.Add(new MailAddress(email));
            var smtp = new SmtpClient(BlogSettings.EmailSmtpServer)
                           {
                               Credentials = new NetworkCredential(BlogSettings.EmailUsername, BlogSettings.EmailPassword)
                           };
            smtp.Send(m);
        }

        public static ProcessModelSection GetProcessModel()
        {
            var pms = (ProcessModelSection)ConfigurationManager.GetSection("system.web/processModel");
            return pms;
        }
    }
}
