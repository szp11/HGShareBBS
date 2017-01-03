using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HGShare.BBS.Models
{
    public class UserEntity
    {
        public VWModel.UserVModel User { get; set; }

        public List<VWModel.UserPositionVModel> UserPositions { get; set; }

        public VWModel.UserOtherVModel UserOther { get; set; }

    }
}