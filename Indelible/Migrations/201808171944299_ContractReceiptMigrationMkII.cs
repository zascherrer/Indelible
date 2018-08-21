namespace Indelible.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContractReceiptMigrationMkII : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ContractReceipts", "DocumentHash");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContractReceipts", "DocumentHash", c => c.String());
        }
    }
}
