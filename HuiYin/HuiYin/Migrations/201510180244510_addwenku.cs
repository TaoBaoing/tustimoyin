namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addwenku : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WenKus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        FileName = c.String(),
                        WenKuFenLeiId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WenKuFenLeis", t => t.WenKuFenLeiId, cascadeDelete: true)
                .Index(t => t.WenKuFenLeiId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WenKus", "WenKuFenLeiId", "dbo.WenKuFenLeis");
            DropIndex("dbo.WenKus", new[] { "WenKuFenLeiId" });
            DropTable("dbo.WenKus");
        }
    }
}
