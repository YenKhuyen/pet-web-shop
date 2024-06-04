using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using pet_web_shop.Models.DAO;
using System.Data.Entity;
using pet_web_shop.Models.EF;
using pet_web_shop.Common;

namespace pet_web_shop.Areas.Admin.Controllers
{
    public class ProductManagementController : Controller
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

        // GET: Admin/ProductManagement
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

            int pageNumber = (page ?? 1);
            int pageSize = 10;


            var dao = new Product_DAO();
            var productList = dao.GetList(search);

            return View(productList.ToPagedList(pageNumber, pageSize));
        }


        // GET: Admin/ProductManagement/Create
        [HttpGet]
        public ActionResult Create()
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            var dao = new Category_DAO();
            ViewBag.ListCategory = new SelectList(dao.GetList(""), "id", "title");
            return View();
        }

        // POST: Admin/ProductManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tb_product product, List<string> Images, List<int> DefaulImage)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            var cate_dao = new Category_DAO();
            if (ModelState.IsValid)
            {
                var dao = new Product_DAO();
                var productCheck = dao.GetItemByTitle(product.title);

                if (productCheck != null)
                {
                    ModelState.AddModelError("", "Thêm sản phẩm thất bại! Sản phẩm đã tồn tại!");
                }
                else
                {
                    var pd = dao.Add(product, Images, DefaulImage);
                    if (pd != null)
                    {
                        return RedirectToAction("index", "productmanagement");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm sản phẩm thất bại");
                    }
                }

            }
            ViewBag.ListCategory = new SelectList(cate_dao.GetList(""), "id", "title");
            return View("Create");

        }

        // GET: Admin/ProductManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            if (id == null)
            {
                return RedirectToAction("index", "productmanagement");
            }
            var cate_dao = new Category_DAO();
            ViewBag.ListCategory = new SelectList(cate_dao.GetList(""), "id", "title");
            var dao = new Product_DAO();
            var product = dao.GetItemByID(id ?? 0);
            if (product != null)
                return View(product);
            return RedirectToAction("index", "productmanagement");
        }

        // POST: Admin/ProductManagement/Edit/5
        [HttpPost]
        public ActionResult Edit(tb_product product)
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return authResult;
                }

                if (ModelState.IsValid)
                {
                    var dao = new Product_DAO();

                    if (product != null)
                    {
                        var updated = dao.Update(product);
                        if (updated != null)
                            return RedirectToAction("index", "productmanagement");
                        else
                        {
                            ModelState.AddModelError("", "Cập nhất thông tin sản phẩm thất bại, vui lòng thử lại sau!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Không tìm được sản phẩm, vui lòng thử lại sau!");
                    }
                }
                return View(product);
            }
            catch
            {
                return View();
            }
        }


        // POST: Admin/ProductManagement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return Json(new { success = false });
            }

            var dao = new Product_DAO();
            var product = dao.GetItemByID(id);

            if (product != null)
            {
                var deleted = dao.Detele(id);
                return Json(new { success = deleted });
            }
            return Json(new { success = false });
        }
    }
}
