using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class Book : BaseModel
    {
        public int BookID { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Vui lòng nhập")]
        [MinLength(2, ErrorMessage = "Vui lòng nhập hơn 2 ký tự")]
        [Index(IsUnique = true)]
        public string BookCode { get; set; }

        public bool Status { get; set; }

        public DateTime ReceiptDate { get; set; }

        public int BookTypeID { get; set; }

        public virtual BookType BookType { get; set; }

        public virtual ICollection<HireDetail> HireDetails{ get; set; }

        public Book()
        {
            Status = false;
        }
    }
}
