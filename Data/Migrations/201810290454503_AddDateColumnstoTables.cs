namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateColumnstoTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "UploadDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Answers", "EditDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Questions", "UploadDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Questions", "EditDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "EditDate");
            DropColumn("dbo.Questions", "UploadDate");
            DropColumn("dbo.Answers", "EditDate");
            DropColumn("dbo.Answers", "UploadDate");
        }
    }
}
