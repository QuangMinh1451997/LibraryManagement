using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class Employee: Person
    {
        public int EmployeeID { get; set; }

        public virtual Account Account { get; set; }

    }
}
