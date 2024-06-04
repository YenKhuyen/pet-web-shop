namespace pet_web_shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01052024_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_order", "tb_account_id", "dbo.tb_account");
            DropIndex("dbo.tb_order", new[] { "tb_account_id" });
            RenameColumn(table: "dbo.tb_order", name: "tb_account_id", newName: "user_id");
            AlterColumn("dbo.tb_order", "user_id", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_order", "user_id");
            AddForeignKey("dbo.tb_order", "user_id", "dbo.tb_account", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_order", "user_id", "dbo.tb_account");
            DropIndex("dbo.tb_order", new[] { "user_id" });
            AlterColumn("dbo.tb_order", "user_id", c => c.Int());
            RenameColumn(table: "dbo.tb_order", name: "user_id", newName: "tb_account_id");
            CreateIndex("dbo.tb_order", "tb_account_id");
            AddForeignKey("dbo.tb_order", "tb_account_id", "dbo.tb_account", "id");
        }
    }
}
