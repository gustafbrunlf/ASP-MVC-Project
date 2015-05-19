namespace PortfolioGB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilePaths : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilePaths",
                c => new
                    {
                        FilePathId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileType = c.Int(nullable: false),
                        PortfolioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FilePathId)
                .ForeignKey("dbo.Portfolios", t => t.PortfolioID, cascadeDelete: true)
                .Index(t => t.PortfolioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilePaths", "PortfolioID", "dbo.Portfolios");
            DropIndex("dbo.FilePaths", new[] { "PortfolioID" });
            DropTable("dbo.FilePaths");
        }
    }
}
