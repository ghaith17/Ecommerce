namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class youe : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Orders", "Items", c => c.String(maxLength: 128));
            //CreateIndex("dbo.Orders", "Items");
            //DropForeignKey("dbo.Items", "Order_Id", "dbo.Orders");
            //DropIndex("dbo.Items", new[] { "Order_Id" });
            //DropColumn("dbo.Items", "Order_Id");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Items", "Order_Id", c => c.String(maxLength: 128));
            //CreateIndex("dbo.Items", "Order_Id");
            //AddForeignKey("dbo.Items", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
