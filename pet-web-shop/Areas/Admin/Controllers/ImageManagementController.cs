using pet_web_shop.Common;
using pet_web_shop.Models.DAO;
using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet_web_shop.Areas.Admin.Controllers
{
    public class ImageManagementController : Controller
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

        // GET: Admin/ImageProduct
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/ImageProduct/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/ImageProduct/Create
        //public ActionResult Create(int product_id, string url_image)
        //{

        //    return View();
        //}

        // POST: Admin/ImageProduct/Create
        [HttpPost]
        public ActionResult Create(int product_id, string url_image)
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return authResult;
                }

                var dao = new Image_DAO();
                var check = dao.Add(product_id, url_image);

                if (check)
                {
                    var new_list = dao.GetListByProductId(product_id);
                    List<dynamic> arr = new List<dynamic>();

                    foreach (tb_image_product image in new_list)
                    {
                        arr.Add(new { id = image.id, product_id = image.product_id, image = image.image, isDefault = image.isDefault });
                    }
                    return Json(new { success = true, new_list = arr.ToArray() });
                }
                return Json(new { success = false });
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

                return Json(new { success = false, error = "Validation failed. See log for details." });
            }
        }

        // GET: Admin/ImageProduct/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/ImageProduct/Edit/5
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

        // GET: Admin/ImageProduct/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Admin/ImageProduct/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int product_id)
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return Json(new { success = false });
                }

                var dao = new Image_DAO();
                var check = dao.Detele(id);

                if (check)
                {
                    var new_list = dao.GetListByProductId(product_id);
                    List<dynamic> arr = new List<dynamic>();

                    foreach (tb_image_product image in new_list)
                    {
                        arr.Add(new { id = image.id, product_id = image.product_id, image = image.image, isDefault = image.isDefault });
                    }
                    return Json(new { success = true, new_list = arr.ToArray() });
                }
                return Json(new { success = false });
            }
            catch (Exception e)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public ActionResult DefaultImage(int id, int product_id)
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return Json(new { success = false });
                }

                var dao = new Image_DAO();
                var check = dao.Default(id);

                if (check)
                {
                    var new_list = dao.GetListByProductId(product_id);
                    List<dynamic> arr = new List<dynamic>();

                    foreach (tb_image_product image in new_list)
                    {
                        arr.Add(new { id = image.id, product_id = image.product_id, image = image.image, isDefault = image.isDefault });
                    }
                    return Json(new { success = true, new_list = arr.ToArray() });
                }
                return Json(new { success = false });
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

                return Json(new { success = false, error = "Validation failed. See log for details." });
            }
        }


    }
}
