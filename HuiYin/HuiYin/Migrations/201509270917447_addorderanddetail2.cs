namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addorderanddetail2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Money", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderDetails", "PageCount", c => c.Int());
            AddColumn("dbo.OrderDetails", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Money", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Money");
            DropColumn("dbo.OrderDetails", "Count");
            DropColumn("dbo.OrderDetails", "PageCount");
            DropColumn("dbo.OrderDetails", "Money");
        }
    }
}
