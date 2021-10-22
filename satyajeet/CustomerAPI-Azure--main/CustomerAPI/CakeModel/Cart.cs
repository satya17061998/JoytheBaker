using System;
using System.Collections.Generic;

#nullable disable

namespace CustomerAPI.CakeModel
{
    public partial class Cart
    {
        public Cart()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int CartId { get; set; }
        public int? CustomerId { get; set; }
        public int? CakeId { get; set; }
        public int? Quantity { get; set; }
        public double? ItemPrice { get; set; }
        public string Status { get; set; }

        public virtual Cake Cake { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
