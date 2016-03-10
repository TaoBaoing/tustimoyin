namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addwenkutime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WenKus", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WenKus", "CreateTime");
        }
    }
}
