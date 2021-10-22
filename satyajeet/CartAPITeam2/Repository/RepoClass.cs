using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartAPITeam2.CakeModel;
using Microsoft.EntityFrameworkCore;

namespace CartAPI.Repository
{
    public class RepoClass : ICart
    {
        public void AdditemtoCart(Cart temp)
        {
            
            using (var db = new dbCakeStoreContext()) { 
            db.Carts.Add(temp);
            db.SaveChanges();
        }
            
        }

        public Cart CartByCartID(int id)
        {
            using(var db = new dbCakeStoreContext())
            {
                Cart temp = db.Carts.Find(id);
                return temp;
            }
        }

        public List<Cart> CartbyCustID(int id)
        {
            List<Cart> Cartlist = new List<Cart>();
            using (var db = new dbCakeStoreContext())
            {
                foreach (var temp in db.Carts.ToList())
                {
                    if (temp.CustomerId == id && temp.Status.Equals("Pending"))
                    {
                        Cartlist.Add(temp);
                    }
                }
                return Cartlist;
            }
        }

        public void DeleteItemFromCart(int id)
        {
            using (var db = new dbCakeStoreContext())
            {
                Cart temp = db.Carts.Find(id);
                db.Carts.Remove(temp);
                db.SaveChanges();
            }
        }

        public void UpdateStatusInCart(int id)

        { using (var db = new dbCakeStoreContext())
            {
                Cart temp = db.Carts.Find(id);
                temp.Status = "Placed";
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                
            }



        }
    }
}
