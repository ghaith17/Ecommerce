namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class runtio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Offer_Offer_id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Items", "Offer_Offer_id");
            AddForeignKey("dbo.Items", "Offer_Offer_id", "dbo.Offers", "Offer_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Offer_Offer_id", "dbo.Offers");
            DropIndex("dbo.Items", new[] { "Offer_Offer_id" });
            DropColumn("dbo.Items", "Offer_Offer_id");
        }
    }
}
