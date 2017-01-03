using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using HGShare.Backstage.Controllers.Base;
using HGShare.Business;
using HGShare.Common;
using HGShare.Site;
using HGShare.Site.ActionResult;
using HGShare.Site.Config;
using HGShare.VWModel;
using Webdiyer.WebControls.Mvc;

namespace HGShare.Backstage.Controllers
{
    /// <summary>
    /// 文章类型管理
    /// </summary>
    [Description("文章类型管理")]
    public class ArticleTypesController : BaseController
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [Description("文章类型列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count, pageCount;
            var result = ArticleTypes.ArticleTypeInfosToVModels(ArticleTypes.GetArticleTypePageList(pageIndex, pageSize, null, null, out pageCount, out count));
            PagedList<ArticleTypeVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
        /// <summary>
        /// 删除ArticleType
        /// </summary>
        /// <param name="id">类型Id</param>
        /// <returns></returns>
        [HttpPost]
        [Description("删除文章类型操作")]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel { ResultState = ArticleTypes.DeleteArticleType(id) > 0 };
            return Json(result);
        }
        /// <summary>
        /// 删除ArticleType
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
        [Description("删除多个文章类型操作")]
        public JsonResult Deletes(int [] ids)
        {
            var result = new JsonResultModel { ResultState = ArticleTypes.DeleteArticleTypes(ids) > 0 };
            return Json(result);
        }
        /// <summary>
        /// 添加ArticleType
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("添加文章类型页面")]
        public ActionResult Add()
        {
            var vm = new ArticleTypeVModel()
            {
                PId = 0,
                PName = ArticleTypes.RootType
            };
            return View(vm);
        }
        /// <summary>
        /// 添加ArticleType
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [Description("添加文章类型操作")]
        public JsonResult Add(ArticleTypeVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                model.PinYin = PinYinHelper.ConvertToPinYin(model.Name);

                //add 
                result.ResultState = ArticleTypes.AddArticleType(ArticleTypes.ArticleTypeVModelToInfo(model)) > 0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
        /// <summary>
        /// 修改ArticleType
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("修改文章类型页面")]
        public ActionResult Edit(int id)
        {
            var model = ArticleTypes.ArticleTypeInfoToVModel(ArticleTypes.GetArticleTypeInfo(id));
            if (model.PId != 0)
            {
                var parent = ArticleTypes.GetArticleTypeInfo(model.PId);
                if (parent != null)
                    model.PName = parent.Name;
            }
            return View(model);
        }
        /// <summary>
        /// 修改ArticleType
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [Description("修改文章类型操作")]
        public JsonResult Edit(ArticleTypeVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = ArticleTypes.UpdateArticleType(ArticleTypes.ArticleTypeVModelToInfo(model)) > 0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }

        /// <summary>
        /// 文章类型选择页面
        /// </summary>
        /// <param name="id">需要排除的id</param>
        /// <param name="pid">要默认选中的id</param>
        /// <param name="did">要默认禁用的id</param>
        /// <returns></returns>
        [HttpGet]
        [RoleAuthorizeIgnore]
        [Description("文章类型选择页面")]
        public ActionResult ArticleTypeSelect(int? id,int? pid,int? did)
        {
            //tree data
            var model = ArticleTypes.ArticleTypeInfosToTreeVModels(ArticleTypes.GetAllArticleType(id));
            ViewBag.SelectedId = pid;
            ViewBag.disableid = did;
            return View(model);
        }
    }
}

