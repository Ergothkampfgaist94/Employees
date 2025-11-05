using Employees.Backend.Data;
using Employees.Backend.Repositories.Interfaces;
using Employees.Shared.Entities;
using Employees.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Employees.Backend.Repositories.Implementations;

public class StatesRepository : GenericRepository<State>, IStatesRepository
{
    private readonly DataContext _context;

    public StatesRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public Task<ActionResponse<State>> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResponse<IEnumerable<State>>> GetAsync()
    {
        throw new NotImplementedException();
    }
}