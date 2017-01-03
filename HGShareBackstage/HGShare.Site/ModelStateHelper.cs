using System.Text;
using System.Web.Mvc;

namespace HGShare.Site
{
    /// <summary>
    /// 实体验证工具
    /// </summary>
    public class ModelStateHelper
    {
        /// <summary>
        /// 得到所有验证错误信息
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="splitStr"></param>
        /// <returns></returns>
        public static string GetAllErrorMessage(ModelStateDictionary modelState,string splitStr="<br />")
        {
            var sbErrors = new StringBuilder();
            foreach (var item in modelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    for (int i = item.Errors.Count - 1; i >= 0; i--)
                    {
                        sbErrors.Append(item.Errors[i].ErrorMessage);
                        sbErrors.Append(splitStr);
                    }
                }
            }
            return sbErrors.ToString();
        }
    }
}
