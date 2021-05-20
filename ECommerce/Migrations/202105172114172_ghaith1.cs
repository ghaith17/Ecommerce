namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ghaith1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Users", name: "VirtualWallet_Id", newName: "virtualWallet_VirtualWallet_Id");
            RenameIndex(table: "dbo.Users", name: "IX_VirtualWallet_Id", newName: "IX_virtualWallet_VirtualWallet_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Users", name: "IX_virtualWallet_VirtualWallet_Id", newName: "IX_VirtualWallet_Id");
            RenameColumn(table: "dbo.Users", name: "virtualWallet_VirtualWallet_Id", newName: "VirtualWallet_Id");
        }
    }
}
