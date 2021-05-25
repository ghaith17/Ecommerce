namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Virtual_Store_DB : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Items", new[] { "order_Id" });
            CreateIndex("dbo.Items", "Order_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Items", new[] { "Order_Id" });
            CreateIndex("dbo.Items", "order_Id");
        }
    }
}
