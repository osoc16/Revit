namespace Revit.Api.Azure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBV01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Competences", "name_FR", c => c.String());
            AlterColumn("dbo.Competences", "name_NL", c => c.String());
            AlterColumn("dbo.Competences", "name_EN", c => c.String());
            AlterColumn("dbo.Competences", "name_DE", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Competences", "name_DE", c => c.String(nullable: false));
            AlterColumn("dbo.Competences", "name_EN", c => c.String(nullable: false));
            AlterColumn("dbo.Competences", "name_NL", c => c.String(nullable: false));
            AlterColumn("dbo.Competences", "name_FR", c => c.String(nullable: false));
        }
    }
}
