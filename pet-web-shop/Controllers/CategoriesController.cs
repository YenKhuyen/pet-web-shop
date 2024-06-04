using PagedList;
using pet_web_shop.Models.DAO;
using pet_web_shop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet_web_shop.Controllers
{
    public class CategoriesController : Controller
    {
        public ActionResult CategoriesProduct(int id, string search, string currentFilter, int? page)
        {
            try
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

                var dao = new Category_DAO();
                var category = dao.GetItemByID(id);
                var products = dao.GetSearchProduct(search, id);

                var categoriesViewModel = new CategoriesViewModels
                {
                    Category = category,
                    ListProduct = products.ToPagedList(pageNumber, pageSize)
                };

                return View(categoriesViewModel);
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
                return View("/");
            }
        }

        public ActionResult ListCategories()
        {
            var dao = new Category_DAO();
            var list = dao.GetList("");
            return PartialView("_ListCategories", list);
         }
    }
}
