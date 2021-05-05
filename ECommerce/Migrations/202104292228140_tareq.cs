namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tareq : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.Offers", "Item_Id");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Offers", "Item_Id", c => c.String());
        }
    }
}
