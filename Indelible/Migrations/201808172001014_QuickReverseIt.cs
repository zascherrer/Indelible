namespace Indelible.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuickReverseIt : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ContractReceipts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ContractReceipts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
