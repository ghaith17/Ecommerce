namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GP_DB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offers", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offers", "Status");
        }
    }
}
