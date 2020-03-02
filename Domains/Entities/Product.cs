using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Entities
{
    public class Product : AbstractEntity
    {
        public string ProductName { get; set; }
        public string QuatityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public double UnitsInStock { get; set; }
        public int UnitsInOrder { get; set; }
        public int ReorderLevel { get; set; }
        public int Discountinued { get; set; }
        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
    }
}
