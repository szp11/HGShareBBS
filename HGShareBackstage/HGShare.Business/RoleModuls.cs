using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.Model;

namespace HGShare.Business
{
    public class RoleModuls
    {
        /// <summary>
        /// 添加RoleModulInfo
        /// </summary>
        /// <param name="rolemodul"></param>
        /// <returns></returns>
        public static int AddRoleModul(RoleModulInfo rolemodul)
        {
            return DataProvider.RoleModuls.AddRoleModul(rolemodul);
        }

        /// <summary>
        /// 添加RoleModulInfo
        /// </summary>
        /// <param name="rolemoduls"></param>
        /// <returns></returns>
        public static int AddRoleModuls(List<RoleModulInfo> rolemoduls)
        {
            int count = 0;
            rolemoduls.ForEach(n => { count += AddRoleModul(n); });
            return count;
        }

        /// <summary>
        /// 根据rid删除RoleModul多条记录
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public static Int32 DeleteRoleModuls(int rid)
        {
            return DataProvider.RoleModuls.DeleteRoleModuls(rid);
        }
        /// <summary>
        /// 根据角色id获取已有模块id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetMIds(int id)
        {
            return DataProvider.RoleModuls.GetMIds(id);
        }
    }
}
