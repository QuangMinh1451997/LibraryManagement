using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class SpecializedDAO
    {
        LibraryContext db = null;
        public SpecializedDAO()
        {
            db = new LibraryContext();
        }

        public IQueryable<Specialized> GetAllSpecialized()
        {
            var queryList = db.Specializeds.Where(sp => sp.Deleted == false);
            return queryList;
        }

        public bool Insert(Specialized newSpecialized, string username)
        {
            Helper.SetDefaultCreateModel(newSpecialized, username);
            db.Specializeds.Add(newSpecialized);
            db.SaveChanges();
            return true;
        }
        
        public Specialized GetSpecializedByID(int id)
        {
            return db.Specializeds.SingleOrDefault(sp => sp.SpecializedID == id && sp.Deleted == false);
        }

        public bool Update(Specialized specializedUpdate, string username)
        {
            var specializedCurr = GetSpecializedByID(specializedUpdate.SpecializedID);
            if (specializedCurr == null) return false;

            specializedCurr.SpecializedName = specializedUpdate.SpecializedName;
            specializedCurr.Descripstion = specializedUpdate.Descripstion;
            Helper.SetDefaultEditModel(specializedCurr, username);

            db.SaveChanges();
            return true;
        }

        public int Delete(int id, string username)
        {
            var specialized = GetSpecializedByID(id);
            if (specialized == null) return -1;
            specialized.Deleted = true;
            Helper.SetDefaultEditModel(specialized, username);
            db.SaveChanges();
            return 1;
        }

    }
}
