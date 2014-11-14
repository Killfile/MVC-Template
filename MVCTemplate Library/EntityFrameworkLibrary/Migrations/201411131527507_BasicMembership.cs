namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicMembership : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EFMembership");
        }
    }
}
