using Model.EF;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
                            from em in db.Employees
                           from p in db.Permissions
                           where ac.Username.CompareTo(username) == 0 && ac.EmployeeID == em.EmployeeID && em.PermissionID == p.PermissionID && ac.Deleted==false
                            select new EmployeeLogin
                            {
                                EmployeeID = ac.EmployeeID,
                                Username = ac.Username,
                                PermissionID = p.PermissionID,
                                PermissionName = p.PermissionName,
                                QuanLy = p.QuanLy,
                                ThuThu = p.ThuThu
                            }).SingleOrDefault();
                return user;
            }
            return null;
        }

        public int ChangePassword(int id, string username, string oldPassword, string password)
        {
            var accountUpdate = db.Accounts.SingleOrDefault(ac => ac.EmployeeID == id && ac.Deleted == false);
            if (accountUpdate == null)
                return -1;
            if (HashMd5(oldPassword) != accountUpdate.Password.ToLower())
                return -2;

            accountUpdate.Password = HashMd5(password);

            Helper.SetDefaultEditModel(accountUpdate, username);

            db.SaveChanges();
            return 1;
        }

        public int RestorePassword(int id, string username)
        {
            var employee = db.Employees.SingleOrDefault(em => em.EmployeeID == id && em.Deleted == false);
            if (employee == null)
                return -1;
            var defaultPasswrod = HashMd5(CreatePasswordDefault(employee.BirthDay));
            employee.Account.Password = defaultPasswrod;
            Helper.SetDefaultEditModel(employee.Account, username);
            return db.SaveChanges();
        }

        private string HashMd5(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sHash = new StringBuilder();
            Array.ForEach(bHash, b => sHash.Append(String.Format("{0:x2}", b)));
            return sHash.ToString();
        }

        private string CreatePasswordDefault(DateTime date)
        {
            string strDate = date.ToString("dd/MM/yyyy");

            return  strDate.Replace("/", "");
        }
    }
}
