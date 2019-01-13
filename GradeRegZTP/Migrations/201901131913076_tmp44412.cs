namespace GradeRegZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tmp44412 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HourOfDays", "HourId", c => c.Int(nullable: false));
            CreateIndex("dbo.HourOfDays", "HourId");
            AddForeignKey("dbo.HourOfDays", "HourId", "dbo.Hours", "Id", cascadeDelete: false);
            DropColumn("dbo.HourOfDays", "Hour");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HourOfDays", "Hour", c => c.DateTime());
            DropForeignKey("dbo.HourOfDays", "HourId", "dbo.Hours");
            DropIndex("dbo.HourOfDays", new[] { "HourId" });
            DropColumn("dbo.HourOfDays", "HourId");
        }
    }
}
