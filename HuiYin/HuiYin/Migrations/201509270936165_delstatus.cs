namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delstatus : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderDetails", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Status", c => c.Int(nullable: false));
        }
    }
}
