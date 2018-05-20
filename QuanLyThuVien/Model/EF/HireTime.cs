using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class HireTime : BaseModel
    {
        public int HireTimeID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số tuần")]
        public int NumberOfWeek { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<HireDetail> HireDetails { get; set; }
    }
}
