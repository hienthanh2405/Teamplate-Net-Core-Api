using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Entities
{
    public class Category : AbstractEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
    }
}
