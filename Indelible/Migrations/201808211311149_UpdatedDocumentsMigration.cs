namespace Indelible.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDocumentsMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "ContractAddress", c => c.String());
            AddColumn("dbo.Documents", "TimeStamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "IsPublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "IsPublic");
            DropColumn("dbo.Documents", "TimeStamp");
            DropColumn("dbo.Documents", "ContractAddress");
        }
    }
}
