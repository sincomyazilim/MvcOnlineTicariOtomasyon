namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ekle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Caris", "VergiNo1", c => c.Int(nullable: false));
            AddColumn("dbo.Caris", "Adres1", c => c.String(maxLength: 150, unicode: false));
           
        }
        
        public override void Down()
        {
            
            DropColumn("dbo.Caris", "Adres1");
            DropColumn("dbo.Caris", "VergiNo1");
        }
    }
}
