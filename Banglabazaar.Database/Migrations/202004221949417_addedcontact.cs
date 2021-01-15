namespace Banglabazaar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcontact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Contact");
        }
    }
}
