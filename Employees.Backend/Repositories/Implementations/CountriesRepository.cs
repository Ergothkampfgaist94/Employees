using Employees.Backend.Data;
using Employees.Backend.Repositories.Interfaces;
using Employees.Shared.Entities;
using Employees.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Employees.Backend.Repositories.Implementations;

public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
{
    private readonly DataContext _context;

    public CountriesRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public Task<ActionResponse<Country>> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResponse<IEnumerable<Country>>> GetAsync()
    {
        throw new NotImplementedException();
    }
}