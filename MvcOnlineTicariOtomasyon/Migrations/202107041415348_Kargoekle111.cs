namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kargoekle111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KargoDetays", "PersonelId", c => c.Int(nullable: false));
            AddColumn("dbo.KargoDetays", "CariId", c => c.Int(nullable: false));
            CreateIndex("dbo.KargoDetays", "PersonelId");
            CreateIndex("dbo.KargoDetays", "CariId");
            AddForeignKey("dbo.KargoDetays", "CariId", "dbo.Caris", "CariId", cascadeDelete: true);
            AddForeignKey("dbo.KargoDetays", "PersonelId", "dbo.Personels", "PersonelId", cascadeDelete: true);
            DropColumn("dbo.KargoDetays", "Personel");
            DropColumn("dbo.KargoDetays", "Alıcı");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KargoDetays", "Alıcı", c => c.String(maxLength: 30, unicode: false));
            AddColumn("dbo.KargoDetays", "Personel", c => c.String(maxLength: 20, unicode: false));
            DropForeignKey("dbo.KargoDetays", "PersonelId", "dbo.Personels");
            DropForeignKey("dbo.KargoDetays", "CariId", "dbo.Caris");
            DropIndex("dbo.KargoDetays", new[] { "CariId" });
            DropIndex("dbo.KargoDetays", new[] { "PersonelId" });
            DropColumn("dbo.KargoDetays", "CariId");
            DropColumn("dbo.KargoDetays", "PersonelId");
        }
    }
}
