namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfenlei3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.WenKuFenLeis", new[] { "ParentId" });
            AddColumn("dbo.WenKuFenLeis", "FenLeiType", c => c.Int(nullable: false));
            AlterColumn("dbo.WenKuFenLeis", "ParentId", c => c.Long());
            CreateIndex("dbo.WenKuFenLeis", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.WenKuFenLeis", new[] { "ParentId" });
            AlterColumn("dbo.WenKuFenLeis", "ParentId", c => c.Long(nullable: false));
            DropColumn("dbo.WenKuFenLeis", "FenLeiType");
            CreateIndex("dbo.WenKuFenLeis", "ParentId");
        }
    }
}
