namespace pet_web_shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_account",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_name = c.String(nullable: false, maxLength: 50),
                        password = c.String(nullable: false, maxLength: 50),
                        role = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        first_name = c.String(nullable: false, maxLength: 225),
                        last_name = c.String(nullable: false, maxLength: 225),
                        address = c.String(maxLength: 225),
                        date_of_birth = c.DateTime(nullable: false),
                        email = c.String(nullable: false, maxLength: 100),
                        avatar = c.Binary(storeType: "image"),
                        phone_number = c.String(maxLength: 50),
                        gender = c.Int(),
                        created = c.DateTime(nullable: false),
                        modified = c.DateTime(),
                        tb_order_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tb_order", t => t.tb_order_id)
                .Index(t => t.tb_order_id);
            
            CreateTable(
                "dbo.tb_cart",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_id = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        created = c.DateTime(nullable: false),
                        modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tb_account", t => t.user_id, cascadeDelete: true)
                .ForeignKey("dbo.tb_product", t => t.product_id, cascadeDelete: true)
                .Index(t => t.product_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.tb_product",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        category_id = c.Int(nullable: false),
                        description = c.String(nullable: false),
                        price = c.Int(nullable: false),
                        sale = c.Int(),
                        sold_count = c.Int(),
                        quantity = c.Int(nullable: false),
                        status = c.Int(),
                        detail = c.String(),
                        created = c.DateTime(nullable: false),
                        modified = c.DateTime(),
                        isHot = c.Boolean(nullable: false),
                        isNew = c.Boolean(nullable: false),
                        isSale = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tb_category", t => t.category_id, cascadeDelete: true)
                .Index(t => t.category_id);
            
            CreateTable(
                "dbo.tb_category",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        description = c.String(),
                        created = c.DateTime(nullable: false),
                        modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tb_image_product",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_id = c.Int(nullable: false),
                        image = c.String(nullable: false, maxLength: 500),
                        isDefault = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tb_product", t => t.product_id, cascadeDelete: true)
                .Index(t => t.product_id);
            
            CreateTable(
                "dbo.tb_order_detail",
                c => new
                    {
                        order_id = c.Int(nullable: false),
                        product_id = c.Int(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        quantity = c.Int(nullable: false),
                        created = c.DateTime(nullable: false),
                        modified = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.order_id, t.product_id })
                .ForeignKey("dbo.tb_order", t => t.order_id, cascadeDelete: true)
                .ForeignKey("dbo.tb_product", t => t.product_id, cascadeDelete: true)
                .Index(t => t.order_id)
                .Index(t => t.product_id);
            
            CreateTable(
                "dbo.tb_order",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        code = c.String(nullable: false),
                        customer_name = c.String(nullable: false),
                        phone = c.String(nullable: false),
                        address = c.String(nullable: false),
                        total_price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        status = c.Int(nullable: false),
                        created = c.DateTime(nullable: false),
                        modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tb_review",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_id = c.Int(nullable: false),
                        comment = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                        created = c.DateTime(nullable: false),
                        modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tb_account", t => t.user_id, cascadeDelete: true)
                .ForeignKey("dbo.tb_product", t => t.product_id, cascadeDelete: true)
                .Index(t => t.product_id)
                .Index(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_review", "product_id", "dbo.tb_product");
            DropForeignKey("dbo.tb_review", "user_id", "dbo.tb_account");
            DropForeignKey("dbo.tb_order_detail", "product_id", "dbo.tb_product");
            DropForeignKey("dbo.tb_order_detail", "order_id", "dbo.tb_order");
            DropForeignKey("dbo.tb_account", "tb_order_id", "dbo.tb_order");
            DropForeignKey("dbo.tb_image_product", "product_id", "dbo.tb_product");
            DropForeignKey("dbo.tb_product", "category_id", "dbo.tb_category");
            DropForeignKey("dbo.tb_cart", "product_id", "dbo.tb_product");
            DropForeignKey("dbo.tb_cart", "user_id", "dbo.tb_account");
            DropIndex("dbo.tb_review", new[] { "user_id" });
            DropIndex("dbo.tb_review", new[] { "product_id" });
            DropIndex("dbo.tb_order_detail", new[] { "product_id" });
            DropIndex("dbo.tb_order_detail", new[] { "order_id" });
            DropIndex("dbo.tb_image_product", new[] { "product_id" });
            DropIndex("dbo.tb_product", new[] { "category_id" });
            DropIndex("dbo.tb_cart", new[] { "user_id" });
            DropIndex("dbo.tb_cart", new[] { "product_id" });
            DropIndex("dbo.tb_account", new[] { "tb_order_id" });
            DropTable("dbo.tb_review");
            DropTable("dbo.tb_order");
            DropTable("dbo.tb_order_detail");
            DropTable("dbo.tb_image_product");
            DropTable("dbo.tb_category");
            DropTable("dbo.tb_product");
            DropTable("dbo.tb_cart");
            DropTable("dbo.tb_account");
        }
    }
}
