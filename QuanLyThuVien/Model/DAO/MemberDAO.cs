using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class MemberDAO
    {
        LibraryContext db = null;
        public MemberDAO()
        {
            db = new LibraryContext();
        }

        public IEnumerable<Member> GetAllMember()
        {
            return db.Members.Where(m => m.Deleted == false);
        }

        public Member GetMemberByID(int id)
        {
            var member = db.Members.SingleOrDefault(m => m.Deleted == false && m.MemberID == id);
            return member;
        }

        public Member GetMemberByStudentCode(string studentCode)
        {
            return db.Members
                .Include(m => m.HireDetails)
                .SingleOrDefault(m => m.Deleted == false && m.StudentCode.ToLower() == studentCode);

        }

        public Member GetMemberByStudentCodeNoLoad(string studentCode)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Members
                .SingleOrDefault(m => m.Deleted == false && m.StudentCode.ToLower() == studentCode);

        }

        public int GetNextID()
        {
            return Helper.GetNextID(db, "Members");
        }

        public bool Insert(Member newMember, string username)
        {
            Helper.SetDefaultCreateModel(newMember, username);
            db.Members.Add(newMember);
            db.SaveChanges();
            return true;
        }

        public bool Update(Member memberUpdate, string username)
        {
            var memberCurr = GetMemberByID(memberUpdate.MemberID);
            if (memberCurr == null)
                return false;

            memberCurr.FirstName = memberUpdate.FirstName;
            memberCurr.LastName = memberUpdate.LastName;
            memberCurr.BirthDay = memberUpdate.BirthDay;
            memberCurr.Sex = memberUpdate.Sex;
            memberCurr.Address = memberUpdate.Address;
            memberCurr.Phone = memberUpdate.Phone;
            memberCurr.UrlAvatar = memberUpdate.UrlAvatar;
            memberCurr.StudentCode = memberUpdate.StudentCode;
            db.SaveChanges();
            return true;
        }

        public int Delete(int id, string username)
        {
            var member = db.Members.Include(m => m.HireDetails).SingleOrDefault(m => m.MemberID == id && m.Deleted == false);
            if (member == null)
                return -1;
            if (member.HireDetails.Where(hr => hr.Status == 1).Count() > 0)
                return -2;
            member.Deleted = true;
            Helper.SetDefaultEditModel(member, username);
            db.SaveChanges();
            return 1;
        }
        // true : đã tồn tại
        //false : chưa tồn tại
        public bool CheckStudentCodeAvailabel(int id, string studentCode)
        {
            var member = db.Members.SingleOrDefault(m => m.MemberID != id && m.StudentCode.ToLower().CompareTo(studentCode) == 0 && m.Deleted == false);
            return (member != null) ? true : false;
        }
        public bool CheckStudentCodeAvailabel(string studentCode)
        {
            var member = db.Members.SingleOrDefault(m => m.StudentCode.ToLower().CompareTo(studentCode) == 0 && m.Deleted == false);
            return (member != null) ? true : false;
        }
    }
}
