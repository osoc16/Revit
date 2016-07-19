namespace RevitApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcompetences : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Competences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Dimensions", "Competence_ID", c => c.Int());
            CreateIndex("dbo.Dimensions", "Competence_ID");
            AddForeignKey("dbo.Dimensions", "Competence_ID", "dbo.Competences", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dimensions", "Competence_ID", "dbo.Competences");
            DropIndex("dbo.Dimensions", new[] { "Competence_ID" });
            DropColumn("dbo.Dimensions", "Competence_ID");
            DropTable("dbo.Competences");
        }
    }
}
