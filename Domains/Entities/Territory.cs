using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Entities
{
    public class Territory :AbstractEntity
    {
        public string Description { get; set; }
        public Region Region { get; set; }
    }
}
