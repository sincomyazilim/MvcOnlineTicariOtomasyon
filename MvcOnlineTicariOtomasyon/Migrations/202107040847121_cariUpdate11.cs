namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cariUpdate11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Caris", "VergiNo", c => c.String(maxLength: 11, unicode: false));
            AddColumn("dbo.Caris", "Adres", c => c.String(maxLength: 160, unicode: false));
            DropColumn("dbo.Caris", "VergiNo1");
            DropColumn("dbo.Caris", "Adres1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Caris", "Adres1", c => c.String(maxLength: 150, unicode: false));
            AddColumn("dbo.Caris", "VergiNo1", c => c.String(maxLength: 11, unicode: false));
            DropColumn("dbo.Caris", "Adres");
            DropColumn("dbo.Caris", "VergiNo");
        }
    }
}
