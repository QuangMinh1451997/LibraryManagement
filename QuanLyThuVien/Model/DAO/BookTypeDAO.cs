using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class BookTypeDAO
    {
        LibraryContext db = null;
        public BookTypeDAO()
        {
            db = new LibraryContext();
        }

        public IEnumerable<BookType> GetAllBookType()
        {
            return db.BookTypes.Where(bt => bt.Deleted == false);
        }

        public BookType GetBookTypeByID(int id)
        {
            return db.BookTypes.SingleOrDefault(bt => bt.Deleted == false && bt.BookTypeID == id);
        }

        public int GetIDMax()
        {
            return Helper.GetNextID(db, "BookTypes");
           // return db.Database.SqlQuery<int>("SELECT IDENT_CURRENT('BookTypes')").SingleOrDefault();
        }

        public int Insert(BookType newBookType, string username)
        {
            Helper.SetDefaultCreateModel(newBookType, username);
            newBookType.Quantity = 0;
            db.BookTypes.Add(newBookType);
            db.SaveChanges();
            return newBookType.BookTypeID;
        }

        public bool Update(BookType bookTypeUpdate, string username)
        {
            var bookTypeCurr = GetBookTypeByID(bookTypeUpdate.BookTypeID);
            if (bookTypeCurr == null) return false;

            Helper.SetDefaultEditModel(bookTypeCurr, username);
            bookTypeCurr.BookTypeName = bookTypeUpdate.BookTypeName;
            bookTypeCurr.Author = bookTypeUpdate.Author;
            bookTypeCurr.Size = bookTypeUpdate.Size;
            bookTypeCurr.PublishingHouse = bookTypeUpdate.PublishingHouse;
            bookTypeCurr.NumberOfPage = bookTypeUpdate.NumberOfPage;
            bookTypeCurr.UrlImage = bookTypeUpdate.UrlImage;
            bookTypeCurr.Description = bookTypeUpdate.Description;
            bookTypeCurr.SpecializedID = bookTypeUpdate.SpecializedID;

            db.SaveChanges();
            return true;
        }

        public int CheckBeforeDelete(int id)
        {
            var books = GetBookTypeByID(id).Books;
            foreach(Book book in books)
            {
                if (book.Status == true)
                    return -1;
            }
            if (books.Count > 0)
                return -2;
            return 1;
        }

        public int Delete(int id, string username)
        {
            var bookType = db.BookTypes
                .SingleOrDefault(bt => bt.Deleted == false && bt.BookTypeID == id);
            if (bookType == null) return -1;
            bookType.Deleted = true;
            Helper.SetDefaultEditModel(bookType, username);
            db.SaveChanges();
            return 1;
        }

        public object GetData(int id)
        {
            var data = from bookType in db.BookTypes
                       from specialized in db.Specializeds
                       where bookType.BookTypeID == id && bookType.Deleted == false && bookType.SpecializedID == specialized.SpecializedID
                       select new
                       {
                           SpecializedName = specialized.SpecializedName,
                           Quantity = bookType.Quantity,
                           UrlImage = bookType.UrlImage
                       };
            return data;
        }
    }
}
