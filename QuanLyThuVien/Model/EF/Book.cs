using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class Book : BaseModel
    {
        public int BookID { get; set; }

        [StringLength(10)]
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
