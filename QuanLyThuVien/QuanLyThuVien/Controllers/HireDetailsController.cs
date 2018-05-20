using Model.DAO;
using Model.EF;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class HireDetailsController : ThuThuPermissionController
    {
        HireDetailsDAO dao = new HireDetailsDAO();
        // GET: HireDetails
        public ActionResult Index()
        {
            var listHireDetails = dao.GetAllHireDetails();
            return View(listHireDetails);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var hireDetail = dao.GetHireDetailByID(id.Value);
            if (hireDetail == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView("_Details", hireDetail);
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var daoBook = new BookDAO();
            var book = daoBook.GetBookByIDNotHire(id.Value);
            if (book == null)
                return HttpNotFound();
            HireDetail hireDetail = new HireDetail()
            {
                BookID = id.Value,
                Book = book
            };
            CreateSelectList();
            return View(hireDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? bookID, int? memberID, int? hireTimeID)
        {
            if (bookID == null || memberID == null || hireTimeID == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var bookDAO = new BookDAO();
            var book = bookDAO.GetBookByID(bookID.Value);
            if (book == null)
                ModelState.AddModelError("", "Sách này không tồn tại");
            else if (book.Status == true)
                ModelState.AddModelError("", "Sách này đã có người mượn");
            var memberDAO = new MemberDAO();
            var member = memberDAO.GetMemberByID(memberID.Value);
            if (member == null)
                ModelState.AddModelError("", "Thành viên này không tồn tại");
            else if (member.HireDetails.SingleOrDefault(hr => hr.Status == 2) != null)
                ModelState.AddModelError("", "Thành viên này có phiếu mượn hết hạn. Không thể mượn thêm");
            else if (member.HireDetails.Where(hr=>hr.Status==1).Count() >= 3)
                ModelState.AddModelError("", "Thành viên này đã mượn 3 quyển. Không thể mượn thêm");
            
            var hireDetail = new HireDetail()
            {
                Book = book
            };
            if (ModelState.IsValid)
            {
                try
                {
                    var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                    var result = dao.Insert(bookID.Value, memberID.Value, hireTimeID.Value, user.EmployeeID, user.Username);
                    if (result)
                        return Content("<script>alert('Thành công'); window.location.href='/HireDetails/Index'</script>");
                }
                catch
                {
                    ModelState.AddModelError("", "Có lỗi xãy ra vui lòng tải lại trang");
                }
            }
            CreateSelectList();
            return View(hireDetail);
        }

        [HttpPost]
        public int PayBook(int? id)
        {
            if (id == null)
                return -2;
            try
            {
                var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                var result = dao.Pay(id.Value, user.Username);
                if (result == false)
                    return -1;
                return 1;
            }
            catch
            {
                return -2;
            }

        }
        private void CreateSelectList()
        {
            var dao = new HireTimeDAO();
            ViewBag.SelListHireTime = new SelectList(dao.GetAllHireTimeActive(), "HireTimeID", "NumberOfWeek");
        }
    }
}