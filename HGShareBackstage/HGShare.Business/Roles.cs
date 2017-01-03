using System;
using System.Collections.Generic;
using System.Linq;
using HGShare.Model;
using HGShare.VWModel;

namespace HGShare.Business
{
    /// <summary>
    /// Role 
    /// </summary>
    public class Roles
    {
        /// <summary>
        /// 默认角色
        /// </summary>
        //public static readonly RoleInfo DefaultRoleInfo=new RoleInfo
        //{
        //    Id = 0,
        //    RName = "普通用户",
        //    IsDel = false,
        //    IsSuper = false,
        //    Description = "普通用户",
        //    CreateTime = DateTime.Now
        //};

        /// <summary>
        /// 添加RoleInfo
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static int AddRole(RoleInfo role)
        {
            return DataProvider.Roles.AddRole(role);
        }
        /// <summary>
        /// 修改RoleInfo
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static int UpdateRole(RoleInfo role)
        {
            return DataProvider.Roles.UpdateRole(role);
        }
        /// <summary>
        /// 根据id获取RoleInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RoleInfo GetRoleInfo(int id)
        {

            return DataProvider.Roles.GetRoleInfo(id);
        }
        /// <summary>
        /// 根据id删除Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteRole(int id)
        {
            return DataProvider.Roles.DeleteRole(id);
        }
        /// <summary>
        /// 根据ids删除Role多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteRoles(int[] ids)
        {
            return DataProvider.Roles.DeleteRoles(ids);
        }
        /// <summary>
        /// 获取Role分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>Role列表</returns>
        public static List<RoleInfo> GetRolePageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            return DataProvider.Roles.GetRolePageList(pageIndex, pageSize, beginTime, endTime, out recordCount);
        }
        /// <summary>
        /// 获取Role分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>Role列表</returns>
        public static List<RoleInfo> GetRolePageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            return DataProvider.Roles.GetRolePageList(pageIndex, pageSize, beginTime, endTime, out pageCount, out count);
        }

        /// <summary>
        /// 获取角色集合
        /// </summary>
        /// <returns></returns>
        public static List<RoleInfo> GetAllRole()
        {
            var list = DataProvider.Roles.GetAllRole();
           
            return list;
        }
        #region 实体转换
        /// <summary>
        /// DataModel 转 ViewModel
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static RoleVModel RoleInfoToVModel(RoleInfo role)
        {
            if (role == null)
                return new RoleVModel();
            return new RoleVModel
            {
                Id = role.Id,
                RName = role.RName,
                CreateTime = role.CreateTime,
                IsDel = role.IsDel,
                Description = role.Description,
                IsSuper = role.IsSuper,
                ArticleNeedVerified = role.ArticleNeedVerified,
                CommentNeedVerified = role.CommentNeedVerified
            };
        }
        /// <summary>
        /// DataModels 转 ViewModels
        /// </summary>
        /// <param name="roleInfos"></param>
        /// <returns></returns>
        public static List<RoleVModel> RoleInfosToVModels(List<RoleInfo> roleInfos)
        {
            return roleInfos.Select(RoleInfoToVModel).ToList();
        }
        /// <summary>
        /// ViewModel 转 DataModel
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static RoleInfo RoleVModelToInfo(RoleVModel role)
        {
            if (role == null)
                return new RoleInfo();
            return new RoleInfo
            {
                Id = role.Id,
                RName = role.RName,
                CreateTime = role.CreateTime,
                IsDel = role.IsDel,
                Description = role.Description,
                IsSuper = role.IsSuper,
                ArticleNeedVerified = role.ArticleNeedVerified,
                CommentNeedVerified = role.CommentNeedVerified
            };
        }
        /// <summary>
        /// ViewModels 转 DataModels
        /// </summary>
        /// <param name="roleVModels"></param>
        /// <returns></returns>
        public static List<RoleInfo> RoleVModelsToInfos(List<RoleVModel> roleVModels)
        {
            return roleVModels.Select(RoleVModelToInfo).ToList();
        }
        #endregion

    }
}
