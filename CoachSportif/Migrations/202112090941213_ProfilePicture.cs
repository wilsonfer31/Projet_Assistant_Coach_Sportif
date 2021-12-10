namespace CoachSportif.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ProfilePicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilisateurs", "ProfilePicture", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Utilisateurs", "ProfilePicture");
        }
    }
}
