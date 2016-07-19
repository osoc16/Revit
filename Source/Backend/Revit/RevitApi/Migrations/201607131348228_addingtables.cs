namespace RevitApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingtables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FormCompetences", newName: "CompetenceForms");
            DropPrimaryKey("dbo.CompetenceForms");
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
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
            
            AddPrimaryKey("dbo.CompetenceForms", new[] { "Competence_ID", "Form_ID" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JuryScreenings", "Screening_ID", "dbo.Screenings");
            DropForeignKey("dbo.JuryScreenings", "Jury_ID", "dbo.Juries");
            DropForeignKey("dbo.Screenings", "Form_ID", "dbo.Forms");
            DropForeignKey("dbo.ScreeningCandidates", "Candidate_ID", "dbo.Candidates");
            DropForeignKey("dbo.ScreeningCandidates", "Screening_ID", "dbo.Screenings");
            DropForeignKey("dbo.FormCandidates", "Candidate_ID", "dbo.Candidates");
            DropForeignKey("dbo.FormCandidates", "Form_ID", "dbo.Forms");
            DropIndex("dbo.JuryScreenings", new[] { "Screening_ID" });
            DropIndex("dbo.JuryScreenings", new[] { "Jury_ID" });
            DropIndex("dbo.ScreeningCandidates", new[] { "Candidate_ID" });
            DropIndex("dbo.ScreeningCandidates", new[] { "Screening_ID" });
            DropIndex("dbo.FormCandidates", new[] { "Candidate_ID" });
            DropIndex("dbo.FormCandidates", new[] { "Form_ID" });
            DropIndex("dbo.Screenings", new[] { "Form_ID" });
            DropPrimaryKey("dbo.CompetenceForms");
            DropTable("dbo.JuryScreenings");
            DropTable("dbo.ScreeningCandidates");
            DropTable("dbo.FormCandidates");
            DropTable("dbo.Juries");
            DropTable("dbo.Screenings");
            DropTable("dbo.Candidates");
            AddPrimaryKey("dbo.CompetenceForms", new[] { "Form_ID", "Competence_ID" });
            RenameTable(name: "dbo.CompetenceForms", newName: "FormCompetences");
        }
    }
}
