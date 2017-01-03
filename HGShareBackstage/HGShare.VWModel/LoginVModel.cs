using System.ComponentModel.DataAnnotations;

namespace HGShare.VWModel
{
    public class LoginVModel
    {
        [MaxLength(15,ErrorMessage = "用户名最大长度为15字符！")]
        [Required(ErrorMessage = "用户名不能为空")]
        [Display(Name = "用户名")]
        public string Username { get; set; }

        [MaxLength(15, ErrorMessage = "密码最大长度为15字符！")]
        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}