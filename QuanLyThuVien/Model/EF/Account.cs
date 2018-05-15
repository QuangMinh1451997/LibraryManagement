using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class Account : BaseModel
    {
        [Key]
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }

        [StringLength(30)]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        [StringLength(250)]
        public string Password { get; set; }

        public virtual Employee Employee { get; set; }

        public int PermissionID { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
