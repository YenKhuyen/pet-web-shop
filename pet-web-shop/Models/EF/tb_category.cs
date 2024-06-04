namespace pet_web_shop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_category
    {
        public tb_category()
        {
            this.product = new HashSet<tb_product>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Cần nhập tiêu đề!")]
        public string title { get; set; }

        public string description { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }

        public virtual ICollection<tb_product> product { get; set; }
    }
}
