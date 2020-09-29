namespace Co_Operations.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLocationName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Location", "LocationName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Location", "LocationName");
        }
    }
}
