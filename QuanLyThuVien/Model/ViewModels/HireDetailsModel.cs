using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    public class HireDetailsModel
    {
        public int HireDetailsID { get; set; }

        public int BookID { get; set; }

        public int MemberID { get; set; }

        public int HireTimeID { get; set; }

        public int Status { get; set; }

        public Book Book { get; set; }

        public Member Member { get; set; }

        public HireTime HireTime { get; set; }
    }
}
