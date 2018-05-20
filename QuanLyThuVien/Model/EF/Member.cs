
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class Member : Person
    {
        [Required(ErrorMessage = "Mã không hợp lệ")]
        public int MemberID { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Vui lòng nhập")]
        [Index(IsUnique = true)]
        public string StudentCode { get; set; }

        public virtual ICollection<HireDetail> HireDetails { get; set; }
    }
}
