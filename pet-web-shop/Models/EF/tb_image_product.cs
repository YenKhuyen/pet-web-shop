namespace pet_web_shop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_image_product
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int product_id { get; set; }

        [Required]
        [StringLength(500)]
        public string image { get; set; }

        public bool isDefault { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }

        [ForeignKey("product_id")]
        [Required]
        public virtual tb_product product { get; set; }
    }
}
