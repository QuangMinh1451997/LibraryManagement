using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThuVien.Common
{
    [Serializable]
    public class UserLogin
    { 
        public string Username { get; set; }
        public string Password { get; set; }
        public bool QuanTri { get; set; }
        public bool ThuThu { get; set; }
        public UserLogin()
        {

        }
        public UserLogin(string username, string password, bool quanTri, bool thuThu)
        {
            Username = username;
            Password = password;
            QuanTri = quanTri;
            ThuThu = thuThu;
        }
    }
}