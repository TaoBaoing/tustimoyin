namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfenlei2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.WenKuFenLeis", "ParentId");
            AddForeignKey("dbo.WenKuFenLeis", "ParentId", "dbo.WenKuFenLeis", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WenKuFenLeis", "ParentId", "dbo.WenKuFenLeis");
            DropIndex("dbo.WenKuFenLeis", new[] { "ParentId" });
        }
    }
}
