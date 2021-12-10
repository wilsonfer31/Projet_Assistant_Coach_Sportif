namespace CoachSportif.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoursChat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cours", "Chat_Id", c => c.Int());
            CreateIndex("dbo.Cours", "Chat_Id");
            AddForeignKey("dbo.Cours", "Chat_Id", "dbo.GroupeChats", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cours", "Chat_Id", "dbo.GroupeChats");
            DropIndex("dbo.Cours", new[] { "Chat_Id" });
            DropColumn("dbo.Cours", "Chat_Id");
        }
    }
}
