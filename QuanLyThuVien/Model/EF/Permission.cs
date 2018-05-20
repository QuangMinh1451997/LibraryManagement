using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class Permission:BaseModel
    {
        public int PermissionID { get; set; }

        [StringLength(250)]
        [MinLength(3,ErrorMessage ="Tên quyền phải có ít nhất 3 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập")]
        public string PermissionName { get; set; }

        public bool QuanLy { get; set; }

        public bool ThuThu { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
