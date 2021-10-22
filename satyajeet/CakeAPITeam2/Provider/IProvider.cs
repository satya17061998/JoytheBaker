using CakeAPITeam2.CakeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowApi.Provider
{
    public interface Iprovider
    {
        public Cake GetCakeById(int id);
        public List<Cake> GetCakeById(string name);
        public void UpdateCake(Cake temp);
        public void DeleteCakebyId(int id);
        public void RegisterCake(Cake temp);

        public List<Cake> GetAllCake();

    }
}
