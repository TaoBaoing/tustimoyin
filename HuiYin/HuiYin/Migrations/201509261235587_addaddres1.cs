namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addaddres1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LhUsers", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LhUsers", "Address");
        }
    }
}
