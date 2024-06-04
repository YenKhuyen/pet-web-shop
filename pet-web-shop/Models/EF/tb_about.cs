namespace pet_web_shop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class tb_about
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Cần nhập nội dung!")]
        [AllowHtml]
        public string contents { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }
    }
}
