using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class LibraryContext: DbContext
    {

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Specialized> Specializeds { get; set; }
        public DbSet<BookType> BookTypes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<HireDetail> HireDetails { get; set; }
        public DbSet<HireTime> HireTimes { get; set; }
        public LibraryContext() : base()
        {

        }


    }
}
