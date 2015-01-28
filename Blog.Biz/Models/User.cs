using System;
using System.Collections.Generic;
using System.Text;
using Leafing.Data.Definition;
using Leafing.Core.Text;
using Blog.Biz.Helpers;

namespace Blog.Biz.Models
{
    public enum UserRole
    {
        [ShowString("管理员")]
        Administrator,

        [ShowString("用户")]
        User,

        [ShowString("未激活用户")]
        NonactivatedUser,
    }

    [Cacheable]
    public class User : DbObjectModel<User>
    {
        [Length(1, 128), Index(UNIQUE = true)]
        public string Email { get; set; }

        [Length(1, 128)]
        public string Password { get; set; }

        [Length(1, 50)]
        public string ShowName { get; set; }

        public UserRole Role { get; set; }

        [SpecialName]
        public DateTime CreatedOn { get; set; }

        [SpecialName]
        public DateTime? UpdatedOn { get; set; }

        [HasMany]
        public IList<Article> Articles { get; private set; }

        [HasMany]
        public IList<Comment> Comments { get; private set; }

        [Index(UNIQUE = true), Length(40), StringColumn(IsUnicode = false)]
        public string SessionId { get; set; }

        protected override void OnInserting()
        {
            HashPassword();
        }

        protected override void OnUpdating()
        {
            HashPassword();
        }

        private void HashPassword()
        {
            if (Password != null && Password.Length < 100)
            {
                Password = CommonHelper.GetHashedPassword(Password);
            }
        }

        public static User GetUserForLogin(string email, string password)
        {
            if (email == null || password == null)
            {
                return null;
            }

            var pass = CommonHelper.GetHashedPassword(password);
            var u = FindOne(p => p.Email == email);
            if (u != null)
            {
                if (u.Password != pass)
                {
                    return null;
                }
            }
            return u;
        }

        public static User GetUserForLogin(string sessionId)
        {
            if(sessionId == null)
            {
                return null;
            }

            return FindOne(p => p.SessionId == sessionId);
        }

        public bool HasUserPermission()
        {
            if(Role == UserRole.Administrator)
            {
                return true;
            }
            if (Role == UserRole.User && (DateTime.Now - CreatedOn).TotalHours > 23)
            {
                return true;
            }
            return false;
        }

        public static string SerializeToString(string sessionId)
        {
            if (sessionId == null) { return null; }
            var bs = Encoding.UTF8.GetBytes(sessionId);
            return Base32StringCoding.Decode(bs);
        }

        public static User DeserializeFromString(string source)
        {
            if (source == null) { return null; }
            var bs = Base32StringCoding.Encode(source);
            var s = Encoding.UTF8.GetString(bs);
            return GetUserForLogin(s);
        }
    }
}
