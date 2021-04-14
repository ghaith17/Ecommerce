namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "ShoppingCart_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Items", "ShoppingCart_Id");
            AddForeignKey("dbo.Items", "ShoppingCart_Id", "dbo.ShoppingCarts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "ShoppingCart_Id", "dbo.ShoppingCarts");
            DropIndex("dbo.Items", new[] { "ShoppingCart_Id" });
            DropColumn("dbo.Items", "ShoppingCart_Id");
        }
    }
}
