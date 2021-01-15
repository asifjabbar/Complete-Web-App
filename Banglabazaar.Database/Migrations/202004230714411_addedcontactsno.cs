namespace Banglabazaar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcontactsno : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Contact", c => c.String(nullable: false));
        }
    }
}
