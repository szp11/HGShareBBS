using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{
    /// <summary>
    /// EmailTemplate 
    /// </summary>
    public class EmailTemplates
    {

        /// <summary>
        /// 添加EmailTemplateInfo
        /// </summary>
        /// <param name="emailtemplate"></param>
        /// <returns></returns>
        public static int AddEmailTemplate(EmailTemplateInfo emailtemplate)
        {
            return DataProvider.EmailTemplates.AddEmailTemplate(emailtemplate);
        }
        /// <summary>
        /// 修改EmailTemplateInfo
        /// </summary>
        /// <param name="emailtemplate"></param>
        /// <returns></returns>
        public static int UpdateEmailTemplate(EmailTemplateInfo emailtemplate)
        {
            return DataProvider.EmailTemplates.UpdateEmailTemplate(emailtemplate);
        }
        /// <summary>
        /// 根据id获取EmailTemplateInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static EmailTemplateInfo GetEmailTemplateInfo(int id)
        {
            return DataProvider.EmailTemplates.GetEmailTemplateInfo(id);
        }
        /// <summary>
        /// 根据id删除EmailTemplate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteEmailTemplate(int id)
        {
            return DataProvider.EmailTemplates.DeleteEmailTemplate(id);
        }
        /// <summary>
        /// 根据ids删除EmailTemplate多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteEmailTemplates(int[] ids)
        {
            return DataProvider.EmailTemplates.DeleteEmailTemplates(ids);
        }
        /// <summary>
        /// 获取EmailTemplate分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>EmailTemplate列表</returns>
        public static List<EmailTemplateInfo> GetEmailTemplatePageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            return DataProvider.EmailTemplates.GetEmailTemplatePageList(pageIndex, pageSize, beginTime, endTime, out recordCount);
        }
        /// <summary>
        /// 获取EmailTemplate分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>EmailTemplate列表</returns>
        public static List<EmailTemplateInfo> GetEmailTemplatePageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            return DataProvider.EmailTemplates.GetEmailTemplatePageList(pageIndex, pageSize, beginTime, endTime, out pageCount, out count);
        }
        #region 实体转换
        /// <summary>
        /// DataModel 转 ViewModel
        /// </summary>
        /// <param name="emailtemplate"></param>
        /// <returns></returns>
        public static EmailTemplateVModel EmailTemplateInfoToVModel(EmailTemplateInfo emailtemplate)
        {
            if (emailtemplate == null)
                return new EmailTemplateVModel();
            return new EmailTemplateVModel
            {
                Id = emailtemplate.Id,
                Title = emailtemplate.Title,
                Template = emailtemplate.Template,
                ValueIdentifier = emailtemplate.ValueIdentifier,
                Explanation = emailtemplate.Explanation,
                IsSystem = emailtemplate.IsSystem,
                IsHtml = emailtemplate.IsHtml,
                CreateTime = emailtemplate.CreateTime,
                UserId = emailtemplate.UserId,
                LastEditUserId = emailtemplate.LastEditUserId,
                LastEditTime = emailtemplate.LastEditTime
            };
        }
        /// <summary>
        /// DataModels 转 ViewModels
        /// </summary>
        /// <param name="emailtemplateInfos"></param>
        /// <returns></returns>
        public static List<EmailTemplateVModel> EmailTemplateInfosToVModels(List<EmailTemplateInfo> emailtemplateInfos)
        {
            return emailtemplateInfos.Select(EmailTemplateInfoToVModel).ToList();
        }
        /// <summary>
        /// ViewModel 转 DataModel
        /// </summary>
        /// <param name="emailtemplate"></param>
        /// <returns></returns>
        public static EmailTemplateInfo EmailTemplateVModelToInfo(EmailTemplateVModel emailtemplate)
        {
            if (emailtemplate == null)
                return new EmailTemplateInfo();
            return new EmailTemplateInfo
            {
                Id = emailtemplate.Id,
                Title = emailtemplate.Title,
                Template = emailtemplate.Template,
                ValueIdentifier = emailtemplate.ValueIdentifier,
                Explanation = emailtemplate.Explanation,
                IsSystem = emailtemplate.IsSystem,
                IsHtml = emailtemplate.IsHtml,
                CreateTime = emailtemplate.CreateTime,
                UserId = emailtemplate.UserId,
                LastEditUserId = emailtemplate.LastEditUserId,
                LastEditTime = emailtemplate.LastEditTime
            };
        }
        /// <summary>
        /// ViewModels 转 DataModels
        /// </summary>
        /// <param name="emailtemplateVModels"></param>
        /// <returns></returns>
        public static List<EmailTemplateInfo> EmailTemplateVModelsToInfos(List<EmailTemplateVModel> emailtemplateVModels)
        {
            return emailtemplateVModels.Select(EmailTemplateVModelToInfo).ToList();
        }
        #endregion
    }
}
