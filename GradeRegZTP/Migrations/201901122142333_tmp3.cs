namespace GradeRegZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tmp3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubjectStudentGroups", "TeacherID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubjectStudentGroups", "TeacherID");
        }
    }
}
