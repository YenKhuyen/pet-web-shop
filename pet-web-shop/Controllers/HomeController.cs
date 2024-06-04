using pet_web_shop.Common;
using pet_web_shop.Models.DAO;
using pet_web_shop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet_web_shop.Controllers
{
    public class HomeController : Controller
    {
        [Route("trang-chu")]
        public ActionResult Index()
        {
            var cate_dao = new Category_DAO();
            var list_cate = cate_dao.GetList("");
            var homeViewModel = new HomeViewModels
            {
                ListCate = list_cate.Take(5).ToList(),
            };

            return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}