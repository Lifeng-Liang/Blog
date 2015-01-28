using System.Text.RegularExpressions;
using System.Web;

namespace Blog.Biz.Helpers
{
    public class TextEncoder : ContentEncoder
    {
        public override string Encode(string content)
        {
            if (content != null)
		    {
                string s = content.Replace("  ", "　");
		        s = HttpUtility.HtmlEncode(s);
		        return Regex.Replace(s, "\r\n|\n", "<br>\n");
		    }
	        return null;
        }
    }
}
