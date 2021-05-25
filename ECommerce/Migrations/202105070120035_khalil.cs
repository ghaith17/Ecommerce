namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class khalil : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Order_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Items", "Order_Id");
            AddForeignKey("dbo.Items", "Order_Id", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Items", new[] { "Order_Id" });
            DropColumn("dbo.Items", "Order_Id");
        }
    }
}
