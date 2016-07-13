namespace RevitApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dimensions",
                c => new
                    {
                        DimID = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.DimID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Dimensions");
        }
    }
}
