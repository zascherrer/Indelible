namespace Indelible.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileExtensionAndEmailMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "FileExtension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "FileExtension");
        }
    }
}
