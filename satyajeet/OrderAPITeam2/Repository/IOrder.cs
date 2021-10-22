using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OrderAPITeam2.CakeModel;

namespace OrderAPI.Repository
{
    interface IOrder
    {
        public void AddingToOrderDetails(OrderDetail ord);
        public List<OrderDetail> OrderdetailsbyCustomerId(int id);
        public List<Occasion> GetAllOccasion();

        public List<OrderDetail> AllOrders();

        public void UpdateStatus(OrderDetail temp);

        public OrderDetail OrderByOrderID(int id);
    }
}
