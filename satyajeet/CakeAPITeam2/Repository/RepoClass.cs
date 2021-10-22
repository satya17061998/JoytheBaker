using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CakeAPITeam2.CakeModel;
using Microsoft.EntityFrameworkCore;

namespace FlowApi.Repository
{
    public class RepoClass : ICake
    {
        public RepoClass()
        {
        }
        public readonly dbCakeStoreContext db;
        public Cake GetCakeById(int id)
        {
            //Flower flower = new Flower()
            //{
            //    Id = id,
            //    Name = "Rose",
            //    Occassion="Birthday",
            //    UnitPrice=500,
            //    FlImage=null,
            //};
            using(var db=new dbCakeStoreContext())
            {
                Cake flower = db.Cakes.Find(id);
                return flower;
            }
            
            
        }

        public List<Cake> GetCakeById(string name)
        {
            List<Cake> flowerlist = new List<Cake>();
            using (var db = new dbCakeStoreContext())
            {
                foreach (var temp in  db.Cakes.ToList())
                {
                    if (temp.Name.ToLower() == name.ToLower())
                    {
                        flowerlist.Add(temp);
                    }
                }
            }
            return flowerlist;

        }

        public void UpdateCake(Cake temp)
        {
            using (var db = new dbCakeStoreContext())
            {
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteCakebyId(int id)
        {
            using (var db = new dbCakeStoreContext()) {
                Cake temp =  db.Cakes.Find(id);
                db.Cakes.Remove(temp);
                 db.SaveChanges();
            }
                
        }

        public void RegisterCake(Cake temp)
        {
            using (var db = new dbCakeStoreContext())
            {
                db.Cakes.Add(temp);
                db.SaveChanges();
            }
        }

        public List<Cake> GetAllCake()
        {
            using(var db = new dbCakeStoreContext())
            {
                return db.Cakes.ToList();
            }
            
        }
    }
}
