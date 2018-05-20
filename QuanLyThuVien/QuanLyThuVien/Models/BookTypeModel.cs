using Model.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Models
{
    public class BookTypeModel
    {
        public int BookTypeID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Vui lòng nhập tên sách")]
        [MinLength(3, ErrorMessage = "Vui lòng nhập hơn 3 ký tự")]
        public string BookTypeName { get; set; }

        [StringLength(250)]
        [MinLength(3, ErrorMessage = "Vui lòng nhập hơn 3 ký tự")]
        public string PublishingHouse { get; set; }

        [Range(10, int.MaxValue, ErrorMessage = "Số trang phải lớn hơn 10")]
        [Required(ErrorMessage = "Vui lòng nhập số trang")]
        public int NumberOfPage { get; set; }

        [StringLength(15)]
        public string Size { get; set; }

        [StringLength(250)]
        public string Author { get; set; }

        [StringLength(250)]
        public string UrlImage { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public int Quantity { get; set; }

        public int SpecializedID { get; set; }

        public virtual Specialized Specialized { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}