using PagedList;
using pet_web_shop.Common;
using pet_web_shop.Models.DAO;
using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet_web_shop.Controllers
{
    public class OrderController : Controller
    {

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
            if (user.role != Constants.RoleUser)
            {
                return Redirect("~/home-admin");
            }

            return null;
        }

        // GET: Order
        public ActionResult Index(string status, string currentFilter, int? page)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            if (status != null)
            {
                page = 1;
            }
            else
            {
                status = currentFilter;
            }

            ViewBag.CurrentFilter = status;
            ViewBag.Status = status;

            var dao = new Order_DAO();
            var list = dao.GetList(null, "");

            if (status != null && status != "-1")
            {
                list = dao.GetList(int.Parse(status), "");
            }

            if (list == null)
            {
                list = new List<tb_order>();
            }

            int pageNumber = (page ?? 1);
            int pageSize = 5;

            return View(list.OrderByDescending(x => x.created).ToPagedList(pageNumber, pageSize));
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            Random rd = new Random();
            var dao = new Cart_DAO();
            var session = Session[Constants.USER_SESSION] as UserLogin;
            var code = "PWS" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
            ViewBag.ListCart = dao.GetCart(session.id);
            ViewBag.UserId = session.id;
            ViewBag.Code = code;
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tb_order order)
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return authResult;
                }

                var user_id = (Session[Constants.USER_SESSION] as UserLogin).id;
                var cart_dao = new Cart_DAO();
                List<tb_cart> list_cart = cart_dao.GetCart(user_id);


                if (ModelState.IsValid)
                {
                    if (list_cart.Any(x => x.quantity > x.product.quantity))
                    {
                        ModelState.AddModelError("", "Đặt hàng thất bại, không đủ số lượng sản phẩm để đặt hàng, vui lòng thử lại sau!");
                    }
                    else
                    {
                        var dao = new Order_DAO();
                        var created = dao.Add(order, user_id, list_cart);
                        if (created != null)
                            return RedirectToAction("Success");
                        else
                            ModelState.AddModelError("", "Đặt hàng thất bại, vui lòng thử lại sau!");
                    }
                }

                Random rd = new Random();
                var dao_cate = new Cart_DAO();
                var code = "PWS" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                ViewBag.UserId = user_id;
                ViewBag.Code = code;
                ViewBag.ListCart = dao_cate.GetCart(user_id);

                return View("Create");
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
                var user_id = (Session[Constants.USER_SESSION] as UserLogin).id;
                var dao_cate = new Cart_DAO();

                Random rd = new Random();
                var code = "PWS" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                ViewBag.UserId = user_id;
                ViewBag.Code = code;
                ViewBag.ListCart = dao_cate.GetCart(user_id);

                return View("Create");
            }
        }

        public ActionResult MenuLeft(int? status)
        {
            if (status != null)
            {
                ViewBag.Status = status;
            }
            else
            {
                ViewBag.Status = null;
            }
            return PartialView("_MenuLeft");
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Success()
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            return View();
        }

        [HttpPost]
        public ActionResult CancelOrder(int id)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }
            var dao = new Order_DAO();
            var cancel = dao.UpdateStatus(id, Constants.Cancelled);

            if (cancel)
            {
                return Json(new { success = true, msg = "Đơn hàng đã được huỷ thành công!" });
            }

            return Json(new { success = false, msg = "Đã xảy ra lỗi, vui lòng thử lại sau!" });
        }
    }
}
