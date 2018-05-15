using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class Permission:BaseModel
    {
        public int PermissionID { get; set; }

        [StringLength(250)]
        public string PermissionName { get; set; }

        public bool QuanLy { get; set; }

        public bool ThuThu { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
