namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UploadFiles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(),
                        FilePathName = c.String(),
                        LhUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LhUsers", t => t.LhUserId)
                .Index(t => t.LhUserId);
            
            AddColumn("dbo.ShoppingCarts", "IsCaiDa", c => c.Boolean(nullable: false));
            AddColumn("dbo.ShoppingCarts", "PrintSize", c => c.Int(nullable: false));
            AddColumn("dbo.ShoppingCarts", "BaoZhuang", c => c.Int(nullable: false));
            AddColumn("dbo.ShoppingCarts", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.ShoppingCarts", "PerCount");
            DropColumn("dbo.ShoppingCarts", "ShiJiPage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCarts", "ShiJiPage", c => c.Int(nullable: false));
            AddColumn("dbo.ShoppingCarts", "PerCount", c => c.Int(nullable: false));
            DropForeignKey("dbo.UploadFiles", "LhUserId", "dbo.LhUsers");
            DropIndex("dbo.UploadFiles", new[] { "LhUserId" });
            DropColumn("dbo.ShoppingCarts", "Status");
            DropColumn("dbo.ShoppingCarts", "BaoZhuang");
            DropColumn("dbo.ShoppingCarts", "PrintSize");
            DropColumn("dbo.ShoppingCarts", "IsCaiDa");
            DropTable("dbo.UploadFiles");
        }
    }
}
