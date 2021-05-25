namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ABC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Activation_code", c => c.String());
            AddColumn("dbo.Users", "Is_Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Is_Active");
            DropColumn("dbo.Users", "Activation_code");
        }
    }
}
