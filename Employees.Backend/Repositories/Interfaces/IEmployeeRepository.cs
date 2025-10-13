using Employees.Shared.DTOs;
using Employees.Shared.Responses;

namespace Employees.Backend.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<ActionResponse<IEnumerable<string>>> GetEmployeeNamesAsync(string text);

    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO Pagination);
}