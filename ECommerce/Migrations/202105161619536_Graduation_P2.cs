namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Graduation_P2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "PhoneNum");
            DropColumn("dbo.Users", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Users", "PhoneNum", c => c.String());
        }
    }
}
