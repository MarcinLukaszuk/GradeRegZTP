namespace GradeRegZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tmp2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyUsers", "StudentsGroupId", c => c.Int());
            DropColumn("dbo.MyUsers", "StrudentsGroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MyUsers", "StrudentsGroupId", c => c.Int());
            DropColumn("dbo.MyUsers", "StudentsGroupId");
        }
    }
}
