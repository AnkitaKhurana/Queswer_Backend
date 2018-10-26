namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Body = c.String(),
                        UpvoteCount = c.Int(nullable: false),
                        DownvoteCount = c.Int(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Password = c.String(),
                        Image = c.String(),
                        FollowCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        AuthorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId, cascadeDelete: false)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        AnswerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.AnswerId);
            
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FollowerId = c.Guid(nullable: false),
                        FollowingId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FollowerId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.FollowingId, cascadeDelete: false)
                .Index(t => t.FollowerId)
                .Index(t => t.FollowingId);
            
            CreateTable(
                "dbo.TagQuestions",
                c => new
                    {
                        Tag_Id = c.Guid(nullable: false),
                        Question_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Question_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Question_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Question_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Follows", "FollowingId", "dbo.Users");
            DropForeignKey("dbo.Follows", "FollowerId", "dbo.Users");
            DropForeignKey("dbo.Votes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Votes", "AnswerId", "dbo.Answers");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.TagQuestions", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.TagQuestions", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Questions", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Answers", "AuthorId", "dbo.Users");
            DropIndex("dbo.TagQuestions", new[] { "Question_Id" });
            DropIndex("dbo.TagQuestions", new[] { "Tag_Id" });
            DropIndex("dbo.Follows", new[] { "FollowingId" });
            DropIndex("dbo.Follows", new[] { "FollowerId" });
            DropIndex("dbo.Votes", new[] { "AnswerId" });
            DropIndex("dbo.Votes", new[] { "UserId" });
            DropIndex("dbo.Questions", new[] { "AuthorId" });
            DropIndex("dbo.Answers", new[] { "AuthorId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.TagQuestions");
            DropTable("dbo.Follows");
            DropTable("dbo.Votes");
            DropTable("dbo.Tags");
            DropTable("dbo.Questions");
            DropTable("dbo.Users");
            DropTable("dbo.Answers");
        }
    }
}
