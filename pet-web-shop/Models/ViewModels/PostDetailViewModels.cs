using PagedList;
using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet_web_shop.Models.ViewModels
{
    public class PostDetailViewModels
    {
        public tb_post PostDetail { get; set; }

        public List<tb_post> PostList { get; set; }

        public List<tb_category> CategoryList { get; set; }

        public IPagedList<tb_review> ListCommnet { get; set; }
    }
}