using Employees.Backend.Data;
using Employees.Backend.Helpers;
using Employees.Backend.Repositories.Interfaces;
using Employees.Shared.DTOs;
using Employees.Shared.Entities;
using Employees.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Employees.Backend.Repositories.Implementations;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    private readonly DataContext _context;

    public EmployeeRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ActionResponse<IEnumerable<string>>> GetEmployeeNamesAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return new ActionResponse<IEnumerable<string>>
            {
                Message = "The text is required."
            };
        }
        var rows = await _context.Employees
            .Where(x => x.LastName.Contains(text) ||
            x.FirstName.Contains(text))
            .ToListAsync();
        return new ActionResponse<IEnumerable<string>>
        {
            IsSuccess = true,
            Result = rows.Select(x => $"{x.FirstName} {x.LastName}")
        };
    }

    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => 
            x.FirstName.ToLower().Contains(pagination.Filter.ToLower()) /*|| 
            x.LastName.ToLower().Contains(pagination.Filter.ToLower())*/);
        }

        return new ActionResponse<IEnumerable<Employee>>
        {
            IsSuccess = true,
            Result = await queryable
                .OrderBy(c => c.FirstName)
                .Paginate(pagination)
                .ToListAsync()
        };
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.Employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x =>
            x.FirstName.ToLower().Contains(pagination.Filter.ToLower()) /*||
            x.LastName.ToLower().Contains(pagination.Filter.ToLower())*/);
        }

        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            IsSuccess = true,
            Result = (int)count
        };
    }
}