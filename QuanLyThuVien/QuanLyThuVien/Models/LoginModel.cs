using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyThuVien.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Nhập tên tài khoản")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu")]
        public string Password { get; set; }

        public LoginModel()
        {

        }
        public LoginModel(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}