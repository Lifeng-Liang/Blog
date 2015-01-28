MyBlog
==========

简介
----------

这是 [我的网站](http://llf.hanzify.org) 使用的源代码，可以作为一个使用 [DbEntry.Net](http://dbentry.codeplex.com/) 开发实际项目的例子。

因为是我自己用的，所以有一些地方是不完善的。

大部分程序是2008年左右写的，现在只是进行了一些小改动，以便能使用最新版的 DbEntry.Net。

另外，加入了 Markdown 的支持，这个是使用 [MarkdownSharp](https://code.google.com/p/markdownsharp/) 实现的。

页面部分，为了在手机上显示正常，在电脑上显示的反而不协调，也不美观，有时间的话，下一步准备先改一下这里。

其中的 ubb 中 code 部分，是使用 [SyntaxHighlighter](http://alexgorbatchev.com/) 实现的。Markdown 的 code 部分，准备使用 [prettify](https://code.google.com/p/google-code-prettify/)，如果好用的话，ubb 的也改成这个。

目前，它还只是一个单人使用的 blog，普通的注册用户，只是在发评论和搜索的时候有一些方便，只有管理员才能发表文章。

配置
----------

关于 DbEntry 部分，缺省是 SQLite 1.0.66.0 用于开发，Firebird 2.5.2 用于部署，具体的配置可以参见 DbEntry 的站点，这里就不多说了。

appSettings 里面，是大多数情况下都要修改的部分。RegSolt 用于对于密码 hash 加料，需要修改成一个随意的值，正式用之后就不能再改了，否则现有用户将无法登录。Email 相关部分需要改成你自己的 email 服务器的信息，用于注册用户邮箱的验证。

rss 生成部分，站点部分，在页面里是硬编码为 http://llf.hanzify.org 的，也应修改，具体页面为 /View/Rss 下的 Article.aspx 和 Comment.aspx 。

历史列表部分，使用的是我在 DbEntry 里引入的静态模式，如果希望改成动态模式，修改 Blog.Biz.Controllers.ArticleController 上的 Attribute ListStyle 就可以了。

第一次运行时，会自动创建所需要的表，并且插入一些初始数据，比如本文章，这个是在 Global.asax.cs 里实现的，如果需要，也可修改。

为调试方便，log 配置为记录所有的 sql 语句，这会导致 log 文件迅速增长，如不需要，可以删除配置文件中 SqlLogRecorder 那一行。

使用
----------

1.  注册你的帐号
2.  使用 test@test.com/123 登录系统
3.  在【管理 -> 用户管理】页面，将新注册的用户改为管理员
4.  退出，用新帐号登录系统
5.  删除 test@test.com 的帐号

已知问题
----------

*   ubb 部分，实现的不完全，也不严格。
*   图片部分，只提供了链接输入方式，图片本身需要另找途径上传到站点。
*   发表和修改文章的页面，没有预览，只能直接发布。
*   Articles 表因为某些原因，改成了软删除模式，但是 Comments 和 Statistics 还没有改，会造成某些情况下出错。
*   Controllers 里绑定值，因为历史原因，只是单个值绑定，因为时间原因，这次仍然保留了这个做法。
*   这次升级，取消了Session，但是还没来得及修改数据库，所以登录时选不选【Remember Me】都是浏览器关闭即失效。
