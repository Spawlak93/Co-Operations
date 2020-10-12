namespace Co_Operations.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCollectedCommissionsClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CollectedCommission",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AmountCollected = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateOfCollection = c.DateTimeOffset(nullable: false, precision: 7),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId)
                .Index(t => t.UserId);
            
            DropColumn("dbo.ApplicationUser", "FundsPayedOut");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUser", "FundsPayedOut", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.CollectedCommission", "UserId", "dbo.ApplicationUser");
            DropIndex("dbo.CollectedCommission", new[] { "UserId" });
            DropTable("dbo.CollectedCommission");
        }
    }
}
