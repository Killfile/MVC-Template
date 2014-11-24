namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Membership : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EFMembership",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        UserName = c.String(),
                        HashedPassword = c.String(),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.EFRoll",
                c => new
                    {
                        RollID = c.Guid(nullable: false),
                        Label = c.String(),
                    })
                .PrimaryKey(t => t.RollID);
            
            CreateTable(
                "dbo.EFPermission",
                c => new
                    {
                        PermissionID = c.Guid(nullable: false),
                        Label = c.String(),
                    })
                .PrimaryKey(t => t.PermissionID);
            
            CreateTable(
                "dbo.EFRollPermissions",
                c => new
                    {
                        RollID = c.Guid(nullable: false),
                        PermissionID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RollID, t.PermissionID })
                .ForeignKey("dbo.EFRoll", t => t.RollID, cascadeDelete: true)
                .ForeignKey("dbo.EFPermission", t => t.PermissionID, cascadeDelete: true)
                .Index(t => t.RollID)
                .Index(t => t.PermissionID);
            
            CreateTable(
                "dbo.EFMembershipRolls",
                c => new
                    {
                        UserID = c.Guid(nullable: false),
                        RollID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.RollID })
                .ForeignKey("dbo.EFMembership", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.EFRoll", t => t.RollID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RollID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EFMembershipRolls", "RollID", "dbo.EFRoll");
            DropForeignKey("dbo.EFMembershipRolls", "UserID", "dbo.EFMembership");
            DropForeignKey("dbo.EFRollPermissions", "PermissionID", "dbo.EFPermission");
            DropForeignKey("dbo.EFRollPermissions", "RollID", "dbo.EFRoll");
            DropIndex("dbo.EFMembershipRolls", new[] { "RollID" });
            DropIndex("dbo.EFMembershipRolls", new[] { "UserID" });
            DropIndex("dbo.EFRollPermissions", new[] { "PermissionID" });
            DropIndex("dbo.EFRollPermissions", new[] { "RollID" });
            DropTable("dbo.EFMembershipRolls");
            DropTable("dbo.EFRollPermissions");
            DropTable("dbo.EFPermission");
            DropTable("dbo.EFRoll");
            DropTable("dbo.EFMembership");
        }
    }
}
