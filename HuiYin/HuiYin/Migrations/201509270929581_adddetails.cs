namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "LhUserId", c => c.Long());
            AddColumn("dbo.OrderDetails", "Name", c => c.String());
            AddColumn("dbo.OrderDetails", "IsDanMian", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderDetails", "IsCaiDa", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderDetails", "PrintSize", c => c.Int(nullable: false));
            AddColumn("dbo.OrderDetails", "BaoZhuang", c => c.Int(nullable: false));
            AddColumn("dbo.OrderDetails", "TotalPage", c => c.Int(nullable: false));
            AddColumn("dbo.OrderDetails", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.ShoppingCarts", "UploadFileId", c => c.Long(nullable: false));
            CreateIndex("dbo.ShoppingCarts", "UploadFileId");
            AddForeignKey("dbo.ShoppingCarts", "UploadFileId", "dbo.UploadFiles", "Id", cascadeDelete: true);
            DropColumn("dbo.OrderDetails", "PageCount");
            DropColumn("dbo.ShoppingCarts", "TempName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCarts", "TempName", c => c.String());
            AddColumn("dbo.OrderDetails", "PageCount", c => c.Int());
            DropForeignKey("dbo.ShoppingCarts", "UploadFileId", "dbo.UploadFiles");
            DropIndex("dbo.ShoppingCarts", new[] { "UploadFileId" });
            DropColumn("dbo.ShoppingCarts", "UploadFileId");
            DropColumn("dbo.OrderDetails", "Status");
            DropColumn("dbo.OrderDetails", "TotalPage");
            DropColumn("dbo.OrderDetails", "BaoZhuang");
            DropColumn("dbo.OrderDetails", "PrintSize");
            DropColumn("dbo.OrderDetails", "IsCaiDa");
            DropColumn("dbo.OrderDetails", "IsDanMian");
            DropColumn("dbo.OrderDetails", "Name");
            DropColumn("dbo.OrderDetails", "LhUserId");
        }
    }
}
