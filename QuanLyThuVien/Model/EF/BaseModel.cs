using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public abstract class BaseModel
    {
        [StringLength(30)]
        public string CreateBy { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày")]
        public DateTime CreateDate { get; set; }

        [StringLength(30)]
        public string EditBy { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày")]
        public DateTime EditDate { get; set; }

        public bool Deleted { get; set; }

        public BaseModel()
        {
            Deleted = false;
        }
    }
}
