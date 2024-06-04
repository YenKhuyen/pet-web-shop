using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace pet_web_shop.Models.EF
{
    public partial class PetShopDBContext : DbContext
    {
        public PetShopDBContext()
            : base("name=PetShopDBContext")
        {
        }

        public virtual DbSet<tb_account> tb_account { get; set; }
        public virtual DbSet<tb_order> tb_order { get; set; }
        public virtual DbSet<tb_order_detail> tb_order_detail { get; set; }
        public virtual DbSet<tb_category> tb_category { get; set; }
        public virtual DbSet<tb_cart> tb_cart { get; set; }
        public virtual DbSet<tb_image_product> tb_image_product { get; set; }
        public virtual DbSet<tb_review> tb_review { get; set; }
        public virtual DbSet<tb_product> tb_product { get; set; }
        public virtual DbSet<tb_post> tb_post { get; set; }
        public virtual DbSet<tb_about> tb_about { get; set; }
    }
}
