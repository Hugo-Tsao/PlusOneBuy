namespace FBPlusOneBuy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ShopID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ShopID");
        }
    }
}
