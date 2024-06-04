namespace pet_web_shop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_order
    {
        public tb_order() { this.order_detail = new HashSet<tb_order_detail>(); }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string code { get; set; }

        [Required(ErrorMessage = "Cần nhập họ tên!")]
        public string customer_name { get; set; }

        [Required(ErrorMessage = "Cần nhập số điện thoại!")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Cần nhập địa chỉ!")]
        public string address { get; set; }

        [Required]
        public int user_id { get; set; }

        public decimal total_price { get; set; }

        public int status { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }

        [ForeignKey("user_id")]
        public virtual tb_account user { get; set; }

        public virtual ICollection<tb_order_detail> order_detail { get; set; }
    }
}
