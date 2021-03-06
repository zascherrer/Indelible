namespace Indelible.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImAnIdiotButThisWillWork : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractReceipts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocumentHash = c.String(),
                        ContractAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContractReceipts");
        }
    }
}
