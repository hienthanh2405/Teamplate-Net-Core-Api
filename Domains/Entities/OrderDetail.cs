using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Entities
{
    public class OrderDetail :AbstractEntity
    {
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public Order Order { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
