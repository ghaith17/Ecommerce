namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class saif : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Offers", "Item_Id", "dbo.Items");
            DropIndex("dbo.Items", new[] { "Order_Id" });
            DropIndex("dbo.Offers", new[] { "Item_Id" });
            CreateIndex("dbo.Items", "order_Id");
            DropColumn("dbo.Offers", "Item_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Offers", "Item_Id", c => c.String(maxLength: 128));
            DropIndex("dbo.Items", new[] { "order_Id" });
            CreateIndex("dbo.Offers", "Item_Id");
            CreateIndex("dbo.Items", "Order_Id");
            AddForeignKey("dbo.Offers", "Item_Id", "dbo.Items", "Item_Id");
        }
    }
}
