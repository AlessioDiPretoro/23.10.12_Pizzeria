namespace InForno.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetailsOrders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idOrder = c.Int(nullable: false),
                        idProduct = c.Int(nullable: false),
                        quatity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Orders", t => t.idOrder)
                .ForeignKey("dbo.Products", t => t.idProduct)
                .Index(t => t.idOrder)
                .Index(t => t.idProduct);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idBuyer = c.Int(nullable: false),
                        idDetailsOrder = c.Int(nullable: false),
                        address = c.String(nullable: false, maxLength: 200),
                        notes = c.String(maxLength: 200),
                        processed = c.Boolean(nullable: false),
                        data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.idBuyer)
                .Index(t => t.idBuyer);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false, maxLength: 50),
                        password = c.String(nullable: false, maxLength: 50),
                        role = c.String(maxLength: 50),
                        nome = c.String(maxLength: 50),
                        cognome = c.String(maxLength: 50),
                        email = c.String(maxLength: 50),
                        phone = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        photo = c.String(nullable: false, maxLength: 200),
                        price = c.Decimal(nullable: false, storeType: "money"),
                        deliveryTime = c.Time(nullable: false, precision: 7),
                        ingredients = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Products_Ingredients",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idProduct = c.Int(nullable: false),
                        idIngredient = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Ingredients", t => t.idIngredient)
                .ForeignKey("dbo.Products", t => t.idProduct)
                .Index(t => t.idProduct)
                .Index(t => t.idIngredient);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products_Ingredients", "idProduct", "dbo.Products");
            DropForeignKey("dbo.Products_Ingredients", "idIngredient", "dbo.Ingredients");
            DropForeignKey("dbo.DetailsOrders", "idProduct", "dbo.Products");
            DropForeignKey("dbo.Orders", "idBuyer", "dbo.Users");
            DropForeignKey("dbo.DetailsOrders", "idOrder", "dbo.Orders");
            DropIndex("dbo.Products_Ingredients", new[] { "idIngredient" });
            DropIndex("dbo.Products_Ingredients", new[] { "idProduct" });
            DropIndex("dbo.Orders", new[] { "idBuyer" });
            DropIndex("dbo.DetailsOrders", new[] { "idProduct" });
            DropIndex("dbo.DetailsOrders", new[] { "idOrder" });
            DropTable("dbo.Ingredients");
            DropTable("dbo.Products_Ingredients");
            DropTable("dbo.Products");
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.DetailsOrders");
        }
    }
}
