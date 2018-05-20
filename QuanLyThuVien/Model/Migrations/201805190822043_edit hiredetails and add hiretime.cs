namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edithiredetailsandaddhiretime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HireTimes",
                c => new
                    {
                        HireTimeID = c.Int(nullable: false, identity: true),
                        NumberOfWeek = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 30),
                        CreateDate = c.DateTime(nullable: false),
                        EditBy = c.String(maxLength: 30),
                        EditDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.HireTimeID);
            
            AddColumn("dbo.HireDetails", "HireTimeID", c => c.Int(nullable: false));
            AlterColumn("dbo.HireDetails", "Status", c => c.Int(nullable: false));
            CreateIndex("dbo.HireDetails", "HireTimeID");
            AddForeignKey("dbo.HireDetails", "HireTimeID", "dbo.HireTimes", "HireTimeID", cascadeDelete: true);
            DropColumn("dbo.HireDetails", "ExpirationDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HireDetails", "ExpirationDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.HireDetails", "HireTimeID", "dbo.HireTimes");
            DropIndex("dbo.HireDetails", new[] { "HireTimeID" });
            AlterColumn("dbo.HireDetails", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.HireDetails", "HireTimeID");
            DropTable("dbo.HireTimes");
        }
    }
}
