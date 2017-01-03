using System;
using System.Collections.Generic;
using System.Linq;
using HGShare.Model;
using HGShare.VWModel;

namespace HGShare.Business
{
    public static  class Moduls
    {
        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        public static List<ModulInfo> GetAllModul()
        {
            return DataProvider.Moduls.GetAllModul();
        }
        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <param name="exclusiveid">要排除的id</param>
        /// <returns></returns>
        public static List<ModulInfo> GetAllModul(int? exclusiveid)
        {
            var moduls = GetAllModul();
            if(moduls==null)return new List<ModulInfo>();
            if (exclusiveid.HasValue)
                return moduls.Where(n => n.Id != exclusiveid).ToList();
            return moduls;
        }
        /// <summary>
        /// 获取所有启用并显示的模块
        /// </summary>
        /// <returns></returns>
        public static List<ModulInfo> GetIsShowDisplayList()
        {
            var moduls = GetAllModul();
            if (moduls!=null)
                return moduls.Where(n => n.IsShow && n.IsDisplay).ToList();
            return null;
        }
        /// <summary>
        /// 根据角色id获取所有启用的模块
        /// </summary>
        /// <returns></returns>
        public static List<ModulInfo> GetIsShowListByRoleId(int roleId)
        {
            return DataProvider.Moduls.GetModulsByRoleId(roleId);
        }
        /// <summary>
        /// 根据角色id获取所有启用并显示的模块
        /// </summary>
        /// <returns></returns>
        public static List<ModulInfo> GetIsShowDisplayListByRoleId(int roleId)
        {
            var modils = DataProvider.Moduls.GetModulsByRoleId(roleId);
            if (modils == null)
                return null;
            return modils.Where(n=>n.IsDisplay).ToList();
        }
        /// <summary>
        /// 添加ModulInfo
        /// </summary>
        /// <param name="modul"></param>
        /// <returns></returns>
        public static int AddModul(ModulInfo modul)
        {
            return DataProvider.Moduls.AddModul(modul);
        }
        /// <summary>
        /// 修改ModulInfo
        /// </summary>
        /// <param name="modul"></param>
        /// <returns></returns>
        public static int UpdateModul(ModulInfo modul)
        {
            return DataProvider.Moduls.UpdateModul(modul);
        }
        /// <summary>
        /// 根据id获取ModulInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ModulInfo GetModulInfo(int id)
        {
            return DataProvider.Moduls.GetModulInfo(id);
        }
        /// <summary>
        /// 根据id删除Modul
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteModul(int id)
        {
            return DataProvider.Moduls.DeleteModul(id);
        }
        /// <summary>
        /// 根据ids删除Modul多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteModuls(int[] ids)
        {
            return DataProvider.Moduls.DeleteModuls(ids);
        }
        /// <summary>
        /// 获取Modul分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>Modul列表</returns>
        public static List<ModulInfo> GetModulPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            return DataProvider.Moduls.GetModulPageList(pageIndex, pageSize, beginTime, endTime, out recordCount);
        }
        /// <summary>
        /// 获取Modul分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>Modul列表</returns>
        public static List<ModulInfo> GetModulPageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            return DataProvider.Moduls.GetModulPageList(pageIndex, pageSize, beginTime, endTime, out pageCount, out count);
        }
        #region 实体转换
        /// <summary>
        /// DataModel 转 ViewModel
        /// </summary>
        /// <param name="modulInfo"></param>
        /// <returns></returns>
        public static ModulVModel ModulInfoToVModel(ModulInfo modulInfo)
        {
            if (modulInfo == null)
                return new ModulVModel();
            return new ModulVModel
            {
                Id = modulInfo.Id,
                PName = modulInfo.PId == 0 ? "顶级模块" : "",
                ModulName = modulInfo.ModulName,
                Controller = modulInfo.Controller,
                Action = modulInfo.Action,
                Description = modulInfo.Description,
                CreateTime = modulInfo.CreateTime,
                PId = modulInfo.PId,
                OrderId = modulInfo.OrderId,
                IsShow = modulInfo.IsShow,
                Priority = modulInfo.Priority,
                IsDisplay = modulInfo.IsDisplay,
                Ico = modulInfo.Ico
            };
        }
        /// <summary>
        /// DataModels 转 ViewModels
        /// </summary>
        /// <param name="modulInfos"></param>
        /// <returns></returns>
        public static List<ModulVModel> ModulInfosToVModels(List<ModulInfo> modulInfos)
        {
            return modulInfos.Select(ModulInfoToVModel).ToList();
        }
        /// <summary>
        /// ViewModel 转 DataModel
        /// </summary>
        /// <param name="modulVModel"></param>
        /// <returns></returns>
        public static ModulInfo ModulVModelToInfo(ModulVModel modulVModel)
        {
            if (modulVModel == null)
                return new ModulInfo();
            return new ModulInfo
            {
                Id = modulVModel.Id,
                ModulName = modulVModel.ModulName,
                Controller = modulVModel.Controller,
                Action = modulVModel.Action,
                Description = modulVModel.Description,
                CreateTime = modulVModel.CreateTime,
                PId = modulVModel.PId,
                OrderId = modulVModel.OrderId,
                IsShow = modulVModel.IsShow,
                Priority = modulVModel.Priority,
                IsDisplay = modulVModel.IsDisplay,
                Ico = modulVModel.Ico
            };
        }
        /// <summary>
        /// ViewModels 转 DataModels
        /// </summary>
        /// <param name="modulVModels"></param>
        /// <returns></returns>
        public static List<ModulInfo> ModulVModelsToInfos(List<ModulVModel> modulVModels)
        {
            return modulVModels.Select(ModulVModelToInfo).ToList();
        }
        /// <summary>
        /// ModulInfos 转 TreeVModels
        /// </summary>
        /// <param name="modulInfos"></param>
        /// <returns></returns>
        public static List<TreeVModel> ModulInfosToTreeVModels(List<ModulInfo> modulInfos)
        {
            var result=new List<TreeVModel>
            {
                new TreeVModel
                {
                    text = "顶级模块",
                    dataid = 0,
                    pid =-1,
                    state=new TreeState {opened = true},
                    children=new List<TreeVModel>(),
                    PIds=new []{-1},
                    Order=0
                }
            };
            if (modulInfos == null || modulInfos.Count == 0)
                return result;
            var parents = modulInfos.Where(n => n.PId == 0);
            List<TreeVModel> trees = parents.Select(n => new TreeVModel
            {
                dataid = n.Id,
                pid = n.PId,
                text = n.ModulName,
                icon = n.Ico,
                state = new TreeState { opened = false},
                children = new List<TreeVModel>(),
                PIds=new []{-1,0},
                Order=n.OrderId
            }).ToList();
            foreach (var node in trees)
            {
               node.children=GetChildrens(node, modulInfos);
            }
            result[0].children = trees.OrderByDescending(n=>n.Order).ToList();
            return result;
        }
        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="modulInfos"></param>
        /// <returns></returns>
        private static List<TreeVModel> GetChildrens(TreeVModel tree, List<ModulInfo> modulInfos)
        {
            var pids = new List<int>();
            pids.AddRange(tree.PIds);
            pids.Add(tree.dataid);
            List<TreeVModel> result = modulInfos.Where(n => n.PId == tree.dataid).Select(n => new TreeVModel
            {
                dataid = n.Id,
                pid = n.PId,
                text = n.ModulName,
                icon = n.Ico,
                state = new TreeState { opened = false },
                children = new List<TreeVModel>(),
                PIds = pids.ToArray(),
                Order = n.OrderId
            }).ToList();
            foreach (var item in result)
            {
                if (modulInfos.Any(n => n.PId == item.dataid))
                {
                    item.children = GetChildrens(item, modulInfos);
                }
            }
            return result.OrderByDescending(n => n.Order).ToList();
        }
        #endregion

        #region tree数据操作 已废弃，树的操作要用树自身api在前端操作
        /// <summary>
        /// 默认选中传入的id的节点(选中并打开)
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public static List<TreeVModel> SelectTreeNode(List<TreeVModel> tree, int? dataId)
        {
            if (!dataId.HasValue)
                return tree;
            int pid = -1;
            if (dataId.Value == 0)
            {
                var node = GetModulInfo(dataId.Value);
                if (node != null)
                    pid = node.PId;
            }
            //选中
            TreeSelectNode(tree, new[] { dataId.Value });
            if (pid > -1)//打开所有父级
            {
                var node = TreeNode(tree, dataId.Value);
                if (node != null)
                    TreeOpenNode(tree, node.PIds);
            }
            return tree;
        }
        /// <summary>
        /// 默认选中传入的id的节点(选中不打开)
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="dataIds"></param>
        /// <returns></returns>
        public static List<TreeVModel> SelectTreeNode(List<TreeVModel> tree, int[] dataIds)
        {
            if (dataIds.Length==0)
                return tree;
            //选中
            TreeSelectNode(tree, dataIds);
            return tree;
        }
        
        /// <summary>
        /// 递归找到节点
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="dataId"></param>
        private static TreeVModel TreeNode(IEnumerable<TreeVModel> tree, int dataId)
        {
            foreach (var item in tree)
            {
                if (item.dataid == dataId)
                {
                    return item;
                }
                if (item.children != null)
                    return TreeNode(item.children, dataId);
            }
            return null;
        }
        /// <summary>
        /// 递归找到节点并选中
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="dataIds"></param>
        private static void TreeSelectNode(IEnumerable<TreeVModel> tree, int[] dataIds)
        {
            foreach (var item in tree)
            {
                if (dataIds.Any(n => n == item.dataid))
                    item.state.selected = true;
                if (item.children != null)
                    TreeSelectNode(item.children, dataIds);
            }
        }
        /// <summary>
        /// 递归找到节点并打开
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="dataIds"></param>
        private static void TreeOpenNode(IEnumerable<TreeVModel> tree, int[] dataIds)
        {
            foreach (var item in tree)
            {
                if (dataIds.Any(n => n == item.dataid))
                    item.state.opened = true;
                if (item.children != null)
                    TreeOpenNode(item.children, dataIds);
            }
        }

        #endregion

        /// <summary>
        /// 根据父级生成排序值（排序值只会越来越大，越大的越靠前）
        /// </summary>
        /// <param name="pid">父级id</param>
        /// <param name="id">编辑时可排除自身所占位置</param>
        /// <returns></returns>
        public static int GetOrderNumber(int pid, int? id)
        {
            return DataProvider.Moduls.GetOrderNumber(pid, id);
        }
    }
}
