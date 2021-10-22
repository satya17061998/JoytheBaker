using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartAPI.Repository;
using CartAPITeam2.CakeModel;

namespace CartAPI.Provider
{
    public class ProviderClass : IProvider

    {

        ICart _cartRepository;
        public ProviderClass()
        {
        }
        public void AdditemtoCart(Cart temp)
        {
            _cartRepository = new RepoClass();
             _cartRepository.AdditemtoCart(temp);
        }

        public Cart CartByCartID(int id)
        {
            _cartRepository = new RepoClass();
            Cart temp = _cartRepository.CartByCartID(id);
            return temp;
        }

        public List<Cart> CartbyCustID(int id)
        {
            _cartRepository = new RepoClass();
            return _cartRepository.CartbyCustID(id);
        }

        public void DeleteItemFromCart(int id)
        {
            _cartRepository = new RepoClass();
             _cartRepository.DeleteItemFromCart(id);
        }

        public void UpdateStatusInCart(int id)
        {
            _cartRepository = new RepoClass();
            _cartRepository.UpdateStatusInCart(id);
        }
    }
}
