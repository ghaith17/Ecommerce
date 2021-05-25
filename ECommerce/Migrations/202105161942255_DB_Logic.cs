namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB_Logic : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "offer_Offer_id", "dbo.Offers");
            DropForeignKey("dbo.Items", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.ShoppingCarts", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.Items", "ShoppingCart_Id", "dbo.ShoppingCarts");
            DropIndex("dbo.Items", new[] { "offer_Offer_id" });
            DropIndex("dbo.Items", new[] { "Order_Id" });
            DropIndex("dbo.Items", new[] { "ShoppingCart_Id" });
            DropIndex("dbo.ShoppingCarts", new[] { "Item_Id" });
            AlterColumn("dbo.ShoppingCarts", "Item_Id", c => c.String());
            DropTable("dbo.Items");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.Items",
                c => new
                {
                    Item_Id = c.String(nullable: false, maxLength: 128),
                    Quantity = c.Int(nullable: false),
                    Name = c.String(),
                    Description = c.String(),
                    Price = c.Double(nullable: false),
                    DefualtPrice = c.Double(nullable: false),
                    SelectedItem = c.Boolean(nullable: false),
                    offer_Offer_id = c.String(maxLength: 128),
                    Order_Id = c.String(maxLength: 128),
                    ShoppingCart_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Item_Id);

            AlterColumn("dbo.ShoppingCarts", "Item_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ShoppingCarts", "Item_Id");
            CreateIndex("dbo.Items", "ShoppingCart_Id");
            CreateIndex("dbo.Items", "Order_Id");
            CreateIndex("dbo.Items", "offer_Offer_id");
            AddForeignKey("dbo.Items", "ShoppingCart_Id", "dbo.ShoppingCarts", "Id");
            AddForeignKey("dbo.ShoppingCarts", "Item_Id", "dbo.Items", "Item_Id");
            AddForeignKey("dbo.Items", "Order_Id", "dbo.Orders", "Id");
            AddForeignKey("dbo.Items", "offer_Offer_id", "dbo.Offers", "Offer_id");
        }
    }
}
