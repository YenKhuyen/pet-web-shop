namespace pet_web_shop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class tb_order_detail
    {
        [Key]
        [Column(Order = 0)]
        public int order_id { get; set; }

        [Key, Column(Order = 1)]
        public int product_id { get; set; }

        public decimal price { get; set; }

        public int quantity { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }

        [ForeignKey("product_id")]
        [Required]
        public virtual tb_product product { get; set; }

        [ForeignKey("order_id")]
        [Required]
        public virtual tb_order order { get; set; }
    }
}