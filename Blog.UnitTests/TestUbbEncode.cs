using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Biz.Helpers;

namespace Blog.UnitTests
{
    [TestClass]
    public class TestUbbEncode : TestBase
    {
        [TestMethod]
        public void Test1()
        {
            const string origin = "[code=js]aaa[i]bbb[/code]";
            Assert.AreEqual(@"<pre class=""brush: js;"">aaa[i]bbb</pre>" + UbbEncoder.UbbCodeHighlighter, origin.UbbEncode());
        }

        [TestMethod]
        public void Test2()
        {
            const string origin = "[b]aaa[i]b[/i]bb[/b]";
            Assert.AreEqual("<b>aaa<i>b</i>bb</b>", origin.UbbEncode());
        }

        [TestMethod]
        public void Test3()
        {
            const string origin = "[code=js]aaa < bbb > ccc[/code]";
            Assert.AreEqual(@"<pre class=""brush: js;"">aaa &lt; bbb &gt; ccc</pre>" + UbbEncoder.UbbCodeHighlighter, origin.UbbEncode());
        }

        [TestMethod]
        public void Test4()
        {
            const string origin = "[b]aaa < bbb > ccc[/b]";
            Assert.AreEqual("<b>aaa &lt; bbb &gt; ccc</b>", origin.UbbEncode());
        }

        [TestMethod]
        public void Test5()
        {
            const string origin = "[url]http://www.resounding.com[/url]";
            Assert.AreEqual("<a href=\"http://www.resounding.com\" target=\"_blank\">http://www.resounding.com</a>", origin.UbbEncode());
        }

        [TestMethod]
        public void Test6()
        {
            const string origin = "[url=http://www.resounding.com]test[/url]";
            Assert.AreEqual("<a href=\"http://www.resounding.com\" target=\"_blank\">test</a>", origin.UbbEncode());
        }
    }
}
