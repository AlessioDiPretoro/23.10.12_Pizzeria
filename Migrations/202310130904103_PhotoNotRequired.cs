namespace InForno.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "photo", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "photo", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
