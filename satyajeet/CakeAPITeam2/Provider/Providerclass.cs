using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CakeAPITeam2.CakeModel;
using FlowApi.Repository;

namespace FlowApi.Provider
{
    public class Providerclass : Iprovider
    {
        ICake _flowerRepository;
        public Providerclass()
        {
        }

        public void DeleteCakebyId(int id)
        {
            _flowerRepository = new RepoClass();
            _flowerRepository.DeleteCakebyId(id);
        }

        public List<Cake> GetAllCake()
        {
            _flowerRepository = new RepoClass();
            return _flowerRepository.GetAllCake();
        }

        public Cake GetCakeById(int id)
        {
            _flowerRepository = new RepoClass();
            return _flowerRepository.GetCakeById(id);
        }

        public List<Cake> GetCakeById(string name)
        {
            _flowerRepository = new RepoClass();
            return _flowerRepository.GetCakeById(name);
        }

        public void RegisterCake(Cake temp)
        {
            _flowerRepository = new RepoClass();
             _flowerRepository.RegisterCake(temp);
        }

        public void UpdateCake(Cake temp)
        {
            _flowerRepository = new RepoClass();
             _flowerRepository.UpdateCake(temp);
        }
    }
}
