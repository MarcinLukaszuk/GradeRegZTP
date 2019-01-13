namespace GradeRegZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tmp44 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HourOfDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(),
                        SubjectId = c.Int(nullable: false),
                        StudentsGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentsGroups", t => t.StudentsGroupId, cascadeDelete: false)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .Index(t => t.SubjectId)
                .Index(t => t.StudentsGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HourOfDays", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.HourOfDays", "StudentsGroupId", "dbo.StudentsGroups");
            DropIndex("dbo.HourOfDays", new[] { "StudentsGroupId" });
            DropIndex("dbo.HourOfDays", new[] { "SubjectId" });
            DropTable("dbo.HourOfDays");
        }
    }
}
