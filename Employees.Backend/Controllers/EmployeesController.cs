using Employees.Backend.UnitOfWork.Interfaces;
using Employees.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : GenericController<Employee>
{
    private readonly IEmployeeUnitOfWork _employeeUnitOfWork;

    public EmployeesController(IGenericUnitOfWork<Employee> unitOfWork, IEmployeeUnitOfWork employeeUnitOfWork) : base(unitOfWork)
    {
        _employeeUnitOfWork = employeeUnitOfWork;
    }

    [HttpGet("GetEmployeeNames/{text}")]
    public async Task<IActionResult> GetEmployeeNamesAsync(string text)
    {
        var response = await _employeeUnitOfWork.GetEmployeeNamesAsync(text);
        if (response.IsSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest(response.Message);
    }
}