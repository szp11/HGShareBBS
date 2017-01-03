using System.Globalization;

namespace HGShare.Common
{
    public class PinYinHelper
    {
        /// <summary>
        /// 中文转拼音
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isall">转换所有/转换首字母</param>
        /// <returns></returns>
        public static string ConvertToPinYin(string str, bool isall = true)
        {
            string pYstr = string.Empty;
            foreach (char item in str)
            {
                string temp;
                if (Microsoft.International.Converters.PinYinConverter.ChineseChar.IsValidChar(item))
                {
                    var cc = new Microsoft.International.Converters.PinYinConverter.ChineseChar(item);

                    temp = cc.Pinyins[0].Substring(0, cc.Pinyins[0].Length - 1);
                }
                else
                {
                    temp = item.ToString(CultureInfo.InvariantCulture);
                }
                if (isall)
                {
                    pYstr += temp;
                }
                else
                {
                    pYstr += temp.Substring(0, 1);
                }
            }
            return pYstr.ToLower();
        }
    }
}
