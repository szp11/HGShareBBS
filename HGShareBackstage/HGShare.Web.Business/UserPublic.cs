using System.Collections.Generic;
using System.Threading.Tasks;
using HGShare.Business;
using HGShare.Utils.Interface;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.Web.Business
{
    /// <summary>
    /// 用户公共处理
    /// </summary>
    public class UserPublic:IUsersPublic
    {
        private static IMail _mail;
        private static IEmailTemplates _emailTemplates;
        private static ISendMailLogsPublic _sendMailLogsPublic;
        public UserPublic(IMail mail, IEmailTemplates emailTemplates, ISendMailLogsPublic sendMailLogsPublic)
        {
            _mail = mail;
            _emailTemplates = emailTemplates;
            _sendMailLogsPublic = sendMailLogsPublic;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(UserVModel user)
        {
            return Users.UpdateUser(user);
        }
        /// <summary>
        /// 修改用户其它信息
        /// </summary>
        /// <param name="userother"></param>
        /// <returns></returns>
        public bool UpdateUserOther(UserOtherVModel userother)
        {
            return
                UserOthers.UpdateUserOther(UserOthers.UserOtherVModelToInfo(userother))>0;
        }
        /// <summary>
        /// 添加用户位置信息
        /// </summary>
        /// <param name="userPosition"></param>
        /// <returns></returns>
        public async Task<int> AddUserPosition(UserPositionVModel userPosition)
        {
            return await Task.Run(() =>  UserPositions.AddUserPosition(UserPositions.UserPositionVModelToInfo(userPosition)));
        }
        /// <summary>
        /// 删除用户位置信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserPosition(int userid)
        {
            return await Task.Run(() => UserPositions.DeleteUserPosition(userid));
        }
        /// <summary>
        /// 添加用户其它信息
        /// </summary>
        /// <param name="userOther"></param>
        /// <returns></returns>
        public async Task<int> AddUserOther(UserOtherVModel userOther)
        {
            return
                await
                    Task.Run(
                        () =>
                            UserOthers.AddUserOther(
                                UserOthers.UserOtherVModelToInfo(userOther)));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public bool UpdatePassword(int userId, string passWord)
        {
            return Users.ModifyPassWord(userId, passWord);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public bool CheckUserPassword(int userId, string passWord)
        {
            return Users.CheckPassword(userId, passWord);
        }
        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        public bool UpdateAvatar(int userId, string avatar)
        {
          return  Users.UpdateAvatar(userId, avatar)>0;
        }
        /// <summary>
        /// 更新用户评论数
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="num">+1/-1</param>
        /// <returns></returns>
        public async Task<bool> UpdateCommentNum(long aId, int num)
        {
            return await Task.Run(() => Users.UpdateCommentNum((int) aId, num))>0;
        }
        /// <summary>
        /// 更新用户文章数
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="num">+1/-1</param>
        /// <returns></returns>
        public async Task<bool> UpdateArticleNum(long aId, int num)
        {
            return await Task.Run(() => Users.UpdateArticleNum((int)aId, num)) > 0;
        }

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
        public bool SendMail(int userId, int sendUserId, string ip, string to, string subject, string body,bool isSystem=true, string encoding = "UTF-8", bool isBodyHtml = true,
            bool enableSsl = false)
        {
            bool status= _mail.Send(to, subject, body, encoding, isBodyHtml, enableSsl);
            _sendMailLogsPublic.Add(new SendMailLogVModel
            {
                UserId=userId,
                Body = body,
                FromEmail = _mail.GetFromEmail(),
                TemplateId = 0,
                Ip=ip,
                Title = subject,
                IsSystem = isSystem,
                SendUserId = sendUserId,
                ToEmail=to,
                Status = (short)(status?1:2)
            });
            return status;
        }

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
        public void SendMailAsync(int userId, int sendUserId, string ip, string to, string subject, string body, bool isSystem = true, string encoding = "UTF-8", bool isBodyHtml = true,
            bool enableSsl = false)
        {
            _mail.SendAsync(to, subject, body, encoding, isBodyHtml, enableSsl);
            _sendMailLogsPublic.Add(new SendMailLogVModel
            {
                UserId = userId,
                Body = body,
                FromEmail = _mail.GetFromEmail(),
                TemplateId = 0,
                Ip = ip,
                Title = subject,
                IsSystem = isSystem,
                SendUserId = sendUserId,
                ToEmail = to,
                Status =0
            });
        }

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
        public bool SendMail(int userId, int sendUserId, string ip, string to, int templateId, Dictionary<string, object> values, string encoding = "UTF-8", bool enableSsl = false)
        {
            var emalitemplate = _emailTemplates.GeEmailTemplate(templateId);
            if (emalitemplate == null || string.IsNullOrEmpty(emalitemplate.Template) || string.IsNullOrEmpty(emalitemplate.Title))
                return false;
            //替换值
            if (values != null && values.Count > 0)
            {
                foreach (var value in values)
                {
                    emalitemplate.Template = emalitemplate.Template.Replace(value.Key, value.Value.ToString());
                    emalitemplate.Title = emalitemplate.Title.Replace(value.Key, value.Value.ToString());
                }
            }
            bool status = _mail.Send(to, emalitemplate.Title, emalitemplate.Template, encoding, emalitemplate.IsHtml, enableSsl);
            _sendMailLogsPublic.Add(new SendMailLogVModel
            {
                UserId = userId,
                Body = emalitemplate.Template,
                FromEmail = _mail.GetFromEmail(),
                TemplateId = templateId,
                Ip = ip,
                Title = emalitemplate.Title,
                IsSystem = emalitemplate.IsSystem,
                SendUserId = sendUserId,
                ToEmail = to,
                Status = (short)(status?1:2)
            });
            return status;
        }

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
        public void SendMailAsync(int userId, int sendUserId, string ip, string to, int templateId, Dictionary<string, object> values, string encoding = "UTF-8", bool enableSsl = false)
        {
            var emalitemplate = _emailTemplates.GeEmailTemplate(templateId);
            if (emalitemplate == null || string.IsNullOrEmpty(emalitemplate.Template) || string.IsNullOrEmpty(emalitemplate.Title))
                return ;
            //替换值
            if (values != null && values.Count > 0)
            {
                foreach (var value in values)
                {
                    emalitemplate.Template = emalitemplate.Template.Replace(value.Key, value.Value.ToString());
                    emalitemplate.Title = emalitemplate.Title.Replace(value.Key, value.Value.ToString());
                }
            }
            _mail.SendAsync(to, emalitemplate.Title, emalitemplate.Template, encoding, emalitemplate.IsHtml, enableSsl);
            _sendMailLogsPublic.Add(new SendMailLogVModel
            {
                UserId = userId,
                Body = emalitemplate.Template,
                FromEmail = _mail.GetFromEmail(),
                TemplateId = templateId,
                Ip = ip,
                Title = emalitemplate.Title,
                IsSystem = emalitemplate.IsSystem,
                SendUserId = sendUserId,
                ToEmail = to,
                Status = 0
            });
        }
        /// <summary>
        /// 更新用户邮箱激活状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="emailStatus"></param>
        /// <returns></returns>
        public bool UpdateEmailStatus(int id, bool emailStatus)
        {
            return Users.UpdateEmailStatus(id, emailStatus);
        }
        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public bool UpdatePassword(string email, string passWord)
        {
            return Users.ModifyPassWordByEmail(email, passWord);
        }
    }
}
