namespace GradeRegZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tmp444 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayOfWeeks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.HourOfDays", "Hour", c => c.DateTime());
            AddColumn("dbo.HourOfDays", "DayOfWeekId", c => c.Int(nullable: false));
            CreateIndex("dbo.HourOfDays", "DayOfWeekId");
            AddForeignKey("dbo.HourOfDays", "DayOfWeekId", "dbo.DayOfWeeks", "Id", cascadeDelete: false);
            DropColumn("dbo.HourOfDays", "StartDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HourOfDays", "StartDate", c => c.DateTime());
            DropForeignKey("dbo.HourOfDays", "DayOfWeekId", "dbo.DayOfWeeks");
            DropIndex("dbo.HourOfDays", new[] { "DayOfWeekId" });
            DropColumn("dbo.HourOfDays", "DayOfWeekId");
            DropColumn("dbo.HourOfDays", "Hour");
            DropTable("dbo.DayOfWeeks");
        }
    }
}
