namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delrequird : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LhUsers", "Address", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LhUsers", "Address", c => c.String(nullable: false));
        }
    }
}
