namespace Co_Operations.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProductClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Product", "Details");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Details", c => c.String(nullable: false));
            DropColumn("dbo.Product", "Description");
        }
    }
}
