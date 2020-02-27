using Domains.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetEmployeesAsync();
        Task<EmployeeDto> GetEmployeeAsync(Guid id);
        Task AddEmployeeAsync(EmployeeDto employeeDto);
        Task UpdateEmployeeAsync(EmployeeDto employeeDto);
        Task DeleteEmployeeAsync(Guid id);
    }
}