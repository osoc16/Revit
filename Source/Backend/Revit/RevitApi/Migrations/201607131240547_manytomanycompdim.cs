namespace RevitApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manytomanycompdim : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dimensions", "Competence_ID", "dbo.Competences");
            DropIndex("dbo.Dimensions", new[] { "Competence_ID" });
            CreateTable(
                "dbo.DimensionCompetences",
                c => new
                    {
                        Dimension_ID = c.Int(nullable: false),
                        Competence_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Dimension_ID, t.Competence_ID })
                .ForeignKey("dbo.Dimensions", t => t.Dimension_ID, cascadeDelete: true)
                .ForeignKey("dbo.Competences", t => t.Competence_ID, cascadeDelete: true)
                .Index(t => t.Dimension_ID)
                .Index(t => t.Competence_ID);
            
            DropColumn("dbo.Dimensions", "Competence_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dimensions", "Competence_ID", c => c.Int());
            DropForeignKey("dbo.DimensionCompetences", "Competence_ID", "dbo.Competences");
            DropForeignKey("dbo.DimensionCompetences", "Dimension_ID", "dbo.Dimensions");
            DropIndex("dbo.DimensionCompetences", new[] { "Competence_ID" });
            DropIndex("dbo.DimensionCompetences", new[] { "Dimension_ID" });
            DropTable("dbo.DimensionCompetences");
            CreateIndex("dbo.Dimensions", "Competence_ID");
            AddForeignKey("dbo.Dimensions", "Competence_ID", "dbo.Competences", "ID");
        }
    }
}
