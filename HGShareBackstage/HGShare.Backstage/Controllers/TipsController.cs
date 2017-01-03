using System.ComponentModel;
using System.Web.Mvc;
using HGShare.VWModel;

namespace HGShare.Backstage.Controllers
{
    /// <summary>
    /// 提示中心
    /// </summary>
    [Description("提示中心")]
    public class TipsController : Controller
    {
        /// <summary>
        /// 提示
        /// </summary>
        /// <returns></returns>
        [Description("提示页面")]
        public ActionResult Tips(TipsVModel info)
        {
            return View(info);
        }

    }
}