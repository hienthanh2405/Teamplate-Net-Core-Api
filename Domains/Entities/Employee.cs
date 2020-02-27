using System;

namespace Domains.Entities
{
    public class Employee : AbstractEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }

        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}