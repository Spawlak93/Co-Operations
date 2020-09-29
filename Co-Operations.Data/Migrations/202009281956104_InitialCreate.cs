namespace Co_Operations.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FundsOnHand = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalesCommisionPercent = c.Double(nullable: false),
                        LocationCommisionPercent = c.Double(nullable: false),
                        SalesTaxPercent = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LocationUser",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.LocationID })
                .ForeignKey("dbo.Location", t => t.LocationID, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.LocationID);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        FundsEarned = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FundsPayedOut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductSKU = c.String(nullable: false, maxLength: 128),
                        ItemName = c.String(nullable: false),
                        Details = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MakerID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductSKU)
                .ForeignKey("dbo.ApplicationUser", t => t.MakerID)
                .Index(t => t.MakerID);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionProduct",
                c => new
                    {
                        TransactionId = c.Int(nullable: false),
                        PruductSKU = c.String(nullable: false, maxLength: 128),
                        NumberSold = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TransactionId, t.PruductSKU })
                .ForeignKey("dbo.Product", t => t.PruductSKU, cascadeDelete: true)
                .ForeignKey("dbo.Transaction", t => t.TransactionId, cascadeDelete: true)
                .Index(t => t.TransactionId)
                .Index(t => t.PruductSKU);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SellerID = c.String(maxLength: 128),
                        LocationID = c.Int(nullable: false),
                        DateOfSale = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Location", t => t.LocationID, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUser", t => t.SellerID)
                .Index(t => t.SellerID)
                .Index(t => t.LocationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionProduct", "TransactionId", "dbo.Transaction");
            DropForeignKey("dbo.Transaction", "SellerID", "dbo.ApplicationUser");
            DropForeignKey("dbo.Transaction", "LocationID", "dbo.Location");
            DropForeignKey("dbo.TransactionProduct", "PruductSKU", "dbo.Product");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Product", "MakerID", "dbo.ApplicationUser");
            DropForeignKey("dbo.LocationUser", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.LocationUser", "LocationID", "dbo.Location");
            DropIndex("dbo.Transaction", new[] { "LocationID" });
            DropIndex("dbo.Transaction", new[] { "SellerID" });
            DropIndex("dbo.TransactionProduct", new[] { "PruductSKU" });
            DropIndex("dbo.TransactionProduct", new[] { "TransactionId" });
            DropIndex("dbo.Product", new[] { "MakerID" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.LocationUser", new[] { "LocationID" });
            DropIndex("dbo.LocationUser", new[] { "UserId" });
            DropTable("dbo.Transaction");
            DropTable("dbo.TransactionProduct");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Product");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.LocationUser");
            DropTable("dbo.Location");
        }
    }
}
