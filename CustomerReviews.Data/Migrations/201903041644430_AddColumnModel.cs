namespace CustomerReviews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReview", "Virtues", c => c.String(nullable: false, maxLength: 1024));
            AddColumn("dbo.CustomerReview", "Disadvantages", c => c.String(nullable: false, maxLength: 1024));
            AddColumn("dbo.CustomerReview", "Rate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerReview", "Rate");
            DropColumn("dbo.CustomerReview", "Disadvantages");
            DropColumn("dbo.CustomerReview", "Virtues");
        }
    }
}
