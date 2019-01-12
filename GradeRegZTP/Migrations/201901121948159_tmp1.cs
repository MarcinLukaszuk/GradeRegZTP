namespace GradeRegZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tmp1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.StrudentsGroups", newName: "StudentsGroups");
            DropForeignKey("dbo.StudentSubjectGrades", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.StudentSubjectGrades", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.StudentSubjectGrades", new[] { "SubjectId" });
            DropIndex("dbo.StudentSubjectGrades", new[] { "GradeId" });
            CreateTable(
                "dbo.SubjectStudentGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        StudentsGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentsGroups", t => t.StudentsGroupId, cascadeDelete: false)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .Index(t => t.SubjectId)
                .Index(t => t.StudentsGroupId);
            
            DropTable("dbo.StudentSubjectGrades");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.SubjectStudentGroups", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectStudentGroups", "StudentsGroupId", "dbo.StudentsGroups");
            DropIndex("dbo.SubjectStudentGroups", new[] { "StudentsGroupId" });
            DropIndex("dbo.SubjectStudentGroups", new[] { "SubjectId" });
            DropTable("dbo.SubjectStudentGroups");
            CreateIndex("dbo.StudentSubjectGrades", "GradeId");
            CreateIndex("dbo.StudentSubjectGrades", "SubjectId");
            AddForeignKey("dbo.StudentSubjectGrades", "SubjectId", "dbo.Subjects", "Id", cascadeDelete: false);
            AddForeignKey("dbo.StudentSubjectGrades", "GradeId", "dbo.Grades", "Id", cascadeDelete: false);
            RenameTable(name: "dbo.StudentsGroups", newName: "StrudentsGroups");
        }
    }
}
