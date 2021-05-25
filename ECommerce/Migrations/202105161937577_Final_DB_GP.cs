namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Final_DB_GP : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Item_Id)
                .ForeignKey("dbo.Offers", t => t.offer_Offer_id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_Id)
                .Index(t => t.offer_Offer_id)
                .Index(t => t.Order_Id)
                .Index(t => t.ShoppingCart_Id);
            
            AlterColumn("dbo.ShoppingCarts", "Item_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ShoppingCarts", "Item_Id");
            AddForeignKey("dbo.ShoppingCarts", "Item_Id", "dbo.Items", "Item_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "ShoppingCart_Id", "dbo.ShoppingCarts");
            DropForeignKey("dbo.ShoppingCarts", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.Items", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Items", "offer_Offer_id", "dbo.Offers");
            DropIndex("dbo.ShoppingCarts", new[] { "Item_Id" });
            DropIndex("dbo.Items", new[] { "ShoppingCart_Id" });
            DropIndex("dbo.Items", new[] { "Order_Id" });
            DropIndex("dbo.Items", new[] { "offer_Offer_id" });
            AlterColumn("dbo.ShoppingCarts", "Item_Id", c => c.String());
            DropTable("dbo.Items");
        }
    }
}
