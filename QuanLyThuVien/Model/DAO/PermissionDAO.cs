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
        public IEnumerable<Permission> GetAllPermission()
        {
            return db.Permissions;
        }
    }
}
