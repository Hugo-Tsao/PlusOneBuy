namespace FBPlusOneBuy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false),
                        CustomerName = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.Int(),
                        Email = c.String(maxLength: 100),
                        Photo = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        Keyword = c.String(nullable: false, maxLength: 50),
                        OrderDateTime = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .ForeignKey("dbo.Customers", t => t.CustomerID)
                .Index(t => t.ProductID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 50),
                        UnitPrice = c.Decimal(nullable: false, storeType: "money"),
                        Stock = c.Int(nullable: false),
                        ProductImage = c.String(maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Orders", "ProductID", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropIndex("dbo.Orders", new[] { "ProductID" });
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
