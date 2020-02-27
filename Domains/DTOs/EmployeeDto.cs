using System;
using Utilities.Extensions;

namespace Domains.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }

        public string DoB
        {
            get => DateOfBirth.ToDateFormat();
        }

        public Guid CompanyId { get; set; }
        public CompanyDto Company { get; set; }
    }
}