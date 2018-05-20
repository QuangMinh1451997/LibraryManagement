namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStatuspropertyforhiredetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HireDetails", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HireDetails", "Status");
        }
    }
}
