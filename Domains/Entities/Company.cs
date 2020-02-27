using System.Collections.Generic;

namespace Domains.Entities
{
    public class Company : AbstractEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}