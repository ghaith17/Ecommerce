namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalM : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Items", new[] { "Offer_Offer_id" });
            CreateIndex("dbo.Items", "offer_Offer_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Items", new[] { "offer_Offer_id" });
            CreateIndex("dbo.Items", "Offer_Offer_id");
        }
    }
}
