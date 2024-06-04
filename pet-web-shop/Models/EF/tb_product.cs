namespace pet_web_shop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class tb_product
    {
        public tb_product()
        {
            this.images_product = new HashSet<tb_image_product>();
            this.order_detail = new HashSet<tb_order_detail>();
            this.carts = new HashSet<tb_cart>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Cần nhập tiêu đề!")]
        public string title { get; set; }

        [Required(ErrorMessage = "Cần chọn danh mục!")]
        public int category_id { get; set; }

        [Required(ErrorMessage = "Cần nhập mô tả chi tiết!")]
        [AllowHtml]
        public string description { get; set; }

        [Required(ErrorMessage = "Cần nhập giá tiền!")]
        public int price { get; set; }

        public int? sale { get; set; }

        public int? sold_count { get; set; }

        [Required(ErrorMessage = "Cần nhập số lượng!")]
        public int quantity { get; set; }

        public int? status { get; set; }

        public string detail { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }

        public bool isHot { get; set; }

        public bool isNew { get; set; }

        public bool isSale { get; set; }

        [ForeignKey("category_id")]
        public virtual tb_category category { get; set; }

        public virtual ICollection<tb_image_product> images_product { get; set; }

        public virtual ICollection<tb_order_detail> order_detail { get; set; }

        public virtual ICollection<tb_cart> carts { get; set; }
    }
}
