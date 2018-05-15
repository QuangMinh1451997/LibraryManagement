using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class Specialized : BaseModel
    {
        public int SpecializedID { get; set; }

        [StringLength(250)]
        public string SpecializedName { get; set; }

        [StringLength(500)]
        public string Descripstion { get; set; }

        public virtual ICollection<BookType> BookTypes { get; set; }

    }
}
