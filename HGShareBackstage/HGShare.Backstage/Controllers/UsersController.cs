using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using HGShare.Backstage.Controllers.Base;
using HGShare.Business;
using HGShare.FileManager.Avatar;
using HGShare.Site;
using HGShare.Site.ActionResult;
using HGShare.Site.Config;
using HGShare.VWModel;
using Webdiyer.WebControls.Mvc;

namespace HGShare.Backstage.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Description("用户管理")]
    public class UsersController : BaseController
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        [Description("用户列表")]
        public ActionResult Index(int? page,string username)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count, pageCount;
            var result = Users.UserInfosToVModels(Users.GetUserPageList(pageIndex, pageSize, null, null,username, out pageCount, out count));
            if(result!=null)
                result.ForEach(n =>
                {
                    var roleInfo = Roles.GetRoleInfo(n.RoleId);
                    if(roleInfo!=null)n.RName = roleInfo.RName;
                });
            PagedList<UserVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            ViewBag.UserName = username;
            return View(pl);
        }
        /// <summary>
        /// 更新最后登陆时间
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [RoleAuthorizeIgnore]
        [AutoRequestIgnore]
        [Description("更新用户在线时间")]
        public async Task<ActionResult> UpdateUserLastTime()
        {
            var result = new JsonResultModel
            {
                ResultState = true
            };
            if (CurrentUserInfo == null)
            {
                result.ResultState = false;
                result.Message = "无登陆信息！";
            }
            else
            {
                bool state =await Users.UpdateOnLineTime(CurrentUserInfo.Id);
                if (!state)
                {
                    result.ResultState = false;
                    result.Message = "更新失败！";
                }
            }
            return Json(result);
        }
        /// <summary>
        /// 修改密码View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("修改密码页面")]
        public ActionResult ModifyPassWord()
        {
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPassWord"></param>
        /// <param name="newPassWord"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("修改密码操作")]
        public JsonResult ModifyPassWord(string oldPassWord, string newPassWord)
        {
            bool resultStatus = false;
            string msg = "操作失败!";
            if (string.IsNullOrEmpty(oldPassWord))
            {
                msg = "请输入旧密码！";
            }
            else if (string.IsNullOrEmpty(newPassWord))
            {
                msg = "请输入新密码！";
            }
            else if (!Users.CheckPassword(CurrentUserInfo.Name, oldPassWord))
            {
                msg = "旧密码输入错误！";
            }
            else if (Users.ModifyPassWord(CurrentUserInfo.Name, newPassWord))
            {
                resultStatus = true;
                msg = "更新成功";
            }

            var result=new JsonResultModel
            {
                ResultState = resultStatus,
                Message = msg
            };
            return Json(result);
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("添加用户页面")]
        public ActionResult Add()
        {
            var vm=new UserVModel
            {
                Roles = Roles.RoleInfosToVModels(Roles.GetAllRole())
            };
            return View(vm);
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("添加用户操作")]
        public JsonResult Add(UserVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add user
                result.ResultState = Users.AddUser(Users.UserVModelToInfo(model));
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Description("检测用户名")]
        public JsonResult CheckUserName(string Name,int? Id)
        {
            var result = Users.CheckName(Name, Id);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 检测昵称是否存在
        /// </summary>
        /// <param name="NickName"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Description("检测昵称")]
        public JsonResult CheckNickName(string NickName, int? Id)
        {
            var result = Users.CheckNickName(NickName, Id);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 检测邮箱是否存在
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Description("检测邮箱")]
        public JsonResult CheckEmail(string Email, int? Id)
        {
            var result = Users.CheckEmail(Email, Id);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("修改用户页面")]
        public ActionResult Edit(int id)
        {
            var model = Users.UserInfoToVModel(Users.GetUserInfo(id));
            model.Roles = Roles.RoleInfosToVModels(Roles.GetAllRole());
            return View(model);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("修改用户操作")]
        public JsonResult Edit(UserVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = Users.UpdateUser(Users.UserVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
        
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("删除用户操作")]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel {ResultState = Users.DeleteUser(id) > 0};
            return Json(result);
        }
        /// <summary>
        /// 删除User
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
        [Description("删除多个User操作")]
        public JsonResult Deletes(int[] ids)
        {
            var result = new JsonResultModel { ResultState = Users.DeleteUsers(ids) > 0 };
            return Json(result);
        }
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("上传头像页面")]
        public ActionResult UploadAvatar(int id)
        {
            var model = Users.UserInfoToVModel(Users.GetUserInfo(id));
            return View(model);
        }
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("保存头像")]
        public async Task<JsonResult> UploadAvatar(UpdateAvatarVModel model)
        {
            try
            {
                var uploadAvatar = new UploadAvatar(model.ImageBase64);
                uploadAvatar.Save();
                string oldFile = string.Empty;
                var user = Users.GetUserInfo(model.Id);
                if (user != null)
                    oldFile = user.Avatar;

                Users.UpdateAvatar(model.Id, uploadAvatar.FileName);

                if (!string.IsNullOrEmpty(oldFile))
                {
                    //删除旧头像
                    await uploadAvatar.DeleteFileAndThumbnailsAsync(oldFile);
                }

                var result = new JsonResultModel { ResultState =true };
                return Json(result);
            }
            catch (Exception ex)
            {
                var result = new JsonResultModel { ResultState = false,Message = ex.Message};
                return Json(result);
            }
        }
        /// <summary>
        /// 更新用户禁用状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="disable"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("更新用户禁用状态")]
        public JsonResult Disable(int id, bool disable, string reason)
        {
            reason = Server.UrlDecode(reason);
            bool status = Users.UpdateDisable(id, disable, reason);
            return Json(new JsonResultModel { ResultState = status, Message = (status ? "完成!" : "失败!") });
        }
    }
}