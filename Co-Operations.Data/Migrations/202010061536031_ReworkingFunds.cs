namespace Co_Operations.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReworkingFunds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Location", "Expenses", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Location", "FundsOnHand");
            DropColumn("dbo.ApplicationUser", "FundsEarned");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUser", "FundsEarned", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Location", "FundsOnHand", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Location", "Expenses");
        }
    }
}
