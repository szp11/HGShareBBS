using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGShare.DataProvider
{
    public class DbConfig
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static String ArticleManagerConnString
        {
            get { return ConfigurationManager.ConnectionStrings["ArticleManagerConnString"].ConnectionString; }
        }
    }
}
