namespace pet_web_shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _30042024 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_cart", "price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_cart", "price");
        }
    }
}
