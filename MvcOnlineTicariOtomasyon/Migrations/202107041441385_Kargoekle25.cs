namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kargoekle25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KargoDetays", "SatisId", c => c.Int(nullable: false));
            CreateIndex("dbo.KargoDetays", "SatisId");
            AddForeignKey("dbo.KargoDetays", "SatisId", "dbo.SatisHarekets", "SatisId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KargoDetays", "SatisId", "dbo.SatisHarekets");
            DropIndex("dbo.KargoDetays", new[] { "SatisId" });
            DropColumn("dbo.KargoDetays", "SatisId");
        }
    }
}
