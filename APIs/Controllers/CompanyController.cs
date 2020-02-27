using Domains.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Contacts;
using System;
using System.Threading.Tasks;

namespace APIs.Controllers
{
    [Route("api/companies/")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var result = await _companyService.GetCompaniesAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _companyService.GetCompanyAsync(id);

            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CompanyDto companyDto)
        {
            await _companyService.AddCompanyAsync(companyDto);

            return Ok();
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<IActionResult> Update([FromBody] CompanyDto companyDto)
        {
            await _companyService.UpdateCompanyAsync(companyDto);

            return Ok();
        }

        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _companyService.DeleteAsync(id);

            return Ok();
        }
    }
}