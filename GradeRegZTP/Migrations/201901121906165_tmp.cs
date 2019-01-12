namespace GradeRegZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tmp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentSubjectGrades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        GradeId = c.Int(nullable: false),
                        Owner = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grades", t => t.GradeId, cascadeDelete: false)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .Index(t => t.SubjectId)
                .Index(t => t.GradeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentSubjectGrades", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.StudentSubjectGrades", "GradeId", "dbo.Grades");
            DropIndex("dbo.StudentSubjectGrades", new[] { "GradeId" });
            DropIndex("dbo.StudentSubjectGrades", new[] { "SubjectId" });
            DropTable("dbo.StudentSubjectGrades");
        }
    }
}
