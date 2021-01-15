namespace Banglabazaar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usernameadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "UserName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Orders", "Address", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Address");
            DropColumn("dbo.Orders", "UserName");
        }
    }
}
