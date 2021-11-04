namespace CoachSportif.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Gestion_Admin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilisateurs", "Admin", c => c.Boolean(false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilisateurs", "Admin");
        }
    }
}
