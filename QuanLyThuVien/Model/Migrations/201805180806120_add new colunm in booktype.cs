namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewcolunminbooktype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookTypes", "UrlImage", c => c.String(maxLength: 250));
            AddColumn("dbo.BookTypes", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookTypes", "Description");
            DropColumn("dbo.BookTypes", "UrlImage");
        }
    }
}
