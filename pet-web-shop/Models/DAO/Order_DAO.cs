using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace pet_web_shop.Models.DAO
{
    public class Order_DAO
    {
        private PetShopDBContext db = null;
        public Order_DAO()
        {
            db = new PetShopDBContext();
        }

        public tb_order Add(tb_order order, int user_id, List<tb_cart> list_cart)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var user = db.tb_account.Find(user_id);
                    order.created = DateTime.Now;
                    order.user = user;
                    order.user_id = user.id;
                    order.total_price = list_cart.Sum(x => x.price * x.quantity);
                    db.tb_order.Add(order);

                    if (list_cart != null & list_cart.Count > 0)
                    {
                        foreach (var cart in list_cart)
                        {
                            var orderDetail = new tb_order_detail
                            {
                                order_id = order.id,
                                product_id = cart.product.id,
                                price = cart.price,
                                quantity = cart.quantity,
                                created = DateTime.Now
                            };
                            var product = db.tb_product.Find(cart.product.id);
                            var card_order = db.tb_cart.Find(cart.id);
                            db.tb_order_detail.Add(orderDetail);
                            order.order_detail.Add(orderDetail);
                            product.order_detail.Add(orderDetail);
                            product.quantity = product.quantity - cart.quantity;
                            product.sold_count = product.sold_count + cart.quantity;

                            card_order.status = Constants.CartOrdered;
                            card_order.modified = DateTime.Now;
                        }
                    }

                    db.SaveChanges();
                    transaction.Commit();
                    return order;
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
                    // If an error occurs, rollback the transaction to revert changes
                    transaction.Rollback();

                    // Handle the exception or log it
                    // For example:
                    Console.WriteLine("Error occurred: " + ex.Message);
                    return null;
                }
            }
        }

        public Boolean UpdateStatus(int id, int status)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var order = db.tb_order.Find(id);
                    if (order != null)
                    {
                        if (status == Constants.Cancelled)
                        {
                            order.status = status;
                            order.modified = DateTime.Now;
                            db.tb_order.Attach(order);
                            db.Entry(order).Property(x => x.status).IsModified = true;
                            db.Entry(order).Property(x => x.modified).IsModified = true;

                            foreach (var detail in order.order_detail)
                            {
                                var product = detail.product;
                                product.quantity = product.quantity + detail.quantity;
                                product.sold_count = product.sold_count - detail.quantity;
                                product.modified = DateTime.Now;
                            }
                            db.SaveChanges();
                            transaction.Commit();
                            return true;
                        }
                        else if (order.status == Constants.Cancelled && status != Constants.Cancelled)
                        {
                            if (order.order_detail.Any(x => x.quantity > x.product.quantity))
                            {
                                return false;
                            }

                            order.status = status;
                            order.modified = DateTime.Now;
                            db.tb_order.Attach(order);
                            db.Entry(order).Property(x => x.status).IsModified = true;
                            db.Entry(order).Property(x => x.modified).IsModified = true;

                            foreach (var detail in order.order_detail)
                            {
                                var product = detail.product;
                                product.quantity = product.quantity - detail.quantity;
                                product.sold_count = product.sold_count + detail.quantity;
                                product.modified = DateTime.Now;
                            }

                            db.SaveChanges();
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            order.status = status;
                            order.modified = DateTime.Now;
                            db.tb_order.Attach(order);
                            db.Entry(order).Property(x => x.status).IsModified = true;
                            db.Entry(order).Property(x => x.modified).IsModified = true;
                            db.SaveChanges();
                            transaction.Commit();
                        }

                        return true;
                    }



                    return false;
                }
                catch (Exception ex)
                {
                    // If an error occurs, rollback the transaction to revert changes
                    transaction.Rollback();

                    // Handle the exception or log it
                    // For example:
                    Console.WriteLine("Error occurred: " + ex.Message);
                    return false;
                }
            }
        }

        public List<tb_order> GetList(int? status, string search)
        {
            try
            {
                var list = db.tb_order.OrderBy(x => x.created);

                if (search != null)
                {
                    list = list.Where(x => (x.code.Contains(search) || x.phone.Contains(search))).OrderBy(x => x.created);
                }

                if (status != null)
                {
                    list = list.Where(x => (x.status == status)).OrderBy(x => x.created);
                }

                return list.ToList(); ;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                return null;
            }
        }

        public tb_order GetItemByID(int id)
        {
            try
            {
                var order = db.tb_order.Find(id);

                return order;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                return null;
            }
        }

        public decimal[] GetRevenue(int currentYear)
        {

            decimal[] monthlyRevenues = new decimal[12];

            var revenueData = db.tb_order
                .Where(r => r.created.Year == currentYear && r.status == Constants.Delivered)
                .GroupBy(r => r.created.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalRevenue = g.Sum(r => r.total_price)
                })
                .ToList();

            foreach (var data in revenueData)
            {
                monthlyRevenues[data.Month - 1] = data.TotalRevenue;
            }


            return monthlyRevenues;
        }
    }
}
