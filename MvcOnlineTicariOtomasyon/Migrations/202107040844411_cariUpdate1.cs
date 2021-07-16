namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cariUpdate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Caris", "VergiNo1", c => c.String(maxLength: 11, unicode: false));
            AlterColumn("dbo.Caris", "TcNo", c => c.String(maxLength: 11, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Caris", "TcNo", c => c.Int(nullable: false));
            AlterColumn("dbo.Caris", "VergiNo1", c => c.Int(nullable: false));
        }
    }
}
