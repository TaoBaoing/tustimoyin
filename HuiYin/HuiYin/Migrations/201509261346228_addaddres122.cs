namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addaddres122 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCarts", "TempName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCarts", "TempName");
        }
    }
}
