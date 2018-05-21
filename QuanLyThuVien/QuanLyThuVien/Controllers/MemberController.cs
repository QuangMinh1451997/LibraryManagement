
using Model.DAO;
using Model.EF;
using Model.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class MemberController : ThuThuPermissionController
    {
        MemberDAO dao = new MemberDAO();
        // GET: Member
        public ActionResult Index()
        {
            var listMember = dao.GetAllMember().ToList();
            return View(listMember);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var member = dao.GetMemberByID(id.Value);
            if(member==null)
                return HttpNotFound();
            if(Request.IsAjaxRequest())
                return PartialView("_Details", member);
            return HttpNotFound();
        }
        [HttpGet]
        public ActionResult Create()
        {
            CreateSelectList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName, LastName, BirthDay, Sex, Address, Phone, StudentCode")]Member newMember, HttpPostedFileBase UrlAvatar)
        {
            if (UrlAvatar != null && Helper.IsImage(Path.GetExtension(UrlAvatar.FileName)) == false)
                ModelState.AddModelError("", "Tệp tin hình ảnh phải có phần mở rộng là : .jpeg, .png, .jpg");
            var studentCodeIsExisted = dao.CheckStudentCodeAvailabel(newMember.StudentCode.ToLower());
            if (studentCodeIsExisted == true)
            {
                ModelState.AddModelError("", "mã sinh viên đã tồn tại");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                    string path = null;
                    if (UrlAvatar != null)
                    {
                        var nextID = dao.GetNextID();
                        newMember.UrlAvatar = nextID + Path.GetExtension(UrlAvatar.FileName);
                        path = Path.Combine(Server.MapPath("~/Upload/Images/Member"), newMember.UrlAvatar);
                    }
                    else
                        newMember.UrlAvatar = "user.png";
                    bool result = dao.Insert(newMember, user.Username);
                    if (result == true)
                    {
                        if (path != null)
                            UrlAvatar.SaveAs(path);
                        return RedirectToAction("Index");
                    }
                        
                }
                catch
                {
                    ModelState.AddModelError("", "Có lỗi xãy ra, Vui lòng tải lại trang");
                }
            }
            CreateSelectList();
            return View(newMember);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var member = dao.GetMemberByID(id.Value);
            if (member == null)
                return HttpNotFound();
            CreateSelectList();
            return View(member);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID, FirstName, LastName, BirthDay, Sex, Address, Phone, StudentCode, UrlAvatar")]Member memberUpdate, HttpPostedFileBase UrlAvatar)
        {
            if (UrlAvatar != null && Helper.IsImage(Path.GetExtension(UrlAvatar.FileName)) == false)
                ModelState.AddModelError("", "Tệp tin hình ảnh phải có phần mở rộng là : .jpeg, .png, .jpg");
            var studentCodeIsExisted = dao.CheckStudentCodeAvailabel(memberUpdate.MemberID, memberUpdate.StudentCode.ToLower());
            if (studentCodeIsExisted == true)
            {
                ModelState.AddModelError("", "Mã sinh viên này đã tồn tại");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                    string path = null;
                    if (UrlAvatar != null)
                    {
                        memberUpdate.UrlAvatar = memberUpdate.MemberID + Path.GetExtension(UrlAvatar.FileName);
                        path = Path.Combine(Server.MapPath("~/Upload/Images/Member"), memberUpdate.UrlAvatar);
                    }
                    bool result = dao.Update(memberUpdate, user.Username);
                    if (result == true)
                    {
                        if (path != null)
                            UrlAvatar.SaveAs(path);
                        return Content("<script>alert('Đã lưu'); window.location.href='/Member/Index';</script>");
                    }
                    else
                        return HttpNotFound();

                }
                catch
                {
                    ModelState.AddModelError("", "Có lỗi xãy ra, Vui lòng tải lại trang");
                }
            }
            CreateSelectList();
            return View(memberUpdate);
        }

        [HttpPost]
        public int Delete(int? id)
        {
            try
            {
                if (id == null)
                    return -3;
                var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                return dao.Delete(id.Value, user.Username);
            }
            catch
            {
                return -3;
            }
        }

        [HttpGet]
        public string GetData(string studentCode)
        {
            var student = dao.GetMemberByStudentCodeNoLoad(studentCode.ToLower());
            return JsonConvert.SerializeObject(student);
        }

        private void CreateSelectList()
        {
            ViewBag.SelListSex = new SelectList(new SelectListItem[]
            {
                new SelectListItem
                {
                    Value = "true",
                    Text = "Nam"
                },
                new SelectListItem
                {
                    Value = "false",
                    Text = "Nữ"
                }
            }, "Value", "Text");
        }
    }
}