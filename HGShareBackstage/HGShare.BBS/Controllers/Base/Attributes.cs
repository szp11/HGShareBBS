using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HGShare.BBS.Controllers.Base
{
    #region 特性
    /// <summary>
    /// 需要校验权限（目前是只需要登陆）
    /// </summary>
    public class RoleAuthorize : Attribute
    {
        public string Name { get; set; }
    }
    /// <summary>
    /// 系统自动请求
    /// </summary>
    public class AutoRequest : Attribute
    {
        public string Name { get; set; }
    }
    /// <summary>
    /// 禁用验证
    /// </summary>
    public class UserDisableVerification : Attribute
    {
    }
    /// <summary>
    /// 邮箱激活验证
    /// </summary>
    public class EmailActivatedVerification : Attribute
    {
    }
    #endregion
}