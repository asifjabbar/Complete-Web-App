namespace Banglabazaar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sliderimagetableadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SliderImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SliderImages");
        }
    }
}
