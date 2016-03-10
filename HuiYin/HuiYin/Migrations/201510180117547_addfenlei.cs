namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfenlei : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WenKuFenLeis",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ParentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WenKuFenLeis");
        }
    }
}
