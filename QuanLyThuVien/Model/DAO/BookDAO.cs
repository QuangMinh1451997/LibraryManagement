using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class BookDAO
    {
        private LibraryContext db = null;
        public BookDAO()
        {
            db = new LibraryContext();
        }

        public IEnumerable<Book> GetAllBook()
        {
            return db.Books
                .Where(b => b.Deleted == false).OrderByDescending(b=>b.Status);
        }

        public Book GetBookByID(int id)
        {
            return db.Books
                .Include(b => b.BookType.Specialized)
                .Where(b => b.BookID == id && b.Deleted == false).SingleOrDefault();
        }

        public Book GetBookByIDNotHire(int id)
        {
            return db.Books
                .Include(b => b.BookType.Specialized)
                .Where(b => b.BookID == id && b.Deleted == false && b.Status==false).SingleOrDefault();
        }

        //public Book GetBookByBookCode(string bookCode)
        //{
        //    //return db.Books
        //    //    .Include(b => b.BookType.Specialized)
        //    //    .Where(b => b.BookCode == bookCode && b.Deleted == false && ).SingleOrDefault();
        //}

        public bool Insert(Book newBook, string username)
        {
            Helper.SetDefaultCreateModel(newBook, username);
            newBook.ReceiptDate = DateTime.Today;
            db.Books.Add(newBook);
            db.SaveChanges();
            return true;
        }

        public bool Update(Book bookUpdate, string username)
        {
            var bookCurr = db.Books.SingleOrDefault(b => b.BookID == bookUpdate.BookID && b.Deleted == false);
            if (bookCurr == null)
                return false;
            bookCurr.BookCode = bookUpdate.BookCode;
            bookCurr.BookTypeID = bookUpdate.BookTypeID;
            db.SaveChanges();
            return true;
        }

        public int Delete(int id, string username)
        {
            var book = db.Books.SingleOrDefault(b => b.BookID == id && b.Deleted == false);
            if (book == null)
                return -1;
            book.Deleted = true;
            Helper.SetDefaultEditModel(book, username);
            db.SaveChanges();
            return 1;
        }

        public bool CheckBookCodeIsExisted(string bookCode)
        {
            var book = db.Books.SingleOrDefault(b => b.BookCode.ToLower() == bookCode && b.Deleted == false);
            return (book != null) ? true : false;
        }
        public bool CheckBookCodeIsExisted(int bookID,string bookCode)
        {
            var book = db.Books.SingleOrDefault(b => b.BookID != bookID && b.BookCode.ToLower() == bookCode && b.Deleted == false);
            return (book != null) ? true : false;
        }
    }
}
