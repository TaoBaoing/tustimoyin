namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sfd32 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LhUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Pwd = c.String(),
                        XingMing = c.String(),
                        Sex = c.String(),
                        Email = c.String(),
                        UserType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LhUserId = c.Long(nullable: false),
                        Name = c.String(),
                        Count = c.Int(nullable: false),
                        IsDanMian = c.Boolean(nullable: false),
                        PerCount = c.Int(nullable: false),
                        TotalPage = c.Int(nullable: false),
                        ShiJiPage = c.Int(nullable: false),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LhUsers", t => t.LhUserId, cascadeDelete: true)
                .Index(t => t.LhUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCarts", "LhUserId", "dbo.LhUsers");
            DropIndex("dbo.ShoppingCarts", new[] { "LhUserId" });
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.LhUsers");
        }
    }
}
