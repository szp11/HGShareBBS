using System;
using System.Web.Mvc;

namespace HGShare.BBS.Controllers.Base
{
    /// <summary>
    /// 请求需要验证的判断信息
    /// </summary>
    public class RequestRoleAuthorize
    {
        #region 需要的验证判断
        /// <summary>
        /// 是否需要校验权限(目前是只需要登陆)
        /// </summary>
        public bool IsRoleAuthorize { get; private set; }
        /// <summary>
        /// 需要禁用验证
        /// </summary>
        public bool IsDisableVerification { get; private set; }
        /// <summary>
        /// 需要邮箱激活验证
        /// </summary>
        public bool IsEmailActivatedVerification { get; private set; }
        /// <summary>
        /// 是否是系统自动请求
        /// </summary>
        public bool IsAutoRequest { get; private set; }
        #endregion
        /// <summary>
        /// 请求需要验证的判断信息
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="actionDescriptor"></param>
        public RequestRoleAuthorize(ControllerBase controller, ActionDescriptor actionDescriptor)
        {
            IsRoleAuthorize= HaveAttribute<RoleAuthorize>(controller, actionDescriptor); 
            IsDisableVerification = HaveAttribute<UserDisableVerification>(controller, actionDescriptor);
            IsEmailActivatedVerification = HaveAttribute<EmailActivatedVerification>(controller, actionDescriptor);
            IsAutoRequest = HaveAttribute<AutoRequest>(controller, actionDescriptor);
        }
        /// <summary>
        /// 含有验证
        /// </summary>
        public bool HaveVerification
        {
            get { return (IsRoleAuthorize || IsDisableVerification || IsEmailActivatedVerification); }
        }

        #region 特性检测
        /// <summary>
        /// 得到控制器自定义特性
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        private static Attribute GetAttribute<T>(ControllerBase controller)
        {
            return Attribute.GetCustomAttribute(controller.GetType(), typeof(T));
        }

        /// <summary>
        /// 得到ActionDescriptor自定义特性
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        private static object[] GetAttribute<T>(ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetCustomAttributes(typeof(T), false);
        }
        /// <summary>
        /// 检测控制器 和action中是否包含特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static bool HaveAttribute<T>(ControllerBase controller, ActionDescriptor actionDescriptor)
        {
            var noAuthorizeAttributesC = GetAttribute<T>(controller);
            if (noAuthorizeAttributesC != null)
                return true;
            var noAuthorizeAttributes = GetAttribute<T>(actionDescriptor);
            if (noAuthorizeAttributes.Length > 0)
                return true;
            return false;
        }
        #endregion
    }
}