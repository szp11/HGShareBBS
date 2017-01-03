using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{
    /// <summary>
    /// UserOther View Model
    /// </summary>
    public class UserOtherVModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [Display(Name = "用户编号")]
        public int UserId { get; set; }
        /// <summary>
        /// 个性介绍
        /// </summary>
        [MaxLength(500, ErrorMessage = "个性介绍最大长度为500字符！")]
        [Display(Name = "个性介绍")]
        public string PersonalityIntroduce { get; set; }


        public bool IsNull {
            get { return UserId <= 0; }
        }
    }
}
