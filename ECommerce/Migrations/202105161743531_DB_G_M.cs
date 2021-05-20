namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB_G_M : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Items", "Order_Id");
            AlterColumn("dbo.Users", "PhoneNum", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PhoneNum", c => c.String(nullable: false));
        }
    }
}
