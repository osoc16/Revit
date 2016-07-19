namespace RevitApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableintsonform : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Forms", "Score", c => c.Int());
            AlterColumn("dbo.Forms", "MinScore", c => c.Int());
            AlterColumn("dbo.Forms", "MaxScore", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Forms", "MaxScore", c => c.Int(nullable: false));
            AlterColumn("dbo.Forms", "MinScore", c => c.Int(nullable: false));
            AlterColumn("dbo.Forms", "Score", c => c.Int(nullable: false));
        }
    }
}
