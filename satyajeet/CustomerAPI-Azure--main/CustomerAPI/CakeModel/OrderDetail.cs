using System;
using System.Collections.Generic;

#nullable disable

namespace CustomerAPI.CakeModel
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int? CakeId { get; set; }
        public int? CustomerId { get; set; }
        public int? CartId { get; set; }
        public double? Totalprice { get; set; }
        public string Remark { get; set; }
        public string PaymentStatus { get; set; }

        public virtual Cake Cake { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
