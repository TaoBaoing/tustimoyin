namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addhoutai2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LhUsers", "XingMing", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LhUsers", "XingMing", c => c.String());
        }
    }
}
