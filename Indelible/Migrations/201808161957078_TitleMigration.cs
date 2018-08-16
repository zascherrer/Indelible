namespace Indelible.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TitleMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "Title");
        }
    }
}
