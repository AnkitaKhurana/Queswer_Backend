namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Image");
        }
    }
}
