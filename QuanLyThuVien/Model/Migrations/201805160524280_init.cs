namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        Username = c.String(maxLength: 30),
                        Password = c.String(maxLength: 250),
                        CreateBy = c.String(maxLength: 30),
                        CreateDate = c.DateTime(nullable: false),
                        EditBy = c.String(maxLength: 30),
                        EditDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .Index(t => t.EmployeeID)
                .Index(t => t.Username, unique: true);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        PermissionID = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 50),
                        BirthDay = c.DateTime(nullable: false),
                        Sex = c.Boolean(nullable: false),
                        Address = c.String(maxLength: 250),
                        Phone = c.String(maxLength: 13),
                        UrlAvatar = c.String(maxLength: 100),
                        CreateBy = c.String(maxLength: 30),
                        CreateDate = c.DateTime(nullable: false),
                        EditBy = c.String(maxLength: 30),
                        EditDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Permissions", t => t.PermissionID, cascadeDelete: true)
                .Index(t => t.PermissionID);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        PermissionID = c.Int(nullable: false, identity: true),
                        PermissionName = c.String(maxLength: 250),
                        QuanLy = c.Boolean(nullable: false),
                        ThuThu = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 30),
                        CreateDate = c.DateTime(nullable: false),
                        EditBy = c.String(maxLength: 30),
                        EditDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PermissionID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        BookCode = c.String(maxLength: 10),
                        Status = c.Boolean(nullable: false),
                        ReceiptDate = c.DateTime(nullable: false),
                        BookTypeID = c.Int(nullable: false),
                        CreateBy = c.String(maxLength: 30),
                        CreateDate = c.DateTime(nullable: false),
                        EditBy = c.String(maxLength: 30),
                        EditDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookID)
                .ForeignKey("dbo.BookTypes", t => t.BookTypeID, cascadeDelete: true)
                .Index(t => t.BookTypeID);
            
            CreateTable(
                "dbo.BookTypes",
                c => new
                    {
                        BookTypeID = c.Int(nullable: false, identity: true),
                        BookTypeName = c.String(maxLength: 250),
                        PublishingHouse = c.String(maxLength: 250),
                        NumberOfPage = c.Int(nullable: false),
                        Size = c.String(maxLength: 15),
                        Author = c.String(maxLength: 250),
                        Quantity = c.Int(nullable: false),
                        SpecializedID = c.Int(nullable: false),
                        CreateBy = c.String(maxLength: 30),
                        CreateDate = c.DateTime(nullable: false),
                        EditBy = c.String(maxLength: 30),
                        EditDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookTypeID)
                .ForeignKey("dbo.Specializeds", t => t.SpecializedID, cascadeDelete: true)
                .Index(t => t.SpecializedID);
            
            CreateTable(
                "dbo.Specializeds",
                c => new
                    {
                        SpecializedID = c.Int(nullable: false, identity: true),
                        SpecializedName = c.String(maxLength: 250),
                        Descripstion = c.String(maxLength: 500),
                        CreateBy = c.String(maxLength: 30),
                        CreateDate = c.DateTime(nullable: false),
                        EditBy = c.String(maxLength: 30),
                        EditDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SpecializedID);
            
            CreateTable(
                "dbo.HireDetails",
                c => new
                    {
                        HireDetailID = c.Int(nullable: false, identity: true),
                        BookID = c.Int(nullable: false),
                        MemberID = c.Int(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        Penalty = c.Single(nullable: false),
                        CreateBy = c.String(maxLength: 30),
                        CreateDate = c.DateTime(nullable: false),
                        EditBy = c.String(maxLength: 30),
                        EditDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.HireDetailID)
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberID, cascadeDelete: true)
                .Index(t => t.BookID)
                .Index(t => t.MemberID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberID = c.Int(nullable: false, identity: true),
                        StudentCode = c.String(maxLength: 10),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 50),
                        BirthDay = c.DateTime(nullable: false),
                        Sex = c.Boolean(nullable: false),
                        Address = c.String(maxLength: 250),
                        Phone = c.String(maxLength: 13),
                        UrlAvatar = c.String(maxLength: 100),
                        CreateBy = c.String(maxLength: 30),
                        CreateDate = c.DateTime(nullable: false),
                        EditBy = c.String(maxLength: 30),
                        EditDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MemberID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HireDetails", "MemberID", "dbo.Members");
            DropForeignKey("dbo.HireDetails", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.HireDetails", "BookID", "dbo.Books");
            DropForeignKey("dbo.BookTypes", "SpecializedID", "dbo.Specializeds");
            DropForeignKey("dbo.Books", "BookTypeID", "dbo.BookTypes");
            DropForeignKey("dbo.Accounts", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "PermissionID", "dbo.Permissions");
            DropIndex("dbo.HireDetails", new[] { "EmployeeID" });
            DropIndex("dbo.HireDetails", new[] { "MemberID" });
            DropIndex("dbo.HireDetails", new[] { "BookID" });
            DropIndex("dbo.BookTypes", new[] { "SpecializedID" });
            DropIndex("dbo.Books", new[] { "BookTypeID" });
            DropIndex("dbo.Employees", new[] { "PermissionID" });
            DropIndex("dbo.Accounts", new[] { "Username" });
            DropIndex("dbo.Accounts", new[] { "EmployeeID" });
            DropTable("dbo.Members");
            DropTable("dbo.HireDetails");
            DropTable("dbo.Specializeds");
            DropTable("dbo.BookTypes");
            DropTable("dbo.Books");
            DropTable("dbo.Permissions");
            DropTable("dbo.Employees");
            DropTable("dbo.Accounts");
        }
    }
}
