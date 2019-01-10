namespace GradeRegZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyUsers", "StrudentsGroupId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MyUsers", "StrudentsGroupId");
        }
    }
}
