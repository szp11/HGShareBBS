using System.Security.Cryptography;
using System.Text;

namespace HGShare.Common
{
    public class Md5Helper
    {
        //public static string GetMd5(string str)
        //{
        //    Encoder enc = Encoding.Unicode.GetEncoder();
        //    var unicodeText = new byte[str.Length * 2];
        //    enc.GetBytes(str.ToCharArray(), 0, str.Length, unicodeText, 0, true);
        //    using (MD5 md5 = new MD5CryptoServiceProvider())
        //    {
        //        byte[] result = md5.ComputeHash(unicodeText);
        //        var sb = new StringBuilder();
        //        foreach (byte t in result)
        //        {
        //            sb.Append(t.ToString("X2"));
        //        }

        //        return sb.ToString().Replace("-", "");
        //    }
        //}
        public static string Md5(string str)
        {
            //微软md5方法参考return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            var sb = new StringBuilder();
            foreach (byte t in b)
                sb.Append(t.ToString("x").PadLeft(2, '0'));
            return sb.ToString();
        }
    }
}
