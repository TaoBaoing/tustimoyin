namespace HuiYin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfieldswenkuid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UploadFiles", "WenKuId", c => c.Long());
            CreateIndex("dbo.UploadFiles", "WenKuId");
            AddForeignKey("dbo.UploadFiles", "WenKuId", "dbo.WenKus", "Id");
            DropColumn("dbo.LhUsers", "Sex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LhUsers", "Sex", c => c.String());
            DropForeignKey("dbo.UploadFiles", "WenKuId", "dbo.WenKus");
            DropIndex("dbo.UploadFiles", new[] { "WenKuId" });
            DropColumn("dbo.UploadFiles", "WenKuId");
        }
    }
}
