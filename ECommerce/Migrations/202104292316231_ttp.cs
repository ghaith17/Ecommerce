namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ttp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Item_Id", "dbo.Offers");
            DropIndex("dbo.Items", new[] { "Item_Id" });
           // AddColumn("dbo.Items", "Offer_Offer_id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Offers", "Item_Id", c => c.String(maxLength: 128));
        //    CreateIndex("dbo.Items", "Offer_Offer_id");
            CreateIndex("dbo.Offers", "Item_Id");
            AddForeignKey("dbo.Offers", "Item_Id", "dbo.Items", "Item_Id");
        //    AddForeignKey("dbo.Items", "Offer_Offer_id", "dbo.Offers", "Offer_id");
        }
        
        public override void Down()
        {
         //   DropForeignKey("dbo.Items", "Offer_Offer_id", "dbo.Offers");
            DropForeignKey("dbo.Offers", "Item_Id", "dbo.Items");
            DropIndex("dbo.Offers", new[] { "Item_Id" });
         //   DropIndex("dbo.Items", new[] { "Offer_Offer_id" });
            AlterColumn("dbo.Offers", "Item_Id", c => c.String());
            DropColumn("dbo.Items", "Offer_Offer_id");
            CreateIndex("dbo.Items", "Item_Id");
            AddForeignKey("dbo.Items", "Item_Id", "dbo.Offers", "Offer_id");
        }
    }
}
