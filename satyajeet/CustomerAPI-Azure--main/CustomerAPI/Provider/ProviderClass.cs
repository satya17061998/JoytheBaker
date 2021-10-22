using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAPI.CakeModel;
using CustomerAPI.Repository;

namespace CustomerAPI.Provider
{
    public class ProviderClass : IProvider
    {
        ICustomer _customerRepository;
        public ProviderClass()
        {
        }
        public Customer CustomerbyId(int id)
        {
            _customerRepository = new RepoClass();
           return  _customerRepository.CustomerbyId(id);
        }

        public Customer CustomerLogin(string tempPhone, string tempPass, string tempType)
        {
            _customerRepository = new RepoClass();
            return _customerRepository.CustomerLogin(tempPhone, tempPass,tempType);
        }

        public void DeleteCustomerbyId(int id)
        {
            _customerRepository = new RepoClass();
             _customerRepository.DeleteCustomerbyId( id);
        }

        public void RegisterCustomer(Customer temp)
        {
            _customerRepository = new RepoClass();
            _customerRepository.RegisterCustomer(temp);
        }

        public void UpdateCustomer(Customer temp)
        {
            _customerRepository = new RepoClass();
            _customerRepository.UpdateCustomer( temp);
        }
    }
}
