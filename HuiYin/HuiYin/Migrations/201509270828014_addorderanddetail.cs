namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addorderanddetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrderId = c.Long(nullable: false),
                        UploadFileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.UploadFiles", t => t.UploadFileId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.UploadFileId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LhUserId = c.Long(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        OrderStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LhUsers", t => t.LhUserId, cascadeDelete: true)
                .Index(t => t.LhUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "UploadFileId", "dbo.UploadFiles");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "LhUserId", "dbo.LhUsers");
            DropIndex("dbo.Orders", new[] { "LhUserId" });
            DropIndex("dbo.OrderDetails", new[] { "UploadFileId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
    }
}
