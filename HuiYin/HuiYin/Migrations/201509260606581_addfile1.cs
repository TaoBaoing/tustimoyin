namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfile1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingCarts", "LhUserId", "dbo.LhUsers");
            DropIndex("dbo.ShoppingCarts", new[] { "LhUserId" });
            AlterColumn("dbo.ShoppingCarts", "LhUserId", c => c.Long());
            CreateIndex("dbo.ShoppingCarts", "LhUserId");
            AddForeignKey("dbo.ShoppingCarts", "LhUserId", "dbo.LhUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCarts", "LhUserId", "dbo.LhUsers");
            DropIndex("dbo.ShoppingCarts", new[] { "LhUserId" });
            AlterColumn("dbo.ShoppingCarts", "LhUserId", c => c.Long(nullable: false));
            CreateIndex("dbo.ShoppingCarts", "LhUserId");
            AddForeignKey("dbo.ShoppingCarts", "LhUserId", "dbo.LhUsers", "Id", cascadeDelete: true);
        }
    }
}
