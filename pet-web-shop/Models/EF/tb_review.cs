namespace pet_web_shop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_review
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string comment { get; set; }

        public int post_id { get; set; }

        [Required]
        public int user_id { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }

        [ForeignKey("post_id")]
        public virtual tb_post post { get; set; }

        [ForeignKey("user_id")]
        public virtual tb_account account { get; set; }
    }
}
