using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class PermissionDAO
    {
        LibraryContext db = null;
        public PermissionDAO()
        {
            db = new LibraryContext();
        }

        public bool Edit(Permission permissionUpdate, string username)
        {
            var permissionCurr = db.Permissions.SingleOrDefault(p => p.PermissionID == permissionUpdate.PermissionID && p.Deleted == false);
            if (permissionCurr == null)
                return false;

            permissionCurr.PermissionName = permissionUpdate.PermissionName;
            permissionCurr.QuanLy = permissionUpdate.QuanLy;
            permissionCurr.ThuThu = permissionUpdate.ThuThu;
            Helper.SetDefaultEditModel(permissionCurr, username);
            db.SaveChanges();
            return true;

        }

        public IEnumerable<Permission> GetAllPermission()
        {
            return db.Permissions.Where(p=>p.Deleted == false);
        }

        public Permission GetPermissionById(int id)
        {
            return db.Permissions.SingleOrDefault(p => p.PermissionID == id && p.Deleted == false);
        }
    }
}
