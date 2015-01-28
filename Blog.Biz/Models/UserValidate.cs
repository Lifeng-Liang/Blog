using System.Text;
using Leafing.Data.Definition;
using Leafing.Core.Text;

namespace Blog.Biz.Models
{
    public class UserValidate : DbObjectModel<UserValidate>
    {
        [Index]
        public long UserId { get; set; }

        [Length(20, 38)]
        public string Guid { get; set; }

        [Exclude]
        public char Mode { get; set; }

        [Exclude]
        public User User { get; set; }

        public UserValidate Init(long userId)
        {
            this.UserId = userId;
            this.Guid = System.Guid.NewGuid().ToString();
            return this;
        }

        public string SerializeToString()
        {
            var s = string.Format("{0}\n{1}\n{2}", UserId, Guid, Mode);
            var bs = Encoding.UTF8.GetBytes(s);
            return Base32StringCoding.Decode(bs);
        }

        public static UserValidate DeserializeFromString(string source)
        {
            var bs = Base32StringCoding.Encode(source);
            var s = Encoding.UTF8.GetString(bs);
            var ss = s.Split('\n');
            if (ss.Length == 3)
            {
                var id = long.Parse(ss[0]);
                var guid = ss[1];
                var u = User.FindById(id);
                if(u != null)
                {
                    var uv = FindOne(p => p.UserId == id);
                    if(uv != null && uv.Guid == guid)
                    {
                        uv.Delete();
                        uv.Mode = ss[2][0];
                        uv.User = u;
                        return uv;
                    }
                }
            }
            return null;
        }
    }
}
