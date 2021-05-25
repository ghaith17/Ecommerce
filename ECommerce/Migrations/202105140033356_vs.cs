namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Items", new[] { "Order_Id" });
            DropColumn("dbo.Items", "Order_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Items", new[] { "Order_Id" });
            DropColumn("dbo.Items", "Order_Id");
        }
    }
}
