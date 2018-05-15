using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.Entity;
using Model.ViewModels;

namespace Model.DAO
{
    public class EmployeeDAO
    {
        LibraryContext db = null;
        public EmployeeDAO()
        {
            db = new LibraryContext();
        }
        
        //public int Insert(Employee newNhanVien)
        //{
        //    newNhanVien.Deleted = false;
        //    db.NhanViens.Add(newNhanVien);
        //    return db.SaveChanges();
        //}

        public Employee Edit(Employee employeeUpdate, EmployeeLogin user)
        {
            try
            {
                var employeeCurr = db.Employees.Find(employeeUpdate.EmployeeID);
                if (employeeCurr == null)
                    return null;

                employeeCurr.FirstName = employeeUpdate.FirstName;
                employeeCurr.LastName = employeeUpdate.LastName;
                employeeCurr.Phone = employeeUpdate.Phone;
                employeeCurr.BirthDay = employeeUpdate.BirthDay;
                employeeCurr.Address = employeeUpdate.Address;
                if(employeeUpdate.UrlAvatar!=null) employeeCurr.UrlAvatar = employeeUpdate.UrlAvatar;
                employeeCurr.EditBy = employeeUpdate.CreateBy;
                db.SaveChanges();
                return employeeCurr;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Employee> GetAllEmployees()
        {
            var query = db.Employees
                .Include(e => e.Account.Permission).OrderBy(em=>em.EmployeeID);
            return query;
        }

        public Employee GetEmployeeByID(int id)
        {
            return db.Employees.Find(id);
        }
    }
}
