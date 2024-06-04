namespace pet_web_shop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class tb_post
    {
        public tb_post()
        {
            this.reviews = new HashSet<tb_review>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Cần nhập ảnh!")]
        public byte[] image { get; set; }

        [Required(ErrorMessage = "Cần nhập tiêu đề bài viết!")]
        public string title { get; set; }

        [Required(ErrorMessage = "Cần nhập mô tả ngắn!")]
        public string sub_title { get; set; }

        [Required(ErrorMessage = "Cần nhập nội dung!")]
        [AllowHtml]
        public string contents { get; set; }

        [Required]
        public int user_id { get; set; }

        [ForeignKey("user_id")]
        public virtual tb_account user { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }

        public virtual ICollection<tb_review> reviews { get; set; }
    }
}