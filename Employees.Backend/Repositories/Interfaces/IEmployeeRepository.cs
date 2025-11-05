using Employees.Shared.DTOs;
using Employees.Shared.Entities;
using Employees.Shared.Responses;
using System.Diagnostics.Metrics;

namespace Employees.Backend.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<ActionResponse<IEnumerable<string>>> GetEmployeeNamesAsync(string text);

    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO Pagination);

    Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination);
}