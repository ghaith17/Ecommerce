namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tio : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Item_Id", "dbo.Offers");
            DropIndex("dbo.Items", new[] { "Item_Id" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Items", "Item_Id");
            AddForeignKey("dbo.Items", "Item_Id", "dbo.Offers", "Offer_id");
        }
    }
}
