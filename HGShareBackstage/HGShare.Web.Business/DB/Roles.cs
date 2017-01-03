using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.Web.Business.DB
{
    public class Roles:IRoles
    {
        public RoleVModel GetRole(int id)
        {
            return HGShare.Business.Roles.RoleInfoToVModel(HGShare.Business.Roles.GetRoleInfo(id));
        }
    }
}
