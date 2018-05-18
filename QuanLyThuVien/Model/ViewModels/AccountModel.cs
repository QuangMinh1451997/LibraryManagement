using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    public class AccountModel
    {
        public int EmployeeID { get; set; }

        public string UrlAvatar { get; set; }

        public string Username { get; set; }

        [Required]
        [MaxLength(8, ErrorMessage = "Mật khẩu phải ít hơn 8 ký tự")]
        [MinLength(4, ErrorMessage = "Mật khẩu phải ít nhất có 4 ký tự")]
        public string OldPassword { get; set; }

        [MaxLength(8,ErrorMessage = "Mật khẩu phải ít hơn 8 ký tự")]
        [MinLength(4, ErrorMessage = "Mật khẩu phải ít nhất có 4 ký tự")]
        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không trùng khớp")]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
