namespace pet_web_shop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public partial class tb_cart
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int product_id { get; set; }

        [Required]
        public int user_id { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]

        public int price { get; set; }

        public int status { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }

        [ForeignKey("user_id")]
        public virtual tb_account account { get; set; }

        [ForeignKey("product_id")]
        public virtual tb_product product { get; set; }
    }
}
