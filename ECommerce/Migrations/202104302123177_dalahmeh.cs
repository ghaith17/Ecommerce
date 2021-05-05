namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dalahmeh : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.VirtualWallets");
            AddColumn("dbo.Users", "VirtualWallet_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Orders", "Bill_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.VirtualWallets", "VirtualWallet_Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.VirtualWallets", "VirtualWallet_Id");
            CreateIndex("dbo.Users", "VirtualWallet_Id");
            CreateIndex("dbo.Orders", "Bill_Id");
            AddForeignKey("dbo.Orders", "Bill_Id", "dbo.Bills", "Id");
            AddForeignKey("dbo.Users", "VirtualWallet_Id", "dbo.VirtualWallets", "VirtualWallet_Id");
            DropColumn("dbo.VirtualWallets", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VirtualWallets", "Id", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Users", "VirtualWallet_Id", "dbo.VirtualWallets");
            DropForeignKey("dbo.Orders", "Bill_Id", "dbo.Bills");
            DropIndex("dbo.Orders", new[] { "Bill_Id" });
            DropIndex("dbo.Users", new[] { "VirtualWallet_Id" });
            DropPrimaryKey("dbo.VirtualWallets");
            DropColumn("dbo.VirtualWallets", "VirtualWallet_Id");
            DropColumn("dbo.Orders", "Bill_Id");
            DropColumn("dbo.Users", "VirtualWallet_Id");
            AddPrimaryKey("dbo.VirtualWallets", "Id");
        }
    }
}
