using Domains.DTOs;
using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface ICompanyService
    {
        Task<List<CompanyDto>> GetCompaniesAsync();
        Task<CompanyDto> GetCompanyAsync(Guid id);
        Task AddCompanyAsync(CompanyDto companyDto);
        Task UpdateCompanyAsync(CompanyDto companyDto);
        Task DeleteAsync(Guid companyId);
    }
}