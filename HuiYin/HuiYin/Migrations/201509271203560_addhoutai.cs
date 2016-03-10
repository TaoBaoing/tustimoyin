namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addhoutai : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LhUsers", "UserType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LhUsers", "UserType", c => c.String());
        }
    }
}
