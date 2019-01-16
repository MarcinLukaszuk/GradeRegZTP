namespace GradeRegZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
            
            CreateTable(
                "dbo.HourOfDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HourId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        StudentsGroupId = c.Int(nullable: false),
                        DayOfWeekId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DayOfWeeks", t => t.DayOfWeekId, cascadeDelete: false)
                .ForeignKey("dbo.Hours", t => t.HourId, cascadeDelete: false)
                .ForeignKey("dbo.StudentsGroups", t => t.StudentsGroupId, cascadeDelete: false)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .Index(t => t.HourId)
                .Index(t => t.SubjectId)
                .Index(t => t.StudentsGroupId)
                .Index(t => t.DayOfWeekId);
            
            CreateTable(
                "dbo.Hours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HourString = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentsGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Note = c.String(),
                        Date = c.DateTime(),
                        Owner = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        MessageText = c.String(),
                        Time = c.DateTime(nullable: false),
                        SenderID = c.String(),
                        RecieverID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MyUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        BirthDate = c.DateTime(),
                        PhoneNumber = c.String(),
                        StudentsGroupId = c.Int(),
                        Owner = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SubjectStudentGroupTeachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        StudentsGroupId = c.Int(nullable: false),
                        TeacherID = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentsGroups", t => t.StudentsGroupId, cascadeDelete: false)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .Index(t => t.SubjectId)
                .Index(t => t.StudentsGroupId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubjectStudentGroupTeachers", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectStudentGroupTeachers", "StudentsGroupId", "dbo.StudentsGroups");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Grades", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.HourOfDays", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.HourOfDays", "StudentsGroupId", "dbo.StudentsGroups");
            DropForeignKey("dbo.HourOfDays", "HourId", "dbo.Hours");
            DropForeignKey("dbo.HourOfDays", "DayOfWeekId", "dbo.DayOfWeeks");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SubjectStudentGroupTeachers", new[] { "StudentsGroupId" });
            DropIndex("dbo.SubjectStudentGroupTeachers", new[] { "SubjectId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Grades", new[] { "SubjectId" });
            DropIndex("dbo.HourOfDays", new[] { "DayOfWeekId" });
            DropIndex("dbo.HourOfDays", new[] { "StudentsGroupId" });
            DropIndex("dbo.HourOfDays", new[] { "SubjectId" });
            DropIndex("dbo.HourOfDays", new[] { "HourId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SubjectStudentGroupTeachers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MyUsers");
            DropTable("dbo.Messages");
            DropTable("dbo.Grades");
            DropTable("dbo.Subjects");
            DropTable("dbo.StudentsGroups");
            DropTable("dbo.Hours");
            DropTable("dbo.HourOfDays");
            DropTable("dbo.DayOfWeeks");
        }
    }
}
