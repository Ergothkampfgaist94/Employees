using Employees.Backend.Repositories.Interfaces;
using Employees.Backend.UnitOfWork.Interfaces;
using Employees.Shared.Entities;
using Employees.Shared.Responses;
using Microsoft.AspNetCore.Components;

namespace Employees.Backend.UnitOfWork.Implementations;

public class EmployeeUnitOfWork : GenericUnitOfWork<Employee>, IEmployeeUnitOfWork
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeUnitOfWork(IGenericRepository<Employee> repository, IEmployeeRepository employeeRepository) : base(repository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<ActionResponse<IEnumerable<string>>> GetEmployeeNamesAsync(string text) =>
        await _employeeRepository.GetEmployeeNamesAsync(text);
}