using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.VWModel;

namespace HGShare.Web.Interface
{
    /// <summary>
    /// 用户公共处理 接口
    /// </summary>
    public interface IUsersPublic
    {
        /// <summary>
        /// 修改基本信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool UpdateUser(UserVModel user);
        /// <summary>
        /// 修改用户其它信息
        /// </summary>
        /// <param name="userother"></param>
        /// <returns></returns>
        bool UpdateUserOther(UserOtherVModel userother);
        /// <summary>
        /// 添加位置信息
        /// </summary>
        /// <param name="userPosition"></param>
        /// <returns></returns>
        Task<int> AddUserPosition(UserPositionVModel userPosition);
        /// <summary>
        /// 删除用户位置信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<int> DeleteUserPosition(int userid);

        /// <summary>
        /// 添加其它信息
        /// </summary>
        /// <param name="userOther"></param>
        /// <returns></returns>
        Task<int> AddUserOther(UserOtherVModel userOther);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        bool UpdatePassword(int userId,string passWord);
        /// <summary>
        /// 检测用户密码是否正确
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        bool CheckUserPassword(int userId, string passWord);
        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        bool UpdateAvatar(int userId, string avatar);
        /// <summary>
        /// 评论数+1/-1
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="num">+1/-1</param>
        /// <returns></returns>
        Task<bool> UpdateCommentNum(long aId, int num);
        /// <summary>
        /// 文章数+1/-1
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="num">+1/-1</param>
        /// <returns></returns>
        Task<bool> UpdateArticleNum(long aId, int num);

        #region 给用户发送邮件
        /// <summary>
        /// 同步发送邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sendUserId"></param>
        /// <param name="ip"></param>
        /// <param name="to">收件人邮箱地址</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isSystem"></param>
        /// <param name="encoding">编码</param>
        /// <param name="isBodyHtml">是否Html</param>
        /// <param name="enableSsl">是否SSL加密连接</param>
        /// <returns>是否成功</returns>
        bool SendMail(int userId, int sendUserId, string ip, string to, string subject, string body,
            bool isSystem = true, string encoding = "UTF-8", bool isBodyHtml = true,
            bool enableSsl = false);

        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sendUserId"></param>
        /// <param name="ip"></param>
        /// <param name="to">邮件接收人</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="isSystem"></param>
        /// <param name="encoding">编码</param>
        /// <param name="isBodyHtml">是否Html</param>
        /// <param name="enableSsl">是否SSL加密连接</param>
        /// <returns></returns>
        void SendMailAsync(int userId, int sendUserId, string ip, string to, string subject, string body,
            bool isSystem = true, string encoding = "UTF-8", bool isBodyHtml = true,
            bool enableSsl = false);
        /// <summary>
        /// 同步发送邮件
        /// </summary>
        /// <param name="userId">操作人id</param>
        /// <param name="sendUserId"></param>
        /// <param name="ip">操作人ip</param>
        /// <param name="to">收件人邮箱地址</param>
        /// <param name="values">模板占位符的值</param>
        /// <param name="encoding">编码</param>
        /// <param name="enableSsl">是否SSL加密连接</param>
        /// <param name="templateId">模板id</param>
        /// <returns>是否成功</returns>
        bool SendMail(int userId, int sendUserId, string ip, string to, int templateId,
            Dictionary<string, object> values, string encoding = "UTF-8", bool enableSsl = false);
        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="userId">操作人id</param>
        /// <param name="sendUserId"></param>
        /// <param name="ip">操作人ip</param>
        /// <param name="to">收件人邮箱地址</param>
        /// <param name="values">模板占位符的值</param>
        /// <param name="encoding">编码</param>
        /// <param name="enableSsl">是否SSL加密连接</param>
        /// <param name="templateId">模板id</param>
        /// <returns>是否成功</returns>
        void SendMailAsync(int userId, int sendUserId, string ip, string to, int templateId,
            Dictionary<string, object> values, string encoding = "UTF-8", bool enableSsl = false);

        #endregion

        /// <summary>
        /// 更新用户邮箱激活状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="emailStatus"></param>
        /// <returns></returns>
        bool UpdateEmailStatus(int id, bool emailStatus);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        bool UpdatePassword(string email, string passWord);
    }
}
