using System.Data;
using System.IO;
using Leafing.Data;
using Leafing.Core;
using Leafing.Web.Mvc;
using System.Web;

namespace Blog.Biz.Controllers
{
    public class ManageController : ControllerBase
    {
        public void RunSql()
        {
            try
            {
                string sql = Bind("sql");
                this["sql"] = sql;
                if (sql.IsNullOrEmpty())
                {
                    this["ds"] = new DataSet();
                }
                else if (sql.ToLower().Trim().StartsWith("select "))
                {
                    var ds = DbEntry.Provider.ExecuteDataset(sql);
                    this["ds"] = ds;
                    Flash.Notice = string.Format("运行SQL完成{0}", ds.Tables[0].Rows.Count > 0 ? "" : "，未发现记录");
                }
                else
                {
                    this["ds"] = new DataSet();
                    var n = DbEntry.Provider.ExecuteNonQuery(sql);
                    Flash.Notice = string.Format("运行SQL完成，影响{0}条记录", n);
                }
            }
            catch (System.Exception ex)
            {
                this["ds"] = new DataSet();
                Flash.Warning = ex.ToString().ToHtml().Replace("\n", "<br>");
            }
        }

        public void List()
        {
        }

        public void Memory()
        {
        }

        public string Backup()
        {
            try
            {
                var srcPath = SystemHelper.BaseDirectory + "App_Data\\blog.fdb";
                var tgtPath = SystemHelper.BaseDirectory + "blog.fdb";
                File.Copy(srcPath, tgtPath, true);
                Flash.Notice = "备份数据库成功";
            }
            catch (System.Exception ex)
            {
                Flash.Warning = ex.ToString().ToHtml().Replace("\n", "<br>");
            }
            return UrlTo<ManageController>(p => p.List());
        }
    }
}
