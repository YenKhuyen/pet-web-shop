using PagedList;
using pet_web_shop.Common;
using pet_web_shop.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet_web_shop.Areas.Admin.Controllers
{
    public class OrderManagementController : Controller
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
            if (user.role == Constants.RoleUser)
            {
                return Redirect("~/");
            }

            return null;
        }

        // GET: Admin/OrderManagement
        public ActionResult Index(string search, string currentFilter, int? page)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            var dao = new Order_DAO();
            var order_list = dao.GetList(null, search);

            int pageNumber = (page ?? 1);
            int pageSize = 10;

            return View(order_list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/OrderManagement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/OrderManagement/Create
        public ActionResult Create()
        {


            return View();
        }

        // POST: Admin/OrderManagement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/OrderManagement/Edit/5
        public ActionResult Edit(int id)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            var dao = new Order_DAO();
            var order = dao.GetItemByID(id);

            if (order != null)
            {
                return View(order);
            }


            return RedirectToAction("index");
        }

        // POST: Admin/OrderManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, int status_value)
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return authResult;
                }

                var dao = new Order_DAO();
                var order = dao.GetItemByID(id);

                if (order.status == status_value)
                {
                    ModelState.AddModelError("", "Trạng thái không thay đổi.!");
                    return View(order);
                }

                var updated = dao.UpdateStatus(id, status_value);
                if (!updated)
                {
                    ModelState.AddModelError("", "Cập nhật trạng thái đơn hàng thất bại, vui lòng thử lại sau!");
                    return View(order);
                }

                return View(order);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/OrderManagement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/OrderManagement/Delete/5
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
    }
}
