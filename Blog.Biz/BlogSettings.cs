namespace Blog.Biz
{
    public static class BlogSettings
    {
        public static readonly bool AllowRegister = true;
        public static readonly string RegSolt = "";
        public static readonly int RegTimeDiffSecs = 300;
        public static readonly int RegTimeXbits = 11;
        public static readonly int RegTimeRandMax = 1000000;

        public static readonly string LoginCookie = "LeafingBlogLoginUser";
        public static readonly string TitleName = "title";
        public static readonly string PathName = "path";
        public static readonly string HomeName = "Home";
        public static readonly string MenuSpliter = " > ";
        public static readonly char ParameterSpliter = '_';
        public static readonly string BackToUrl = "BackTo";

        public static readonly string SiteBase = "http://localhost:1786/blog";
        public static readonly string BlogName = "";

        public static readonly string EmailAddress = "";
        public static readonly string EmailSmtpServer = "";
        public static readonly string EmailUsername = "";
        public static readonly string EmailPassword = "";

        public static readonly string CopyrightTemplate = "{0}";

        static BlogSettings()
        {
            typeof(BlogSettings).Initialize();
        }
    }
}
