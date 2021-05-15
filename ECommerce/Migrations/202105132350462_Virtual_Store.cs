namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Virtual_Store : DbMigration
    {
        public override void Up()
        {
            
            DropColumn("dbo.Items", "Order_Id");
        }
        
        public override void Down()
        {
            
        }
    }
}
