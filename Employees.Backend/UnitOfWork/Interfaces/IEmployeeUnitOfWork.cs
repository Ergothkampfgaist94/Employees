using Employees.Shared.Responses;

namespace Employees.Backend.UnitOfWork.Interfaces
{
    public interface IEmployeeUnitOfWork
    {
        Task<ActionResponse<IEnumerable<string>>> GetEmployeeNamesAsync(string text);
    }
}