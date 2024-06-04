using pet_web_shop.Common;
using pet_web_shop.Models.DAO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet_web_shop.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            return View();
        }

        public ActionResult Auth()
        {
            var check = CustomAuth.CheckAuth(Session);
            if (!check)
            {
                return Redirect("~/dang-nhap");
            }

            var session = Session[Constants.USER_SESSION] as UserLogin;
            var dao = new User_DAO();
            var user = dao.GetItemByID(session.id);
            if (user.role == Constants.RoleUser)
            {
                return Redirect("~/");
            }

            return null;
        }

        [HttpGet]
        public ActionResult GetRevenueAssume()
        {
            try
            {
                List<string> months = new List<string>
                {
                    "Jan",
                    "Feb",
                    "Mar",
                    "Apr",
                    "May",
                    "Jun",
                    "Jul",
                    "Aug",
                    "Sep",
                    "Oct",
                    "Nov",
                    "Dec"
                };

                var currentYear = DateTime.Now.Year;

                var order_dao = new Order_DAO();
                var revenues = order_dao.GetRevenue(currentYear);

                if (revenues != null && revenues.Any())
                {
                    return Json(new { success = true, revenues = revenues.ToArray(), months = months.ToArray() }, JsonRequestBehavior.AllowGet);
                }


                return Json(new { success = false, msg = "Lỗi! vui lòng thử lại sau!" }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Entity of type \"{entityValidationError.Entry.Entity.GetType().Name}\" has validation error: \"{validationError.ErrorMessage}\"");
                    }
                }

                return Json(new { success = false, error = "Validation failed. See log for details." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTopTen()
        {
            try
            {
                var product_dao = new Product_DAO();
                var top = product_dao.GetTopTen();

                if (top != null && top.Any())
                {
                    return Json(new { success = true, top_ten = top.ToArray() }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = false, msg = "Lỗi! vui lòng thử lại sau!" }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Entity of type \"{entityValidationError.Entry.Entity.GetType().Name}\" has validation error: \"{validationError.ErrorMessage}\"");
                    }
                }

                return Json(new { success = false, error = "Validation failed. See log for details." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}