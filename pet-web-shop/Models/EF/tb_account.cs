namespace pet_web_shop.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_account
    {
        public tb_account()
        {
            this.review = new HashSet<tb_review>();
            this.carts = new HashSet<tb_cart>();
            this.orders = new HashSet<tb_order>();
            this.posts = new HashSet<tb_post>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Cần nhập tên đăng nhập!")]
        [StringLength(50)]
        public string user_name { get; set; }

        [Required(ErrorMessage = "Cần nhập mật khẩu!")]
        [StringLength(50)]
        public string password { get; set; }

        public int role { get; set; }

        public int status { get; set; }

        [Required(ErrorMessage = "Cần nhập họ!")]
        [StringLength(225)]
        public string first_name { get; set; }

        [Required(ErrorMessage = "Cần nhập tên!")]
        [StringLength(225)]
        public string last_name { get; set; }

        [StringLength(225)]
        public string address { get; set; }

        [Required(ErrorMessage = "Cần nhập ngày sinh!")]
        public DateTime date_of_birth { get; set; }

        [Required(ErrorMessage = "Cần nhập Email!")]
        [StringLength(100)]
        public string email { get; set; }

        [Column(TypeName = "image")]
        public byte[] avatar { get; set; }

        [StringLength(50)]
        public string phone_number { get; set; }

        public int? gender { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }

        public virtual ICollection<tb_review> review { get; set; }

        public virtual ICollection<tb_cart> carts { get; set; }

        public virtual ICollection<tb_order> orders { get; set; }

        public virtual ICollection<tb_post> posts { get; set; }
    }
}
