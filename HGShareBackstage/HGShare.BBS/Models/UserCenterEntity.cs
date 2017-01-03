using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HGShare.BBS.Models
{
    public class UserCenterEntity
    {
        public VWModel.UserVModel User { get; set; }

        public List<VWModel.UserPositionVModel> Positions { get; set; }
    }
}