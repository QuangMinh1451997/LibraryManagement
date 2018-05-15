namespace Model.Migrations
{
    using Model.EF;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Model.EF.LibraryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Model.EF.LibraryContext context)
        {
            /*var permissions = new List<Permission>() {
                new Permission(){
                    PermissionID = 1,
                    PermissionName = "Admin",
                    QuanLy = true,
                    ThuThu = true,
                    CreateDate = DateTime.Today,
                    CreateBy = "admin",
                    EditBy = "admin"
                },
                new Permission(){
                    PermissionID = 2,
                    PermissionName = "Quản lý",
                    QuanLy = true,
                    ThuThu = false,
                    CreateDate = DateTime.Today,
                    CreateBy = "admin",
                    EditBy = "admin"
                },
                new Permission(){
                    PermissionID = 3,
                    PermissionName = "Thủ thư",
                    QuanLy = false,
                    ThuThu = true,
                    CreateDate = DateTime.Today,
                    CreateBy = "admin",
                    EditDate = DateTime.Today,
                    EditBy = "admin"
                },
                 new Permission(){
                    PermissionID = 4,
                    PermissionName = "Thủ thư 2",
                    QuanLy = false,
                    ThuThu = true,
                    CreateDate = DateTime.Today,
                    CreateBy = "admin",
                    EditDate = DateTime.Today,
                    EditBy = "admin"
                }
            };
            permissions.ForEach(p => context.Permissions.AddOrUpdate(pe=>pe.PermissionID,p));
            context.SaveChanges();

            var employees = new List<Employee>() {
                new Employee()
                {
                    FirstName = "Đinh Quang",
                    LastName = "Minh",
                    Address = "Trên trời",
                    BirthDay = new DateTime(1997,5,14),
                    Phone = "123456789",
                    Sex = false,
                    UrlAvatar = "user.png",
                    CreateDate = DateTime.Today,
                    CreateBy = "admin",
                    EditDate = DateTime.Today,
                    EditBy = "admin"
                },
                new Employee()
                {
                    FirstName = "Ngô Trung",
                    LastName = "Hưng",
                    Address = "Dưới biển",
                    BirthDay = new DateTime(1997,6,12),
                    Phone = "12345678910",
                    Sex = false,
                    UrlAvatar = "user.png",
                    CreateDate = DateTime.Today,
                    CreateBy = "admin",
                    EditDate = DateTime.Today,
                    EditBy = "admin"
                },
                new Employee()
                {
                    FirstName = "Mai Cồ",
                    LastName = "Tèo",
                    Address = "Trên rừng",
                    BirthDay = new DateTime(1997,5,30),
                    Phone = "0123456789",
                    Sex = true,
                    UrlAvatar = "user.png",
                    CreateDate = DateTime.Today,
                    CreateBy = "admin",
                    EditDate = DateTime.Today,
                    EditBy = "admin"
                }
            };
            employees.ForEach(e => context.Employees.AddOrUpdate(em => em.Phone, e));
            context.SaveChanges();

            var accounts = new List<Account>() {
                new Account()
                {
                    Username = "admin",
                    Password = "123",
                    EmployeeID = 1,
                    PermissionID = 1,
                    CreateBy = "admin",
                    EditBy = "admin",
                    CreateDate= DateTime.Today,
                    EditDate= DateTime.Today,
                },
                new Account()
                {
                    Username = "quanly",
                    Password = "123",
                    EmployeeID = 2,
                    PermissionID = 2,
                    CreateBy = "admin",
                    EditBy = "admin",
                    CreateDate= DateTime.Today,
                    EditDate= DateTime.Today,
                },
                new Account()
                {
                    Username = "thuthu",
                    Password = "123",
                    EmployeeID = 3,
                    PermissionID = 3,
                    CreateBy = "admin",
                    EditBy = "admin",
                    CreateDate= DateTime.Today,
                    EditDate= DateTime.Today,
                }

            };

            accounts.ForEach(ac => context.Accounts.AddOrUpdate(a => a.Username, ac));
            context.SaveChanges();*/
        }
    }
}
