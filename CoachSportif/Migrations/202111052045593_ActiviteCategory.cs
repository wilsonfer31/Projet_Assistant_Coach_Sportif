namespace CoachSportif.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActiviteCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activites", "Categorie_Id", c => c.Int());
            CreateIndex("dbo.Activites", "Categorie_Id");
            AddForeignKey("dbo.Activites", "Categorie_Id", "dbo.CategorieActivites", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activites", "Categorie_Id", "dbo.CategorieActivites");
            DropIndex("dbo.Activites", new[] { "Categorie_Id" });
            DropColumn("dbo.Activites", "Categorie_Id");
        }
    }
}
