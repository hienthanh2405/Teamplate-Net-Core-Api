using AutoMapper;
using Domains;
using Domains.DTOs;
using Domains.Entities;
using Domains.Repositories;
using Services.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Company> _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IMapper mapper,
            IRepository<Company> companyRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CompanyDto>> GetCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            var companyDtos =_mapper.Map<List<CompanyDto>>(companies.ToList()).OrderBy(c => c.Name).ToList();

            return companyDtos;
        }

        public async Task<CompanyDto> GetCompanyAsync(Guid id)
        {
            var company = await _companyRepository.FindOneAsync(c => c.Id == id);
            var companyDto = _mapper.Map<CompanyDto>(company);

            return companyDto;
        }

        public async Task AddCompanyAsync(CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);
            company.CreatedDate = DateTime.Now;
            company.UpdatedDate = DateTime.Now;

            await _companyRepository.AddAsync(company);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task UpdateCompanyAsync(CompanyDto companyDto)
        {
            var company = await _companyRepository.FindOneAsync(c => c.Id == companyDto.Id);
            if (company == null)
            {
                return;
            }

            company.Name = companyDto.Name;
            company.Code = companyDto.Code;
            company.Address = companyDto.Address;
            company.PostCode = companyDto.PostCode;
            company.UpdatedDate = DateTime.Now;

            await _companyRepository.UpdateAsync(company);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task DeleteAsync(Guid companyId)
        {
            if (await IsExistedCompany(companyId) == false)
            {
                return;
            }

            await _companyRepository.DeleteAsync(x => x.Id == companyId);
            await _unitOfWork.CommitChangesAsync();
        }

        private async Task<bool> IsExistedCompany(Guid companyId)
        {
            var existedCompany = await _companyRepository.FindOneAsync(c => c.Id == companyId);

            return existedCompany != null;
        }
    }
}