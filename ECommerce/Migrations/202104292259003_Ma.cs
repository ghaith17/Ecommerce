namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ma : DbMigration
    {
        public override void Up()
        {
           
            
            //DropPrimaryKey("dbo.Items");
            AddColumn("dbo.Offers", "Item_Id", c => c.String());
            AlterColumn("dbo.Items", "Item_Id", c => c.String(nullable: false, maxLength: 128));
          //  AddPrimaryKey("dbo.Items", "Item_Id");
            CreateIndex("dbo.Items", "Item_Id");
        }
        
        public override void Down()
        {
           
           // DropPrimaryKey("dbo.Items");
            AlterColumn("dbo.Items", "Item_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Offers", "Item_Id");
            AddPrimaryKey("dbo.Items", "Item_Id");
            RenameColumn(table: "dbo.Items", name: "Item_Id", newName: "Offer_Offer_id");
            AddColumn("dbo.Items", "Item_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Items", "Offer_Offer_id");
        }
    }
}
