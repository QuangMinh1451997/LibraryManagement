namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduniqueforStudentCodeandBookCode : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Books", "BookCode", unique: true);
            CreateIndex("dbo.Members", "StudentCode", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Members", new[] { "StudentCode" });
            DropIndex("dbo.Books", new[] { "BookCode" });
        }
    }
}
