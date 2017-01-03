using System.Linq;
using System.Web;

namespace HGShare.Common
{
    /// <summary>
    /// Fetch 的摘要说明。
    /// </summary>
    public class Fetch
    {
        ///// <summary>
        ///// 获取Url后面的值
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //public static string Get(string name)
        //{
        //    string text1 = HttpContext.Current.Request.QueryString[name];
        //    return ((text1 == null) ? "" : text1.Trim());
        //}

        ///// <summary>
        ///// 获取表单Post过来的值
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //public static string Post(string name)
        //{
        //    string text1 = HttpContext.Current.Request.Form[name];
        //    return ((text1 == null) ? "" : text1.Trim());
        //}

        ///// <summary>
        ///// 获取Url后面的值，如.....aspx?productid=2将获取到"2"
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //public static int GetQueryId(string name)
        //{
        //    int id;
        //    int.TryParse(Get(name), out id);
        //    return id;
        //}

        ///// <summary>
        ///// 获取表单Post过来的值，如表单checkboxlist传ids:2,3,5过来，将是int[]{2,3,4}
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //public static int[] GetIds(string name)
        //{
        //    var ids = Post(name);
        //    int id = 0;
        //    var array = ids.Split(',');

        //    return (from a in array where int.TryParse(a.Trim(), out id) select id).ToArray();
        //}

        ///// <summary>
        ///// 获取Url过来的值，如.....aspx ? productid=2 & productid=3，将是int[]{2,3}
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //public static int[] GetQueryIds(string name)
        //{
        //    var ids = Get(name);
        //    int id = 0;
        //    var array = ids.Split(',');

        //    return (from a in array where int.TryParse(a.Trim(), out id) select id).ToArray();
        //}

        ///// <summary>
        ///// 获取当前页面的Url
        ///// </summary>
        //public static string CurrentUrl
        //{
        //    get { return HttpContext.Current.Request.Url.ToString(); }
        //}

        ///// <summary>
        ///// 获取当前页面的主域，如www.GMS.com主域是GMS.com
        ///// </summary>
        //public static string ServerDomain
        //{
        //    get
        //    {
        //        string urlHost = HttpContext.Current.Request.Url.Host.ToLower();
        //        string[] urlHostArray = urlHost.Split(new[] {'.'});
        //        if ((urlHostArray.Length < 3) || RegExp.IsIp(urlHost))
        //        {
        //            return urlHost;
        //        }
        //        string urlHost2 = urlHost.Remove(0, urlHost.IndexOf(".", System.StringComparison.Ordinal) + 1);
        //        if ((urlHost2.StartsWith("com.") || urlHost2.StartsWith("net.")) ||
        //            (urlHost2.StartsWith("org.") || urlHost2.StartsWith("gov.")))
        //        {
        //            return urlHost;
        //        }
        //        return urlHost2;
        //    }
        //}

        /// <summary>
        /// 获取访问用户的IP
        /// </summary>
        public static string Ip
        {
            get
            {
                string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                switch (result)
                {
                    case null:
                    case "":
                        result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        break;
                }
                if (!RegExp.IsIp(result))
                {
                    return "Unknown";
                }
                return result;
            }
        }

        /// <summary>
        /// 获取访问用户的IP
        /// </summary>
        public static string UserAgent
        {
            get { return HttpContext.Current.Request.UserAgent ?? "";}
        }
    }
}
