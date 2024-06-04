namespace pet_web_shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04052024_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_review", "product_id", "dbo.tb_product");
            DropIndex("dbo.tb_review", new[] { "product_id" });
            AddColumn("dbo.tb_review", "tb_post_id", c => c.Int());
            CreateIndex("dbo.tb_review", "tb_post_id");
            AddForeignKey("dbo.tb_review", "tb_post_id", "dbo.tb_post", "id");
            DropColumn("dbo.tb_review", "product_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_review", "product_id", c => c.Int(nullable: false));
            DropForeignKey("dbo.tb_review", "tb_post_id", "dbo.tb_post");
            DropIndex("dbo.tb_review", new[] { "tb_post_id" });
            DropColumn("dbo.tb_review", "tb_post_id");
            CreateIndex("dbo.tb_review", "product_id");
            AddForeignKey("dbo.tb_review", "product_id", "dbo.tb_product", "id", cascadeDelete: true);
        }
    }
}
