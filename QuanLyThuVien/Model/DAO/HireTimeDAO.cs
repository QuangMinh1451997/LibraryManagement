using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class HireTimeDAO
    {
        LibraryContext db = null;
        public HireTimeDAO()
        {
            db = new LibraryContext();
        }

        public IEnumerable<HireTime> GetAllHireTime()
        {
            return db.HireTimes;
        }

        public IEnumerable<HireTime> GetAllHireTimeActive()
        {
            return db.HireTimes.Where(ht=>ht.Status==false);
        }

        public bool Insert(HireTime newHireTime, string username)
        {
            Helper.SetDefaultCreateModel(newHireTime, username);
            db.HireTimes.Add(newHireTime);
            db.SaveChanges();
            return true;
        }

        public int Update(int id, string username)
        {
            
            var hireTime = db.HireTimes.Find(id);
            if (hireTime == null)
                return -1;
            hireTime.Status = !hireTime.Status;
            Helper.SetDefaultEditModel(hireTime, username);
            db.SaveChanges();
            return (hireTime.Status == true) ? 1 : 2;
        }
    }
}
