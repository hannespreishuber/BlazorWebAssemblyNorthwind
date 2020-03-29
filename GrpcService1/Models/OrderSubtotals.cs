using System;
using System.Collections.Generic;

namespace GrpcService1.Models
{
    public partial class OrderSubtotals
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
