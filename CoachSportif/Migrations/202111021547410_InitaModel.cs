namespace CoachSportif.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitaModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activites",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ImageUrl = c.String(),
                    Descritption = c.String(),
                    Nom = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.CategorieActivites",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Descritption = c.String(),
                    Nom = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Coaches",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Utilisateur_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Utilisateurs", t => t.Utilisateur_Id)
                .Index(t => t.Utilisateur_Id);

            CreateTable(
                "dbo.Cours",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    DateCours = c.DateTime(nullable: false),
                    Activite_Id = c.Int(),
                    Adresse_Id = c.Int(),
                    Coach_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activites", t => t.Activite_Id)
                .ForeignKey("dbo.Villes", t => t.Adresse_Id)
                .ForeignKey("dbo.Coaches", t => t.Coach_Id)
                .Index(t => t.Activite_Id)
                .Index(t => t.Adresse_Id)
                .Index(t => t.Coach_Id);

            CreateTable(
                "dbo.Utilisateurs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Pseudo = c.String(nullable: false),
                    MotDePasse = c.String(nullable: false),
                    Prenom = c.String(),
                    Tel = c.String(),
                    Mail = c.String(nullable: false),
                    Adresse = c.String(),
                    Nom = c.String(),
                    Ville_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Villes", t => t.Ville_Id, cascadeDelete: true)
                .Index(t => t.Ville_Id);

            CreateTable(
                "dbo.GroupeChats",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Nom = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Messages",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Date = c.DateTime(nullable: false),
                    MessageText = c.String(),
                    Utilisateur_Id = c.Int(),
                    GroupeChat_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Utilisateurs", t => t.Utilisateur_Id)
                .ForeignKey("dbo.GroupeChats", t => t.GroupeChat_Id)
                .Index(t => t.Utilisateur_Id)
                .Index(t => t.GroupeChat_Id);

            CreateTable(
                "dbo.Villes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CP = c.Int(nullable: false),
                    Nom = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UtilisateurCours",
                c => new
                {
                    Utilisateur_Id = c.Int(nullable: false),
                    Cours_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Utilisateur_Id, t.Cours_Id })
                .ForeignKey("dbo.Utilisateurs", t => t.Utilisateur_Id, cascadeDelete: true)
                .ForeignKey("dbo.Cours", t => t.Cours_Id, cascadeDelete: true)
                .Index(t => t.Utilisateur_Id)
                .Index(t => t.Cours_Id);

            CreateTable(
                "dbo.GroupeChatUtilisateurs",
                c => new
                {
                    GroupeChat_Id = c.Int(nullable: false),
                    Utilisateur_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.GroupeChat_Id, t.Utilisateur_Id })
                .ForeignKey("dbo.GroupeChats", t => t.GroupeChat_Id, cascadeDelete: true)
                .ForeignKey("dbo.Utilisateurs", t => t.Utilisateur_Id, cascadeDelete: true)
                .Index(t => t.GroupeChat_Id)
                .Index(t => t.Utilisateur_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Coaches", "Utilisateur_Id", "dbo.Utilisateurs");
            DropForeignKey("dbo.Cours", "Coach_Id", "dbo.Coaches");
            DropForeignKey("dbo.Cours", "Adresse_Id", "dbo.Villes");
            DropForeignKey("dbo.Utilisateurs", "Ville_Id", "dbo.Villes");
            DropForeignKey("dbo.GroupeChatUtilisateurs", "Utilisateur_Id", "dbo.Utilisateurs");
            DropForeignKey("dbo.GroupeChatUtilisateurs", "GroupeChat_Id", "dbo.GroupeChats");
            DropForeignKey("dbo.Messages", "GroupeChat_Id", "dbo.GroupeChats");
            DropForeignKey("dbo.Messages", "Utilisateur_Id", "dbo.Utilisateurs");
            DropForeignKey("dbo.UtilisateurCours", "Cours_Id", "dbo.Cours");
            DropForeignKey("dbo.UtilisateurCours", "Utilisateur_Id", "dbo.Utilisateurs");
            DropForeignKey("dbo.Cours", "Activite_Id", "dbo.Activites");
            DropIndex("dbo.GroupeChatUtilisateurs", new[] { "Utilisateur_Id" });
            DropIndex("dbo.GroupeChatUtilisateurs", new[] { "GroupeChat_Id" });
            DropIndex("dbo.UtilisateurCours", new[] { "Cours_Id" });
            DropIndex("dbo.UtilisateurCours", new[] { "Utilisateur_Id" });
            DropIndex("dbo.Messages", new[] { "GroupeChat_Id" });
            DropIndex("dbo.Messages", new[] { "Utilisateur_Id" });
            DropIndex("dbo.Utilisateurs", new[] { "Ville_Id" });
            DropIndex("dbo.Cours", new[] { "Coach_Id" });
            DropIndex("dbo.Cours", new[] { "Adresse_Id" });
            DropIndex("dbo.Cours", new[] { "Activite_Id" });
            DropIndex("dbo.Coaches", new[] { "Utilisateur_Id" });
            DropTable("dbo.GroupeChatUtilisateurs");
            DropTable("dbo.UtilisateurCours");
            DropTable("dbo.Villes");
            DropTable("dbo.Messages");
            DropTable("dbo.GroupeChats");
            DropTable("dbo.Utilisateurs");
            DropTable("dbo.Cours");
            DropTable("dbo.Coaches");
            DropTable("dbo.CategorieActivites");
            DropTable("dbo.Activites");
        }
    }
}
