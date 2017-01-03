using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.Common;
using HGShare.Site.Config;

namespace HGShare.Site.Token
{
    public class EmailActivateToken:IToken
    {
        /// <summary>
        /// 
        /// </summary>
        private int UserId { get; set; }
        private string Email { get; set; }

        private string Key { get; set; }

        public EmailActivateToken(int userId, string email)
        {
            UserId = userId;
            Email = email;
            Key = TokenConfig.ActivateTokenKey;
        }


        public  string GetToken()
        {
            //邮件激活令牌，每次都是唯一的
            Guid guid = Guid.NewGuid();
            return Md5Helper.Md5(UserId + Email + Key + guid.ToString());
        }
    }
}
