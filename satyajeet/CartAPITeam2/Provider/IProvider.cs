using CartAPITeam2.CakeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartAPI.Provider
{
   public interface IProvider
    {
        public List<Cart> CartbyCustID(int id);

        public void DeleteItemFromCart(int id);
        public void AdditemtoCart(Cart temp);

        public void UpdateStatusInCart(int id);

        public Cart CartByCartID(int id);
    }
}
