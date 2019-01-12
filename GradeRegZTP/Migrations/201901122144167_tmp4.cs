namespace GradeRegZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tmp4 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SubjectStudentGroups", newName: "SubjectStudentGroupTeachers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.SubjectStudentGroupTeachers", newName: "SubjectStudentGroups");
        }
    }
}
