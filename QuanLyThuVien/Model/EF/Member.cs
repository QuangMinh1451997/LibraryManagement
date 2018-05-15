using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class Member : Person
    {
        public int MemberID { get; set; }

        [StringLength(10)]
        public string StudentCode { get; set; }

        public virtual ICollection<HireDetail> HireDetails { get; set; }
    }
}
