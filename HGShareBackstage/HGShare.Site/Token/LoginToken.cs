using HGShare.Common;

namespace HGShare.Site.Token
{
    /// <summary>
    /// 登陆密令
    /// </summary>
    public class LoginToken : IToken
    {
        /// <summary>
        /// 用户标示
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public long TimeStamp { get; set; }
        /// <summary>
        /// 加密钥匙key
        /// </summary>
        public string Key { get; set; }

        public LoginToken(string userId, long timeStamp, string key)
        {
            UserId = userId;
            TimeStamp=timeStamp;
            Key = key;
        }


        /// <summary>
        /// 得到令牌
        /// </summary>
        /// <returns></returns>
        public  string GetToken()
        {
            return Md5Helper.Md5(UserId + TimeStamp + Key);
        }
    }
}
