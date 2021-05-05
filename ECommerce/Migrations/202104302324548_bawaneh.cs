namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bawaneh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCarts", "Item_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ShoppingCarts", "Item_Id");
            AddForeignKey("dbo.ShoppingCarts", "Item_Id", "dbo.Items", "Item_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCarts", "Item_Id", "dbo.Items");
            DropIndex("dbo.ShoppingCarts", new[] { "Item_Id" });
            DropColumn("dbo.ShoppingCarts", "Item_Id");
        }
    }
}
