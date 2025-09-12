using Microsoft.EntityFrameworkCore;
using Employees.Backend.Data;
using Employees.Backend.Repositories.Interfaces;
using Employees.Shared.Entities;
using Employees.Shared.Responses;

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
}