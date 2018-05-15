using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public abstract class Person : BaseModel
    {
        [StringLength(100)]
        [Required(ErrorMessage = "Vui lòng nhập họ")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public bool Sex { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(13)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string UrlAvatar { get; set; }

        public virtual int Age
        {
            get
            {
                return DateTime.Today.Year - BirthDay.Year;
            }          
        }

        public virtual string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
