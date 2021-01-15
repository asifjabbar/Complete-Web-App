namespace Banglabazaar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedqtyinorderitem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Qauntity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "Qauntity");
        }
    }
}
