using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet_web_shop.Areas.Admin.Controllers
{
    public class CartManagementController : Controller
    {
        // GET: Admin/CartManagement
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/CartManagement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CartManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CartManagement/Create
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

        // GET: Admin/CartManagement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/CartManagement/Edit/5
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

        // GET: Admin/CartManagement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CartManagement/Delete/5
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
