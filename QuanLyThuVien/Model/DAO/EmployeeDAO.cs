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

        public int Insert(Employee newEmployee, string username)
        {
            Helper.SetDefaultCreateModel(newEmployee, username);
            db.Employees.Add(newEmployee);
            return db.SaveChanges();
        }

        public Employee EditByUser(Employee employeeUpdate, string username)
        {
            try
            {
                var employeeCurr = db.Employees.SingleOrDefault(em => em.EmployeeID == employeeUpdate.EmployeeID && em.Deleted == false);
                if (employeeCurr == null)
                    return null;

                employeeCurr.FirstName = employeeUpdate.FirstName;
                employeeCurr.LastName = employeeUpdate.LastName;
                employeeCurr.Phone = employeeUpdate.Phone;
                employeeCurr.BirthDay = employeeUpdate.BirthDay;
                employeeCurr.Address = employeeUpdate.Address;
                if (employeeUpdate.UrlAvatar != null) employeeCurr.UrlAvatar = employeeUpdate.UrlAvatar;

                Helper.SetDefaultEditModel(employeeCurr, username);

                db.SaveChanges();
                return employeeCurr;
            }
            catch
            {
                return null;
            }
        }

        public Employee EditByQuanLy(Employee employeeUpdate, string username)
        {

            var employeeCurr = db.Employees.SingleOrDefault(em => em.EmployeeID == employeeUpdate.EmployeeID && em.Deleted == false);
            if (employeeCurr == null)
                return null;

            employeeCurr.FirstName = employeeUpdate.FirstName;
            employeeCurr.LastName = employeeUpdate.LastName;
            employeeCurr.Phone = employeeUpdate.Phone;
            employeeCurr.BirthDay = employeeUpdate.BirthDay;
            employeeCurr.Address = employeeUpdate.Address;
            employeeCurr.Sex = employeeUpdate.Sex;
            employeeCurr.PermissionID = employeeUpdate.PermissionID;

            if (employeeUpdate.UrlAvatar != null) employeeCurr.UrlAvatar = employeeUpdate.UrlAvatar;
            Helper.SetDefaultEditModel(employeeCurr, username);
            db.SaveChanges();
            return employeeCurr;

        }

        public int Delete(int id)
        {
            var employeeDelete = db.Employees.SingleOrDefault(em=>em.EmployeeID==id && em.Deleted == false);
            if (employeeDelete == null)
                return -1;

            employeeDelete.Deleted = true;
            employeeDelete.Account.Deleted = true;

            return db.SaveChanges();
        }

        public IQueryable<Employee> GetAllEmployees()
        {
            var query = db.Employees
                .Include(e => e.Permission).Where(e=>e.Deleted==false).OrderBy(em => em.EmployeeID);
            return query;
        }

        public Employee GetEmployeeByID(int id)
        {
            return db.Employees.SingleOrDefault(em => em.EmployeeID == id && em.Deleted == false);
        }

        public Employee GetEmployeeByUsername(string username)
        {
            return db.Employees.SingleOrDefault(em => em.Account.Username == username && em.Deleted == false);
        }

        public int GetNextID()
        {
            return Helper.GetNextID(db, "Employees");
            //return db.Database.SqlQuery<int>("SELECT IDENT_CURRENT('Employees')").SingleOrDefault();
        }
    }
}
