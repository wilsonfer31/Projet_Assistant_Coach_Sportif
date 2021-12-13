namespace CoachSportif.Migrations
{
    using CoachSportif.Models;
    using CoachSportif.Tools;
    using System.Data.Entity.Migrations;

    public partial class Gestion_Admin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilisateurs", "Admin", c => c.Boolean(false));
            Sql($"INSERT INTO Villes VALUES(31000,'Toulouse')");
            string gid = "SELECT Id FROM Villes WHERE Nom LIKE 'Toulouse'";
            Sql($"INSERT INTO Utilisateurs(Pseudo,MotDePasse,Mail,Nom,Prenom,Ville_Id,Admin) VALUES('Admin','{HashTool.CryptPassword("Admin")}','admin@ad.min','Admin','Admin',({gid}),'True')");
        }

        public override void Down()
        {
            DropColumn("dbo.Utilisateurs", "Admin");
        }
    }
}
