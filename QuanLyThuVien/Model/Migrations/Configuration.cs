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
           /* var permissions = new List<Permission>() {
                new Permission(){
                    PermissionName = "Admin",
                    QuanLy = true,
                    ThuThu = true,
                    CreateDate = DateTime.Today,
                    CreateBy = "admin",
                    EditDate = DateTime.Today,
                    EditBy = "admin"
                },
                new Permission(){
                    PermissionName = "Quản lý",
                    QuanLy = true,
                    ThuThu = false,
                    CreateDate = DateTime.Today,
                    CreateBy = "admin",
                    EditDate = DateTime.Today,
                    EditBy = "admin"
                },
                new Permission(){
                    PermissionName = "Thủ thư",
                    QuanLy = false,
                    ThuThu = true,
                    CreateDate = DateTime.Today,
                    CreateBy = "admin",
                    EditDate = DateTime.Today,
                    EditBy = "admin"
                }
            };
            permissions.ForEach(p => context.Permissions.AddOrUpdate(p));
            context.SaveChanges();*/

            /*var employees = new List<Employee>() {
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
                    EditBy = "admin",
                    PermissionID = 7
                }
            };
            employees.ForEach(e => context.Employees.AddOrUpdate(em => em.Phone, e));
            context.SaveChanges();*/
        }
    }
}
