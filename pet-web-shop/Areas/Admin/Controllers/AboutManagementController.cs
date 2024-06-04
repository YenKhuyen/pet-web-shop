using pet_web_shop.Common;
using pet_web_shop.Models.DAO;
using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet_web_shop.Areas.Admin.Controllers
{
    public class AboutManagementController : Controller
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

        // GET: Admin/About
        public ActionResult Index()
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            var dao = new About_DAO();
            var about = dao.GetItem();
            if (about != null)
                return View(about);
            return View();
        }

        // GET: Admin/About/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/About/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/About/Create
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

        [HttpGet]
        public ActionResult Edit()
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            var dao = new About_DAO();
            var about = dao.GetItem();
            if (about != null)
                return View(about);
            return View();
        }

        // POST: Admin/About/Edit/5
        [HttpPost]
        public ActionResult Edit(tb_about about)
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return authResult;
                }

                var dao = new About_DAO();
                var done = dao.Update(about);

                if (done)
                {
                    if (about != null)
                    {
                        return RedirectToAction("index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra, vui lòng thử lại sau!");
                }
                var new_about = dao.GetItem();
                if (new_about != null)
                {
                    return View(new_about);
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/About/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/About/Delete/5
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
