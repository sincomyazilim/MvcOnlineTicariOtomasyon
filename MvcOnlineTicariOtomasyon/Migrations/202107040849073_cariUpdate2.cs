namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cariUpdate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Caris", "VergiNo", c => c.String(maxLength: 10, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Caris", "VergiNo", c => c.String(maxLength: 11, unicode: false));
        }
    }
}
