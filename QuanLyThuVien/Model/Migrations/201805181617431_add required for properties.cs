namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequiredforproperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Permissions", "PermissionName", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Books", "BookCode", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.BookTypes", "BookTypeName", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Members", "StudentCode", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "StudentCode", c => c.String(maxLength: 10));
            AlterColumn("dbo.BookTypes", "BookTypeName", c => c.String(maxLength: 250));
            AlterColumn("dbo.Books", "BookCode", c => c.String(maxLength: 10));
            AlterColumn("dbo.Permissions", "PermissionName", c => c.String(maxLength: 250));
        }
    }
}
