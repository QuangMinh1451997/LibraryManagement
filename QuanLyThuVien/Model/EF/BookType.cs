using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class BookType : BaseModel
    {
        public int BookTypeID { get; set; }

        [StringLength(250)]
        public string BookTypeName { get; set; }

        [StringLength(250)]
        public string PublishingHouse { get; set; }

        public int NumberOfPage { get; set; }

        [StringLength(15)]
        public string Size { get; set; }

        [StringLength(250)]
        public string Author { get; set; }

        public int Quantity { get; set; }

        public int SpecializedID { get; set; }

        public virtual Specialized Specialized { get; set; }

        public virtual ICollection<Book> Books { get; set; }

    }
}
