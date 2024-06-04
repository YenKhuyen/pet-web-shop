namespace pet_web_shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04052024_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_post", "contents", c => c.String(nullable: false));
            AddColumn("dbo.tb_post", "created", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_post", "modified", c => c.DateTime());
            DropColumn("dbo.tb_post", "description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_post", "description", c => c.String(nullable: false));
            DropColumn("dbo.tb_post", "modified");
            DropColumn("dbo.tb_post", "created");
            DropColumn("dbo.tb_post", "contents");
        }
    }
}
