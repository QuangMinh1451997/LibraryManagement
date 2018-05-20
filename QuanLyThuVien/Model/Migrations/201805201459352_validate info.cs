namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validateinfo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Specializeds", "SpecializedName", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Specializeds", "SpecializedName", c => c.String(maxLength: 250));
        }
    }
}
