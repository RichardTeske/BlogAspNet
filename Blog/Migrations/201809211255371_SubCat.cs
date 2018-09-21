namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubCat : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SubCategory", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubCategory", "Description", c => c.String());
        }
    }
}
