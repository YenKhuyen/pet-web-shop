namespace pet_web_shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04052024_4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_review", "post_id", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_review", "comment", c => c.String());
            CreateIndex("dbo.tb_review", "post_id");
            AddForeignKey("dbo.tb_review", "post_id", "dbo.tb_post", "id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_review", "post_id", "dbo.tb_post");
            DropIndex("dbo.tb_review", new[] { "post_id" });
            AlterColumn("dbo.tb_review", "comment", c => c.Int(nullable: false));
            DropColumn("dbo.tb_review", "post_id");
        }
    }
}
