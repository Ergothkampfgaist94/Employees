using Employees.Backend.Repositories.Interfaces;
using Employees.Backend.UnitOfWork.Interfaces;
using Employees.Shared.DTOs;
using Employees.Shared.Entities;
using Employees.Shared.Responses;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.Metrics;

namespace Employees.Backend.UnitOfWork.Implementations;

public class EmployeeUnitOfWork : GenericUnitOfWork<Employee>, IEmployeeUnitOfWork
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeUnitOfWork(IGenericRepository<Employee> repository, IEmployeeRepository employeeRepository) : base(repository)
    {
        _employeeRepository = employeeRepository;
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) =>
        await _employeeRepository.GetTotalRecordsAsync(pagination);

    public async Task<ActionResponse<IEnumerable<string>>> GetEmployeeNamesAsync(string text) =>
        await _employeeRepository.GetEmployeeNamesAsync(text);
    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination) => 
        await _employeeRepository.GetAsync(pagination);
}