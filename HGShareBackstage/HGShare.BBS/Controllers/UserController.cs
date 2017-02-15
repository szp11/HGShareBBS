using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using HGShare.BBS.Controllers.Base;
using HGShare.BBS.Models;
using HGShare.Site;
using HGShare.Site.ActionResult;
using HGShare.Site.Config;
using HGShare.Site.Token;
using HGShare.Utils.Interface;
using HGShare.VWModel;
using HGShare.Web.Interface;
using Webdiyer.WebControls.Mvc;

namespace HGShare.BBS.Controllers
{
    public class UserController : BaseController
    {
        private static readonly IUsers Users = IcoReader.Service<IUsers>();
        private static readonly IUsersPublic UserPublic = IcoReader.Service<IUsersPublic>();
        private static readonly IUpload Upload = IcoReader.Service<IUpload>();
        private static readonly ILogin UsersLogin = IcoReader.Service<ILogin>();
        private static readonly IUserActivateTokensPublic UserActivateTokensPublic = IcoReader.Service<IUserActivateTokensPublic>();
        private static readonly ISendMailLogsPublic SendMailLogsPublic = IcoReader.Service<ISendMailLogsPublic>();
        private static readonly IVerifyCode VerCode = IcoReader.Service<IVerifyCode>();
        private static readonly IArticles Articles = IcoReader.Service<IArticles>();
        private static readonly IComments Comments = IcoReader.Service<IComments>();

        /// <summary>
        /// 用户主页（是可以被任何人看到的，包括游客蜘蛛，内容使用同步加载）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="aPageIndex"></param>
        /// <param name="cPageIndex"></param>
        /// <returns></returns>
        [Description("个人主页")]
        public async Task<ActionResult> Home(int? id,int? aPageIndex,int? cPageIndex)
        {
            //个人主页
            if (!id.HasValue)
                return RedirectToAction("UserCenter");

            if (!aPageIndex.HasValue || aPageIndex.Value < 1)
                aPageIndex = 1;
            if (!cPageIndex.HasValue || cPageIndex.Value < 1)
                cPageIndex = 1;
            var entity=new HomeEntity();

            entity.User = Users.GetUserById(id.Value);
            if (entity.User == null || entity.User.IsNull)
                return new RedirectResult(Url.Action("Tip404", "Tips"));
            entity.UserOther = await Users.GetUserOtherById(id.Value);
            entity.Positions = await Users.GetUserPositionById(entity.User.Id);

            entity.Articles = (await Articles.SearchArticlesByUserId(id.Value, 1, aPageIndex.Value, PageConfig.UserHomeArticlePageSize)).ToPagedList(aPageIndex.Value, PageConfig.UserHomeArticlePageSize);
            entity.Articles.TotalItemCount = await Articles.SearchArticlesCountByUserId(id.Value, 1);
            entity.Articles.CurrentPageIndex = aPageIndex.Value;

            entity.Comments = (await Comments.SearchCommentsByUserId(id.Value, 1, cPageIndex.Value, PageConfig.UserHomeCommentPageSize)).ToPagedList(cPageIndex.Value, PageConfig.UserHomeCommentPageSize);
            entity.Comments.TotalItemCount = await Comments.SearchCommentsCountByUserId(id.Value, 1);
            entity.Comments.CurrentPageIndex = cPageIndex.Value;
            return View(entity);
        }

        /// <summary>
        /// 个人中心(仅自己看，使用ajax异步加载，并分页)
        /// </summary>
        /// <returns></returns>
        [Description("用户中心")]
        [RoleAuthorize]
        public async Task<ActionResult> UserCenter()
        {
            int userId = CurrentUserInfo.Id;
            
            var entity=new UserCenterEntity();
            entity.User = Users.GetUserById(userId);
            if(entity.User==null || entity.User.IsNull)
                return new RedirectResult(Url.Action("Tip404","Tips"));

            entity.Positions = await Users.GetUserPositionById(userId);
            //刷新缓存中的用户信息
            UsersLogin.RefreshCookieUserInfo(userId);
            return View(entity);
        }
        /// <summary>
        /// 用户设置
        /// </summary>
        /// <returns></returns>
        [Description("用户设置")]
        [RoleAuthorize]
        public async Task<ActionResult> UserSet()
        {
            var model = new UserEntity {User = Users.GetUserById(CurrentUserInfo.Id)};
            if (model.User != null && !model.User.IsNull)
            {
                model.UserPositions =await Users.GetUserPositionById(model.User.Id);
                model.UserOther = await Users.GetUserOtherById(model.User.Id);
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Description("设置基本信息")]
        [HttpPost]
        [RoleAuthorize]
        public async Task<ActionResult> SetInfo(UserVModel user, UserOtherVModel other, string province, string city,string area)
        {
            ModelState.Remove("Name");
            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("Password");
            ModelState.Remove("RoleId");
            ModelState.Remove("Avatar");
            if (!ModelState.IsValid)
                return Json(new JsonResultModel {Message = ModelStateHelper.GetAllErrorMessage(ModelState)});
            user.Id = CurrentUserInfo.Id;
            other.UserId = CurrentUserInfo.Id;

            var olduser = Users.GetUserById(CurrentUserInfo.Id);
            //邮箱修改需要重新验证
            if (olduser.Email.ToLower() != user.Email.ToLower())
                user.EmailStatus = false;
            else
                user.EmailStatus = olduser.EmailStatus;

            //修改基本信息
            var status = UserPublic.UpdateUser(user);
            if (!status)
                return Json(new JsonResultModel {Message = "修改基本信息失败！"});
            var userOther = await Users.GetUserOtherById(user.Id);
            if (userOther==null || userOther.IsNull)
                status = await UserPublic.AddUserOther(other)>0;
            else
                status = UserPublic.UpdateUserOther(other);
            if (!status)
                return Json(new JsonResultModel {Message = "修改信息失败！"});

            await UserPublic.DeleteUserPosition(CurrentUserInfo.Id);
            #region 地址
            if (!string.IsNullOrEmpty(province))
            {
                int pid = await
                    UserPublic.AddUserPosition(new UserPositionVModel
                    {
                        Code = int.Parse(province),
                        Type = 0,
                        UserId = CurrentUserInfo.Id
                    });
                if(pid==0)
                    return Json(new JsonResultModel { Message = "修改居住地区信息失败,！" });
            }
            if (!string.IsNullOrEmpty(city))
            {
                int cid = await
                    UserPublic.AddUserPosition(new UserPositionVModel
                    {
                        Code = int.Parse(city),
                        Type = 1,
                        UserId = CurrentUserInfo.Id
                    });

                if (cid == 0)
                    return Json(new JsonResultModel { Message = "修改居住地区信息失败,！" });
            }
            if (!string.IsNullOrEmpty(area))
            {
                int aid = await
                    UserPublic.AddUserPosition(new UserPositionVModel
                    {
                        Code = int.Parse(area),
                        Type = 2,
                        UserId = CurrentUserInfo.Id
                    });
                if (aid == 0)
                    return Json(new JsonResultModel { Message = "修改居住地区信息失败,！" });
            }

            #endregion
            UsersLogin.RefreshCookieUserInfo(CurrentUserInfo.Id);
            return Json(new JsonResultModel{ResultState = true,Message = "修改成功！"});
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="oldPassword"></param>
        /// <returns></returns>
        [Description("设置密码")]
        [HttpPost]
        [RoleAuthorize]
        public ActionResult SetPassword(UserVModel user,string oldPassword)
        {
            if (string.IsNullOrEmpty(oldPassword) || !UserPublic.CheckUserPassword(user.Id, oldPassword))
                return Json(new JsonResultModel {Message = "旧密码输入错误！"});
            ModelState.Remove("Name");
            ModelState.Remove("NickName");
            ModelState.Remove("Sex");
            ModelState.Remove("RoleId");
            ModelState.Remove("Avatar");
            ModelState.Remove("Email");
            if (!ModelState.IsValid)
                return Json(new JsonResultModel { Message = ModelStateHelper.GetAllErrorMessage(ModelState) });
            var status = UserPublic.UpdatePassword(user.Id, user.Password);
            return Json(new JsonResultModel {ResultState = status,Message = status?"修改成功！":"修改失败！"});


        }
        /// <summary>
        /// 保存头像
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("保存头像")]
        [RoleAuthorize]
        public async Task<JsonResult> UploadAvatar(string imageBase64)
        {
            if(string.IsNullOrEmpty(imageBase64))
                return Json(new JsonResultModel { Message = "图片信息错误！" });
            if (imageBase64.Length > WebSysConfig.AvatarMaxSize)
                return Json(new JsonResultModel { Message = "图片过大，换个图试试！" });
            try
            {
                string oldFile = string.Empty;
                string fileName = Upload.UploadAvatar(imageBase64);
                
                var user = Users.GetUserById(CurrentUserInfo.Id);
                if (user != null &&  !user.IsNull)
                    oldFile = user.Avatar;

                bool status = UserPublic.UpdateAvatar(CurrentUserInfo.Id, fileName);
                UsersLogin.RefreshCookieUserInfo(CurrentUserInfo.Id);
                if (status)
                {
                    if (!string.IsNullOrEmpty(oldFile))
                        await Upload.DeleteAvatar(oldFile);
                    return Json(new JsonResultModel {ResultState = true, Message = "保存成功！"});
                }
            }
            catch (Exception ex)
            {
                return Json(new JsonResultModel {Message = ex.Message });
            }
            return Json(new JsonResultModel { Message = "保存失败！" });
        }
        /// <summary>
        /// 激活账号页面
        /// </summary>
        /// <returns></returns>
        [Description("激活账号页面")]
        [HttpGet]
        [RoleAuthorize]
        public ActionResult Activate()
        {
            var user = Users.GetUserById(CurrentUserInfo.Id);
            if (user == null || user.IsNull)
                return new RedirectResult(Url.Action("Tip404", "Tips"));
            return View(user);
        }
        /// <summary>
        /// 激活账号邮件
        /// </summary>
        /// <returns></returns>
        [Description("激活账号邮件")]
        [HttpPost]
        [RoleAuthorize]
        public ActionResult Activate(int? ig)
        {
            var user = Users.GetUserById(CurrentUserInfo.Id);
            if (user == null || user.IsNull)
                return Json(new JsonResultModel { Message = "用户信息有误，发送激活邮件失败！" });
            if (user.EmailStatus)
                return Json(new JsonResultModel { Message = "您的邮箱已经激活，无需再次激活！"});

            #region 发送邮件限制检测
            string msg;
            bool  checkResult= SendMailLogsPublic.CheckUserEmailAvailable(user.Id, UserConfig.SendEmailInterval,
                UserConfig.SendEmailIntervalMaxNum, UserConfig.SendEmailToDayMaxNum, out msg);
            if (!checkResult)
                return Json(new JsonResultModel { Message = msg });
            #endregion

            #region 写入激活信息
            IToken token = new EmailActivateToken(user.Id, user.Email);
            string tokenCode = token.GetToken();
            var tokeninfo = new UserActivateTokenVModel()
            {
                UserId = CurrentUserInfo.Id,
                Email = user.Email,
                Token = tokenCode
            };
            long id= UserActivateTokensPublic.Add(tokeninfo);
            if (id <= 0)
                return Json(new JsonResultModel {Message = "写入激活信息失败,请重试！"});
            #endregion

            #region 发送激活邮件
            string title = WebSysConfig.WebName + "激活邮件";
            var values = new Dictionary<string, object>
            {
                {"$Title", title},
                {"$UserName", user.NickName ?? user.Name},
                {"$Url", "http://"+Request.Url.Host+Url.Action("activateemail","user",new{token=tokenCode})},
                {"$WebName", WebSysConfig.WebName},
                {"$TimeDsc", WebSysConfig.ActivateTokenExpireTime+"分钟内"}
            };
            bool status = UserPublic.SendMail(CurrentUserInfo.Id, CurrentUserInfo.Id, Common.Fetch.Ip, user.Email,EmailTemplateConfig.EmailActivateTemplate,values,"UTF-8",true);
            if (status)
                return Json(new JsonResultModel { ResultState = true, Message = "激活邮件发生成功，请进入邮箱进行激活！" });
            #endregion

            return Json(new JsonResultModel { Message = "邮件发送失败！" });
        }
        /// <summary>
        /// 激活账号操作
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [Description("激活账号操作")]
        [HttpGet]
        public ActionResult ActivateEmail(string token)
        {
            ViewBag.UserIsLogin = UserIsLogin;
            if (!UserIsLogin)
                ShowWarning("您必须登入后，才能通过该链接进行激活操作!");
            if(string.IsNullOrEmpty(token))
                ShowWarning("令牌有误，激活失败!");
             var user = Users.GetUserById(CurrentUserInfo.Id);
             if (user == null || user.IsNull)
                ShowWarning("用户信息有误，激活失败!");
            if (user.EmailStatus)
                ShowWarning("你已经激活过了，就不要来捣乱了!");
            //进行激活
            long tokenId = UserActivateTokensPublic.CheckToken(CurrentUserInfo.Id, user.Email, token,WebSysConfig.ActivateTokenExpireTime);
            if(tokenId<=0)
                ShowWarning("令牌信息有误，激活失败!");
            bool status = UserPublic.UpdateEmailStatus(CurrentUserInfo.Id, true);

            if(!status)
                ShowWarning("激活失败啦，我也不知道怎么回事!");

            //激活成功后 将该令牌置为无效
            UserActivateTokensPublic.UpdateStatus(tokenId,false);
            //重置缓存的用户信息
            UsersLogin.RefreshCookieUserInfo(CurrentUserInfo.Id);
            return RedirectToAction("activate");

        }
        /// <summary>
        /// 忘记密码页面
        /// </summary>
        /// <returns></returns>
        [Description("忘记密码页面")]
        [HttpGet]
        public ActionResult ForgetPassword(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                ViewBag.Token = token;
                ViewBag.TokenError = false;
                //邮件中的回调
                //进行激活
                long tokenId = UserActivateTokensPublic.CheckToken(token, WebSysConfig.ActivateTokenExpireTime);
                if (tokenId <= 0)
                {
                   ViewBag.TokenError = true;
                    return View();
                }

                return View();
            }
            if (UserIsLogin)
            {
                var user = Users.GetUserById(CurrentUserInfo.Id);
                if (user != null && !user.IsNull)
                    ViewBag.Email = user.Email;
            }
            return View();
        }
        /// <summary>
        /// 忘记密码页面
        /// </summary>
        /// <returns></returns>
        [Description("发送找回密码邮件")]
        [HttpPost]
        public ActionResult ForgetPassword(string email, string vercode)
        {
            if (string.IsNullOrEmpty(email))
                return Json(new JsonResultModel { Message = "请输入您的邮箱地址！" });
            if (string.IsNullOrEmpty(vercode))
                return Json(new JsonResultModel { Message = "请输入验证码！" });
            if (!VerCode.CheckVerifyCode(vercode))
                return Json(new JsonResultModel { Message = "验证码错误，请重新输入！" });
            UserVModel user;
            if (UserIsLogin)
            {
                //已登陆
                user = Users.GetUserById(CurrentUserInfo.Id);
                if (user == null || user.IsNull)
                    return Json(new JsonResultModel {Message = "您的信息有误，操作失败！"});
                if(user.Email.ToLower()!=email.ToLower())
                    return Json(new JsonResultModel { Message = "您的邮箱有误，请确认您个人信息中的邮箱！" });
            }
            else
            {
                //未登陆
                //先查出用户id
                user = Users.GetUserByEmail(email);
                if (user == null || user.IsNull)
                    return Json(new JsonResultModel { Message = "您的邮箱地址不存在，请您确认！" });
            }

            #region 发送邮件限制检测
            string msg;
            bool checkResult = SendMailLogsPublic.CheckUserEmailAvailable(user.Id, UserConfig.SendEmailInterval,
                UserConfig.SendEmailIntervalMaxNum, UserConfig.SendEmailToDayMaxNum, out msg);
            if (!checkResult)
                return Json(new JsonResultModel { Message = msg });
            #endregion

            #region 写入令牌信息
            IToken token = new EmailActivateToken(user.Id, user.Email);
            string tokenCode = token.GetToken();
            var tokeninfo = new UserActivateTokenVModel()
            {
                UserId = user.Id,
                Email = user.Email,
                Token = tokenCode
            };
            long id = UserActivateTokensPublic.Add(tokeninfo);
            if (id <= 0)
                return Json(new JsonResultModel { Message = "写入令牌信息失败,请重试！" });
            #endregion

            #region 发送激活邮件
            string title = WebSysConfig.WebName + "找回密码邮件";
            var values = new Dictionary<string, object>
                {
                    {"$Title", title},
                    {"$Url", "http://"+Request.Url.Host+Url.Action("forgetpassword","user",new{token=tokenCode})},
                    {"$WebName", WebSysConfig.WebName},
                    {"$TimeDsc", WebSysConfig.RetrievePasswordTokenExpireTime+"分钟内"}
                };
            bool status = UserPublic.SendMail(user.Id, user.Id, Common.Fetch.Ip, user.Email, EmailTemplateConfig.RetrievePasswordEmailTemplate, values, "UTF-8", true);
            if (status)
                return Json(new JsonResultModel { ResultState = true, Message = "邮件发送成功，请进入邮箱进行操作！" });
            #endregion

            return Json(new JsonResultModel { Message = "邮件发送失败！" });
        }
        /// <summary>
        /// 找回密码-重置密码 （与登陆、未登录无关）
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [Description("找回密码-重置密码")]
        [HttpPost]
        public ActionResult ForgetSetPassword(UserVModel user,string token)
        {
            if (string.IsNullOrEmpty(token))
                return Json(new JsonResultModel { Message ="令牌校验错误，重置失败！" });
            ModelState.Remove("Name");
            ModelState.Remove("NickName");
            ModelState.Remove("Sex");
            ModelState.Remove("RoleId");
            ModelState.Remove("Avatar");
            ModelState.Remove("Email");
            if (!ModelState.IsValid)
                return Json(new JsonResultModel { Message = ModelStateHelper.GetAllErrorMessage(ModelState) });

            //通过token 得到id
            long tokenId = UserActivateTokensPublic.CheckToken(token, WebSysConfig.ActivateTokenExpireTime);
            if (tokenId <= 0)
                return Json(new JsonResultModel { Message = "令牌无效，重置失败，请重新获取找密码回邮件！" });
            //得到token 信息
            var tokeninfo = UserActivateTokensPublic.GetUserActivateTokenVModel(tokenId);
            if(tokeninfo==null || tokeninfo.IsNull)
                return Json(new JsonResultModel { Message = "令牌无效，重置失败，请重新获取找密码回邮件！" });
            //得到email,通过email修改密码
            bool status= UserPublic.UpdatePassword(tokeninfo.Email, user.Password);

            if (!status)
                return Json(new JsonResultModel { Message = "密码重置失败，稍后再试！" });
            //置令牌无效
            UserActivateTokensPublic.UpdateStatus(tokenId, false);
            return Json(new JsonResultModel { ResultState = true, Message = "密码重置成功,请使用新密码重新登陆！",Action =(UserIsLogin?"":Url.Action("Login","Vip"))});
        }

        #region 个人中心ajax数据接口
        /// <summary>
        /// 我的文章
        /// </summary>
        /// <returns></returns>
        [RoleAuthorize]
        [HttpPost]
        public async Task<JsonResult> MyArticles(int pageIndex)
        {
            int pageSize = PageConfig.UserCenterArticlePageSize;
            var articles = (await Articles.SearchArticlesByUserId(CurrentUserInfo.Id, -1, pageIndex, pageSize)).ToPagedList(pageIndex,pageSize);
            return Json(new JsonResultModel<List<ArticleVModel>> { ResultState = true, Body = articles });
        }
        /// <summary>
        /// 我的文章
        /// </summary>
        /// <returns></returns>
        [RoleAuthorize]
        [HttpPost]
        public async Task<JsonResult> MyArticlesCount()
        {
            int count = await Articles.SearchArticlesCountByUserId(CurrentUserInfo.Id, -1);
            decimal pages = Math.Ceiling((decimal)(count / PageConfig.UserCenterArticlePageSize));
            return Json(new JsonResultModel<decimal> { ResultState = true, Body = pages });
        }
        /// <summary>
        /// 我的文章
        /// </summary>
        /// <returns></returns>
        [RoleAuthorize]
        [HttpPost]
        public async Task<JsonResult> MyComments(int pageIndex)
        {
            int pageSize = PageConfig.UserCenterCommentPageSize;
            var articles = (await Comments.SearchCommentsByUserId(CurrentUserInfo.Id, -1, pageIndex, pageSize)).ToPagedList(pageIndex, pageSize);
            return Json(new JsonResultModel<List<CommentVModel>> { ResultState = true, Body = articles });
        }
        /// <summary>
        /// 我的文章
        /// </summary>
        /// <returns></returns>
        [RoleAuthorize]
        [HttpPost]
        public async Task<JsonResult> MyCommentsCount()
        {
            int count = await Comments.SearchCommentsCountByUserId(CurrentUserInfo.Id, -1);
            decimal pages = Math.Ceiling((decimal)(count / PageConfig.UserCenterCommentPageSize));
            return Json(new JsonResultModel<decimal> { ResultState = true, Body = pages });
        }
        #endregion
    }
}