using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet_web_shop.Models.DAO
{
    public class Product_DAO
    {
        private PetShopDBContext db = null;
        public Product_DAO()
        {
            db = new PetShopDBContext();
        }

        public tb_product GetItemByID(int id)
        {
            return db.tb_product.FirstOrDefault(x => x.id == id);
        }

        public tb_product GetItemByTitle(string title)
        {
            return db.tb_product.FirstOrDefault(x => x.title == title);
        }

        public List<tb_product> GetList(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                return db.tb_product.Where(s => s.title.Contains(search)).OrderBy(x => x.created).ToList();
            }
            return db.tb_product.OrderBy(x => x.created).ToList();
        }

        public tb_product Add(tb_product product, List<string> Images, List<int> DefaulImage)
        {
            product.sold_count = product.sold_count != null ? product.sold_count : 0;
            product.created = DateTime.Now;
            db.tb_product.Add(product);

            if (Images != null && Images.Count > 0)
            {
                for (int i = 0; i < Images.Count; i++)
                {
                    product.images_product.Add(new tb_image_product
                    {
                        product_id = product.id,
                        image = Images[i],
                        isDefault = i + 1 == DefaulImage[0],
                        created = DateTime.Now,
                    });
                }
            }
            db.SaveChanges();
            return product;
        }

        public Boolean Detele(int id)
        {
            var product = db.tb_product.Find(id);
            if (product != null)
            {
                db.tb_product.Remove(product);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public tb_product Update(tb_product product)
        {
            db.tb_product.Attach(product);

            product.modified = DateTime.Now;

            db.Entry(product).Property(x => x.category_id).IsModified = true;
            db.Entry(product).Property(x => x.description).IsModified = true;
            db.Entry(product).Property(x => x.detail).IsModified = true;
            db.Entry(product).Property(x => x.isHot).IsModified = true;
            db.Entry(product).Property(x => x.isNew).IsModified = true;
            db.Entry(product).Property(x => x.isSale).IsModified = true;
            db.Entry(product).Property(x => x.modified).IsModified = true;
            db.Entry(product).Property(x => x.price).IsModified = true;
            db.Entry(product).Property(x => x.quantity).IsModified = true;
            db.Entry(product).Property(x => x.sale).IsModified = true;
            db.Entry(product).Property(x => x.status).IsModified = true;
            db.Entry(product).Property(x => x.title).IsModified = true;
            db.Entry(product).Property(x => x.sold_count).IsModified = true;
            db.SaveChanges();
            return product;
        }
        public List<tb_product> GetTopSoldProduct()
        {
            return db.tb_product.OrderByDescending(x => x.sold_count).Take(5).ToList();
        }

        public List<object> GetTopTen()
        {
            var top = db.tb_product
                .OrderByDescending(p => p.sold_count)
                .Select(g => new
                {
                    name = g.title,
                    count = g.sold_count
                })
                .ToList();

            return top.Cast<object>().ToList();
        }
    }
}