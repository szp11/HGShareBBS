using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.Common;

namespace HGShare.Site.Token
{
    /// <summary>
    /// 密码密令
    /// </summary>
    public class PwdToken : IToken
    {
        public string Key { get; set; }

        public PwdToken(string key, string password)
        {
            Key = key;
            Password = password;
        }

        public string Password { get; set; }


        public string GetToken()
        {
            return Md5Helper.Md5(Password + Key);
        }
    }
}
