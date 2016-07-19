namespace Revit.Api.Azure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NationalNumber = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Score = c.Int(),
                        MinScore = c.Int(),
                        MaxScore = c.Int(),
                        MaxCandidates = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Competences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        code = c.String(nullable: false),
                        name_FR = c.String(nullable: false),
                        name_NL = c.String(nullable: false),
                        name_EN = c.String(nullable: false),
                        name_DE = c.String(nullable: false),
                        description_FR = c.String(),
                        description_NL = c.String(),
                        description_EN = c.String(),
                        description_DE = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Dimensions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        code = c.String(nullable: false),
                        name_FR = c.String(nullable: false),
                        name_NL = c.String(nullable: false),
                        name_EN = c.String(nullable: false),
                        name_DE = c.String(nullable: false),
                        description_FR = c.String(),
                        description_NL = c.String(),
                        description_EN = c.String(),
                        description_DE = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Screenings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Form_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Forms", t => t.Form_ID)
                .Index(t => t.Form_ID);
            
            CreateTable(
                "dbo.Juries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FormCandidates",
                c => new
                    {
                        Form_ID = c.Int(nullable: false),
                        Candidate_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Form_ID, t.Candidate_ID })
                .ForeignKey("dbo.Forms", t => t.Form_ID, cascadeDelete: true)
                .ForeignKey("dbo.Candidates", t => t.Candidate_ID, cascadeDelete: true)
                .Index(t => t.Form_ID)
                .Index(t => t.Candidate_ID);
            
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
            
            CreateTable(
                "dbo.CompetenceForms",
                c => new
                    {
                        Competence_ID = c.Int(nullable: false),
                        Form_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Competence_ID, t.Form_ID })
                .ForeignKey("dbo.Competences", t => t.Competence_ID, cascadeDelete: true)
                .ForeignKey("dbo.Forms", t => t.Form_ID, cascadeDelete: true)
                .Index(t => t.Competence_ID)
                .Index(t => t.Form_ID);
            
            CreateTable(
                "dbo.ScreeningCandidates",
                c => new
                    {
                        Screening_ID = c.Int(nullable: false),
                        Candidate_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Screening_ID, t.Candidate_ID })
                .ForeignKey("dbo.Screenings", t => t.Screening_ID, cascadeDelete: true)
                .ForeignKey("dbo.Candidates", t => t.Candidate_ID, cascadeDelete: true)
                .Index(t => t.Screening_ID)
                .Index(t => t.Candidate_ID);
            
            CreateTable(
                "dbo.JuryScreenings",
                c => new
                    {
                        Jury_ID = c.Int(nullable: false),
                        Screening_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Jury_ID, t.Screening_ID })
                .ForeignKey("dbo.Juries", t => t.Jury_ID, cascadeDelete: true)
                .ForeignKey("dbo.Screenings", t => t.Screening_ID, cascadeDelete: true)
                .Index(t => t.Jury_ID)
                .Index(t => t.Screening_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JuryScreenings", "Screening_ID", "dbo.Screenings");
            DropForeignKey("dbo.JuryScreenings", "Jury_ID", "dbo.Juries");
            DropForeignKey("dbo.Screenings", "Form_ID", "dbo.Forms");
            DropForeignKey("dbo.ScreeningCandidates", "Candidate_ID", "dbo.Candidates");
            DropForeignKey("dbo.ScreeningCandidates", "Screening_ID", "dbo.Screenings");
            DropForeignKey("dbo.CompetenceForms", "Form_ID", "dbo.Forms");
            DropForeignKey("dbo.CompetenceForms", "Competence_ID", "dbo.Competences");
            DropForeignKey("dbo.DimensionCompetences", "Competence_ID", "dbo.Competences");
            DropForeignKey("dbo.DimensionCompetences", "Dimension_ID", "dbo.Dimensions");
            DropForeignKey("dbo.FormCandidates", "Candidate_ID", "dbo.Candidates");
            DropForeignKey("dbo.FormCandidates", "Form_ID", "dbo.Forms");
            DropIndex("dbo.JuryScreenings", new[] { "Screening_ID" });
            DropIndex("dbo.JuryScreenings", new[] { "Jury_ID" });
            DropIndex("dbo.ScreeningCandidates", new[] { "Candidate_ID" });
            DropIndex("dbo.ScreeningCandidates", new[] { "Screening_ID" });
            DropIndex("dbo.CompetenceForms", new[] { "Form_ID" });
            DropIndex("dbo.CompetenceForms", new[] { "Competence_ID" });
            DropIndex("dbo.DimensionCompetences", new[] { "Competence_ID" });
            DropIndex("dbo.DimensionCompetences", new[] { "Dimension_ID" });
            DropIndex("dbo.FormCandidates", new[] { "Candidate_ID" });
            DropIndex("dbo.FormCandidates", new[] { "Form_ID" });
            DropIndex("dbo.Screenings", new[] { "Form_ID" });
            DropTable("dbo.JuryScreenings");
            DropTable("dbo.ScreeningCandidates");
            DropTable("dbo.CompetenceForms");
            DropTable("dbo.DimensionCompetences");
            DropTable("dbo.FormCandidates");
            DropTable("dbo.Juries");
            DropTable("dbo.Screenings");
            DropTable("dbo.Dimensions");
            DropTable("dbo.Competences");
            DropTable("dbo.Forms");
            DropTable("dbo.Candidates");
        }
    }
}
