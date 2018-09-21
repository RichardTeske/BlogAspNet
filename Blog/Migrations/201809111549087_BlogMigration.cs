namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Date = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Post_PostId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Post", t => t.Post_PostId)
                .Index(t => t.Post_PostId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Body = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Category_CategoryId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Category", t => t.Category_CategoryId)
                .ForeignKey("dbo.User", t => t.User_UserId)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Biography = c.String(),
                        Admin = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.SubCategory",
                c => new
                    {
                        SubCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Category_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.SubCategoryId)
                .ForeignKey("dbo.Category", t => t.Category_CategoryId)
                .Index(t => t.Category_CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubCategory", "Category_CategoryId", "dbo.Category");
            DropForeignKey("dbo.Comment", "Post_PostId", "dbo.Post");
            DropForeignKey("dbo.Post", "User_UserId", "dbo.User");
            DropForeignKey("dbo.Post", "Category_CategoryId", "dbo.Category");
            DropIndex("dbo.SubCategory", new[] { "Category_CategoryId" });
            DropIndex("dbo.Post", new[] { "User_UserId" });
            DropIndex("dbo.Post", new[] { "Category_CategoryId" });
            DropIndex("dbo.Comment", new[] { "Post_PostId" });
            DropTable("dbo.SubCategory");
            DropTable("dbo.User");
            DropTable("dbo.Post");
            DropTable("dbo.Comment");
            DropTable("dbo.Category");
        }
    }
}
