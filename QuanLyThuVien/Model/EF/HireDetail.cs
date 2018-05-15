using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class HireDetail : BaseModel
    {
        public int HireDetailID { get; set; }

        public int BookID { get; set; }

        public int MemberID { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int EmployeeID { get; set; }

        public float Penalty { get; set; }

        public virtual Book Book { get; set; }

        public virtual Member Member { get; set; }

        public virtual Employee Employee { get; set; }

    }
}
