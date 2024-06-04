using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet_web_shop.Models.DAO
{
    public class Cart_DAO
    {
        private PetShopDBContext db = null;
        public Cart_DAO()
        {
            db = new PetShopDBContext();
        }

        public bool Add(int product_id, int user_id, int quantity)
        {
            try
            {
                var check_cart = db.tb_cart.Where(x => x.user_id == user_id && x.product_id == product_id && x.status == Constants.OnCart).FirstOrDefault();
                if (check_cart != null)
                {
                    if (check_cart.product.quantity == 0)
                    {
                        return false;
                    }

                    check_cart.quantity = check_cart.quantity + quantity;
                    check_cart.modified = DateTime.Now;
                    db.tb_cart.Attach(check_cart);
                    db.Entry(check_cart).Property(x => x.quantity).IsModified = true;
                    db.Entry(check_cart).Property(x => x.modified).IsModified = true;
                    db.SaveChanges();
                    return true;
                }
                var user = db.tb_account.Find(user_id);
                var product = db.tb_product.Find(product_id);
                var price = product.isSale && product.sale > 0 ? product.price - product.price * product.sale / 100 : product.price;

                if (product.quantity == 0)
                {
                    return false;
                }

                tb_cart new_cart = new tb_cart
                {
                    user_id = user_id,
                    product_id = product_id,
                    account = user,
                    product = product,
                    created = DateTime.Now,
                    quantity = quantity,
                    status = Constants.OnCart,
                    price = price ?? 0,
                };

                db.tb_cart.Add(new_cart);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetCount(int user_id)
        {
            return db.tb_cart.Where(x => x.user_id == user_id && x.status == Constants.OnCart).Count();
        }

        public bool UpdateQuantity(int id, int quantity)
        {
            var cart = db.tb_cart.Find(id);
            if (cart != null)
            {
                if(quantity > cart.product.quantity)
                {
                    return false;
                }

                cart.quantity = quantity;
                cart.modified = DateTime.Now;

                db.tb_cart.Attach(cart);
                db.Entry(cart).Property(x => x.quantity).IsModified = true;
                db.Entry(cart).Property(x => x.modified).IsModified = true;
                db.SaveChanges();

                return true;
            }

            return false;
        }

        public bool Remove(int id)
        {
            var cart = db.tb_cart.Find(id);
            if (cart != null)
            {
                cart.product.quantity = cart.product.quantity + cart.quantity;
                cart.product.modified = DateTime.Now;
                db.tb_cart.Remove(cart);
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public List<tb_cart> GetCart(int user_id)
        {
            var carts = db.tb_cart.Where(x => x.user_id == user_id && x.status == Constants.OnCart).OrderBy(x => x.created).ToList();

            return carts;
        }

        public bool RemoveAll(int user_id)
        {
            var list_cart = db.tb_cart.Where(x => x.user_id == user_id && x.status == Constants.OnCart);
            if (list_cart != null && list_cart.Any())
            {
                foreach (var cart in list_cart)
                {
                    cart.product.quantity = cart.product.quantity + cart.quantity;
                    cart.product.modified = DateTime.Now;
                    db.tb_cart.Remove(cart);
                }
                db.SaveChanges();
                return true;
            }

            return false;
        }
    }

}