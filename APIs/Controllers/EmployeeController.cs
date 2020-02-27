using Domains.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Contacts;
using System;
using System.Threading.Tasks;

namespace APIs.Controllers
{
    [Route("api/employees/")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var result = await _employeeService.GetEmployeesAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _employeeService.GetEmployeeAsync(id);

            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] EmployeeDto employeeDto)
        {
            await _employeeService.AddEmployeeAsync(employeeDto);

            return Ok();
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<IActionResult> Update([FromBody] EmployeeDto employeeDto)
        {
            await _employeeService.UpdateEmployeeAsync(employeeDto);

            return Ok();
        }

        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _employeeService.DeleteEmployeeAsync(id);

            return Ok();
        }
    }
}