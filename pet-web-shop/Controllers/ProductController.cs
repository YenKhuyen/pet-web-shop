using PagedList;
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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(string search, string currentFilter, int? page)
        {
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
            var cate_dao = new Category_DAO();
            var productList = dao.GetList(search);

            ViewBag.ListCategory = new SelectList(cate_dao.GetList(""), "id", "title");
            return View(productList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                Redirect("/");
            }
            var dao = new Product_DAO();
            var cate_dao = new Category_DAO();

            var top_sale = dao.GetTopSoldProduct();
            var detail = dao.GetItemByID(id ?? 0);
            var cate_list = cate_dao.GetList("");

            if (detail == null) ;
            {
                Redirect("/");
            }

            var DetailData = new ProductDetailViewModels
            {
                CategoryList = cate_list,
                ProductDetail = detail,
                TopSaleProduct = top_sale,
            };

            return View(DetailData);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
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

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
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

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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
