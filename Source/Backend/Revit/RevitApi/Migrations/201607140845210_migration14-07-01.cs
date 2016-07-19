namespace RevitApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration140701 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "NationalNumber", c => c.String(nullable: false));
            AddColumn("dbo.Forms", "MaxCandidates", c => c.Int());
            AddColumn("dbo.Competences", "code", c => c.String(nullable: false));
            AddColumn("dbo.Competences", "name_FR", c => c.String(nullable: false));
            AddColumn("dbo.Competences", "name_NL", c => c.String(nullable: false));
            AddColumn("dbo.Competences", "name_EN", c => c.String(nullable: false));
            AddColumn("dbo.Competences", "name_DE", c => c.String(nullable: false));
            AddColumn("dbo.Competences", "description_FR", c => c.String());
            AddColumn("dbo.Competences", "description_NL", c => c.String());
            AddColumn("dbo.Competences", "description_EN", c => c.String());
            AddColumn("dbo.Competences", "description_DE", c => c.String());
            DropColumn("dbo.Competences", "Name");
            DropColumn("dbo.Competences", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Competences", "Description", c => c.String());
            AddColumn("dbo.Competences", "Name", c => c.String());
            DropColumn("dbo.Competences", "description_DE");
            DropColumn("dbo.Competences", "description_EN");
            DropColumn("dbo.Competences", "description_NL");
            DropColumn("dbo.Competences", "description_FR");
            DropColumn("dbo.Competences", "name_DE");
            DropColumn("dbo.Competences", "name_EN");
            DropColumn("dbo.Competences", "name_NL");
            DropColumn("dbo.Competences", "name_FR");
            DropColumn("dbo.Competences", "code");
            DropColumn("dbo.Forms", "MaxCandidates");
            DropColumn("dbo.Candidates", "NationalNumber");
        }
    }
}
