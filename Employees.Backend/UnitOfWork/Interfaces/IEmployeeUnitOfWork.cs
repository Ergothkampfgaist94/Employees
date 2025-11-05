using Employees.Shared.DTOs;
using Employees.Shared.Entities;
using Employees.Shared.Responses;

namespace Employees.Backend.UnitOfWork.Interfaces
{
    public interface IEmployeeUnitOfWork
    {
        Task<ActionResponse<IEnumerable<string>>> GetEmployeeNamesAsync(string text);
        Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO Pagination);
        Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination);
    }
}