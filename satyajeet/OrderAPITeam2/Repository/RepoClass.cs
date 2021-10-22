using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderAPITeam2.CakeModel;

namespace OrderAPI.Repository
{
    public class RepoClass : IOrder
    {
        public void AddingToOrderDetails(OrderDetail ord)
        {
            //  _log4net.Info("Add to order details is invoked");
            

            using (var db = new dbCakeStoreContext())
            {
                db.OrderDetails.Add(ord);
                db.SaveChanges();
            }
        }

        public List<Occasion> GetAllOccasion()
        {
            using (var db = new dbCakeStoreContext())
            {
                return db.Occasions.ToList();
            }
        }
        public List<OrderDetail> AllOrders()
        {
            using (var db = new dbCakeStoreContext())
            {
                return db.OrderDetails.ToList();
            }
        }

        public void UpdateStatus(OrderDetail temp)
        {
            using (var db = new dbCakeStoreContext())
            {
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
            }
        }


        public OrderDetail OrderByOrderID(int id)
        {
            using(var db = new dbCakeStoreContext())
            {
                OrderDetail temp = db.OrderDetails.Find(id);
                return temp;
            }
        }

        public List<OrderDetail> OrderdetailsbyCustomerId(int id)
        {
            List<OrderDetail> orderlist = new List<OrderDetail>();
            using (var db = new dbCakeStoreContext())
            {
                foreach (var temp in db.OrderDetails.ToList())
                {
                    if (temp.CustomerId == id)
                    {
                        orderlist.Add(temp);
                    }
                }
                return orderlist;
            }

        }
    }
}
