namespace pet_web_shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01052024 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_account", "tb_order_id", "dbo.tb_order");
            DropIndex("dbo.tb_account", new[] { "tb_order_id" });
            CreateTable(
                "dbo.tb_post",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        image = c.Binary(nullable: false),
                        title = c.String(nullable: false),
                        sub_title = c.String(nullable: false),
                        description = c.String(nullable: false),
                        user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tb_account", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id);
            
            AddColumn("dbo.tb_order", "tb_account_id", c => c.Int());
            CreateIndex("dbo.tb_order", "tb_account_id");
            AddForeignKey("dbo.tb_order", "tb_account_id", "dbo.tb_account", "id");
            DropColumn("dbo.tb_account", "tb_order_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_account", "tb_order_id", c => c.Int());
            DropForeignKey("dbo.tb_post", "user_id", "dbo.tb_account");
            DropForeignKey("dbo.tb_order", "tb_account_id", "dbo.tb_account");
            DropIndex("dbo.tb_post", new[] { "user_id" });
            DropIndex("dbo.tb_order", new[] { "tb_account_id" });
            DropColumn("dbo.tb_order", "tb_account_id");
            DropTable("dbo.tb_post");
            CreateIndex("dbo.tb_account", "tb_order_id");
            AddForeignKey("dbo.tb_account", "tb_order_id", "dbo.tb_order", "id");
        }
    }
}
