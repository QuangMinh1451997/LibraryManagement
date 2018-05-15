using Model.EF;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class AccountDAO
    {
        LibraryContext db = null;
        
        public AccountDAO()
        {
            db = new LibraryContext();
        }

        public EmployeeLogin GetUserLogin(string username, string password)
        {
            var aParam = new SqlParameter[]
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };
            var result = db.Database.SqlQuery<int>("sp_check_login @username, @password", aParam).SingleOrDefault();
            if(result == 1)
            {
                var user = (from ac in db.Accounts
                           from p in db.Permissions
                           where ac.Username.CompareTo(username) == 0 && ac.PermissionID == p.PermissionID
                            select new EmployeeLogin
                            {
                                EmployeeID = ac.EmployeeID,
                                Username = ac.Username,
                                PermissionName = p.PermissionName,
                                QuanLy = p.QuanLy,
                                ThuThu = p.ThuThu
                            }).SingleOrDefault();
                return user;
            }
            return null;
        }
    }
}
