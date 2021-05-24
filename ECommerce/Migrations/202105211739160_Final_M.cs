namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Final_M : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UserNamee", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UserNamee", c => c.String(nullable: false));
        }
    }
}
