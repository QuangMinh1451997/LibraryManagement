using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    [Serializable]
    public class EmployeeLogin
    {
        public int EmployeeID { get; set; }

        public string Username { get; set; }

        public string PermissionName { get; set; }

        public bool QuanLy { get; set; }

        public bool ThuThu { get; set; }
    }
}
