namespace RevitApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcompetences1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Score = c.Int(nullable: false),
                        MinScore = c.Int(nullable: false),
                        MaxScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FormCompetences",
                c => new
                    {
                        Form_ID = c.Int(nullable: false),
                        Competence_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Form_ID, t.Competence_ID })
                .ForeignKey("dbo.Forms", t => t.Form_ID, cascadeDelete: true)
                .ForeignKey("dbo.Competences", t => t.Competence_ID, cascadeDelete: true)
                .Index(t => t.Form_ID)
                .Index(t => t.Competence_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FormCompetences", "Competence_ID", "dbo.Competences");
            DropForeignKey("dbo.FormCompetences", "Form_ID", "dbo.Forms");
            DropIndex("dbo.FormCompetences", new[] { "Competence_ID" });
            DropIndex("dbo.FormCompetences", new[] { "Form_ID" });
            DropTable("dbo.FormCompetences");
            DropTable("dbo.Forms");
        }
    }
}
