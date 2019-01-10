namespace GradeRegZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pep : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "MyUsers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MyUsers", newName: "Users");
        }
    }
}
