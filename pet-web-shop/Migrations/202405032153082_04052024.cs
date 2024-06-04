namespace pet_web_shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04052024 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_about", "created", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_about", "modified", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_about", "modified");
            DropColumn("dbo.tb_about", "created");
        }
    }
}
