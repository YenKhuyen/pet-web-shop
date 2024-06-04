using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet_web_shop.Models.DAO
{
    public class Image_DAO
    {
        private PetShopDBContext db = null;
        public Image_DAO()
        {
            db = new PetShopDBContext();
        }

        public Boolean Add(int product_id, string url)
        {
            var product = db.tb_product.Find(product_id);
            if (product != null)
            {
                product.images_product.Add(new tb_image_product
                {
                    product_id = product.id,
                    image = url,
                    isDefault = false,
                    created = DateTime.Now,
                });
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Detele(int id)
        {
            var image = db.tb_image_product.Find(id);
            if (image != null)
            {
                if (image.isDefault)
                {
                    var new_image_default = db.tb_image_product.Where(x => x.product_id == image.product_id && x.id != image.id);
                    if (new_image_default.Count() > 0)
                    {
                        var temp = new_image_default.OrderBy(x => x.created).First();
                        temp.isDefault = !temp.isDefault;
                        temp.product = temp.product;
                        temp.modified = DateTime.Now;
                        db.tb_image_product.Attach(image);
                        db.Entry(image).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                db.tb_image_product.Remove(image);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean Default(int id)
        {
            var image = db.tb_image_product.Find(id);
            if (image != null)
            {
                var current_default = db.tb_image_product.FirstOrDefault(x => x.product_id == image.product_id && x.isDefault);
                if (current_default != null)
                {
                    current_default.isDefault = !current_default.isDefault;
                    current_default.modified = DateTime.Now;
                    current_default.product = current_default.product;
                    db.tb_image_product.Attach(current_default);
                    db.Entry(current_default).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                image.isDefault = !image.isDefault;
                image.modified = DateTime.Now;
                image.product = image.product;
                db.tb_image_product.Attach(image);
                db.Entry(image).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<tb_image_product> GetListByProductId(int product_id)
        {
            return db.tb_image_product.Where(s => s.product_id == product_id).OrderBy(x => x.created).ToList();
        }
    }
}