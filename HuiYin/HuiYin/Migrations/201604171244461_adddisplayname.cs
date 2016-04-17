namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddisplayname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCarts", "DisplayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCarts", "DisplayName");
        }
    }
}
