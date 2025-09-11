using Employees.Shared.Responses;

namespace Employees.Backend.UnitOfWork.Interfaces;

public interface IGenericUnitOfWork<T> where T : class
{
    Task<ActionResponse<T>> GetByIdAsync(int id);

    Task<ActionResponse<T>> GetByNameAsync(string text);

    Task<ActionResponse<IEnumerable<T>>> GetAllAsync();

    Task<ActionResponse<T>> AddAsync(T entity);

    Task<ActionResponse<T>> UpdateAsync(T entity);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}