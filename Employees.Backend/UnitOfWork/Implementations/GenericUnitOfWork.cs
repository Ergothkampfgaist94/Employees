using Employees.Backend.Repositories.Interfaces;
using Employees.Backend.UnitOfWork.Interfaces;
using Employees.Shared.DTOs;
using Employees.Shared.Responses;

namespace Employees.Backend.UnitOfWork.Implementations;

public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
{
    private readonly IGenericRepository<T> _repository;

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination) =>
        await _repository.GetAsync(pagination);

    public virtual async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) =>
        await _repository.GetTotalRecordsAsync(pagination);

    public GenericUnitOfWork(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public virtual async Task<ActionResponse<T>> AddAsync(T entity) =>
        await _repository.AddAsync(entity);

    public virtual async Task<ActionResponse<T>> DeleteAsync(int id) =>
        await _repository.DeleteAsync(id);

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAllAsync() =>
        await _repository.GetAllAsync();

    public virtual async Task<ActionResponse<T>> GetByIdAsync(int id) =>
        await _repository.GetByIdAsync(id);

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync() =>
        await _repository.GetAsync();

    public virtual async Task<ActionResponse<T>> GetAsync(int id) =>
        await _repository.GetAsync(id);

    public virtual async Task<ActionResponse<T>> UpdateAsync(T entity) =>
        await _repository.UpdateAsync(entity);
}