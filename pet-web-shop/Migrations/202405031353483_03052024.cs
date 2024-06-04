namespace pet_web_shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03052024 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_about",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        contents = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tb_about");
        }
    }
}
