using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderAPI.Repository;
using OrderAPITeam2.CakeModel;

namespace OrderAPI.Provider
{
    public class ProviderClass : IProvider
    {
        IOrder _orderRepository;
        public ProviderClass()
        {
        }
        public void AddingToOrderDetails(OrderDetail ord)
        {
            _orderRepository = new RepoClass();
            _orderRepository.AddingToOrderDetails(ord);
        }

        public List<Occasion> GetAllOccasion()
        {
            _orderRepository = new RepoClass();
            return _orderRepository.GetAllOccasion();
        }

        public void UpdateStatus(OrderDetail temp)
        {
            _orderRepository = new RepoClass();
            _orderRepository.UpdateStatus(temp);
        }

        public List<OrderDetail> AllOrders()
        {
            _orderRepository = new RepoClass();
            return _orderRepository.AllOrders();
        }

        public OrderDetail OrderByOrderID(int id)
        {
            _orderRepository = new RepoClass();
            return _orderRepository.OrderByOrderID(id);

        }

        public List<OrderDetail> OrderdetailsbyCustomerId(int id)
        {
          _orderRepository = new RepoClass();
            return _orderRepository.OrderdetailsbyCustomerId(id);
        }
    }
}
