using AutoMapper;
using Domains;
using Domains.DTOs;
using Domains.Entities;
using Domains.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Company> _companyRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IMapper mapper,
            IRepository<Company> companyRepository,
            IRepository<Employee> employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<EmployeeDto>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.DbSet.Include(x => x.Company).ToListAsync();
            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees).OrderBy(e => e.FirstName).ToList();

            return employeeDtos;
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid id)
        {
            var employee = await _employeeRepository.FindOneAsync(e => e.Id == id);
            employee.Company = await _companyRepository.FindOneAsync(e => e.Id == employee.CompanyId);

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            employeeDto.Company.Name = employee.Company.Name;
            employeeDto.Company.Code = employee.Company.Code;
            employeeDto.Company.Address = employee.Company.Address;
            employeeDto.Company.PostCode = employee.Company.PostCode;

            return employeeDto;
        }

        public async Task AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.AddAsync(employee);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = await _employeeRepository.FindOneAsync(e => e.Id == employeeDto.Id);
            if (employee == null)
            {
                return;
            }

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.DateOfBirth = employeeDto.DateOfBirth;
            employee.PhoneNumber = employeeDto.PhoneNumber;
            employee.Address = employeeDto.Address;
            employee.PostCode = employeeDto.PostCode;
            employee.CompanyId = employeeDto.CompanyId;

            await _employeeRepository.UpdateAsync(employee);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            if (await IsExistedEmployee(id) == false)
            {
                return;
            }

            await _employeeRepository.DeleteAsync(e => e.Id == id);
            await _unitOfWork.CommitChangesAsync();
        }

        private async Task<bool> IsExistedEmployee(Guid employeeId)
        {
            var existedEmployee = await _employeeRepository.FindOneAsync(e => e.Id == employeeId);

            return existedEmployee != null;
        }
    }
}