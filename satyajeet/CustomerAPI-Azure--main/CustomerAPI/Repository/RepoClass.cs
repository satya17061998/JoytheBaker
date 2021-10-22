using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAPI.CakeModel;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Repository
{
    public class RepoClass : ICustomer
    {
        public Customer CustomerbyId(int id)
        {
            using (var db = new dbCakeStoreContext())
            {
                Customer temp =  db.Customers.Find(id);
                return temp;
            }

        }

        public Customer CustomerLogin(string tempPhone, string tempPass, string tempType)
        {
            Customer cus = new Customer();
            using (var db = new dbCakeStoreContext())
            {
                foreach (var temp in  db.Customers.ToList())
                {
                    //changed
                    if (temp.Phone == tempPhone && temp.Password == tempPass && temp.Vendor == tempType)
                    {
                        //changed
                         cus = temp;
                        
                    }
                }
                return cus;
            }
           
        }

        public void DeleteCustomerbyId(int id)
        {
            using (var db = new dbCakeStoreContext())
            {
                Customer temp =  db.Customers.Find(id);
                db.Customers.Remove(temp);
                 db.SaveChanges();
            }
        }

        public void RegisterCustomer(Customer temp)
        {
            using (var db = new dbCakeStoreContext())
            {
                db.Customers.Add(temp);
                db.SaveChanges();
            }
        }

        public void UpdateCustomer(Customer temp)
        {
            using (var db = new dbCakeStoreContext())
            {
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
