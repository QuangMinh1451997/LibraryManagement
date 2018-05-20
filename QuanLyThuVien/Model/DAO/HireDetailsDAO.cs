using Model.EF;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Model.DAO
{
    public class HireDetailsDAO
    {
        LibraryContext db = null;
        public HireDetailsDAO()
        {
            db = new LibraryContext();
        }

        public IEnumerable<HireDetail> GetAllHireDetails()
        {
            return db.HireDetails
                .Include(hr => hr.Book.BookType)
                .Include(hr => hr.Member)
                .Include(hr => hr.HireTime);
        }

        public HireDetail GetHireDetailByID(int id)
        {
            return db.HireDetails
                .Include(hr => hr.Book)
                .Include(hr => hr.Member)
                .Include(hr => hr.HireTime)
                .SingleOrDefault(hr => hr.HireDetailID == id && hr.Deleted == false);
        }

        public bool Insert(int bookID, int memberID, int hireTimeID, int employeeID, string username)
        {
            var newHireDetails = new HireDetail()
            {
                BookID= bookID,
                MemberID = memberID,
                HireTimeID = hireTimeID,
                EmployeeID = employeeID,
                Status = 1,
                Penalty = 0,
                HireDate = DateTime.Today
            };
            Helper.SetDefaultCreateModel(newHireDetails, username);
            db.HireDetails.Add(newHireDetails);
            db.Books.Find(bookID).Status = true;
            db.SaveChanges();
            return true;
        }

        public bool Pay(int id, string username)
        {
            var hireDetails = db.HireDetails
                .Include(hr => hr.Book.BookType)
                .Include(hr => hr.Member)
                .Include(hr => hr.HireTime)
                .SingleOrDefault(hr => hr.Deleted == false && hr.Status == 1 && hr.HireDetailID==id);
            if (hireDetails == null)
                return false;
            hireDetails.Status = 0;
            hireDetails.Book.Status = false;
            Helper.SetDefaultEditModel(hireDetails, username);
            db.SaveChanges();
            return true;
        }
    }
}
