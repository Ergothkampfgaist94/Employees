using Employees.Backend.Data;
using Employees.Backend.Helpers;
using Employees.Backend.Repositories.Interfaces;
using Employees.Shared.DTOs;
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

    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
    {
        var countries = await _context.Countries
            .OrderBy(x => x.Name)
            .Include(c => c.States)
            .ToListAsync();
        return new ActionResponse<IEnumerable<Country>>
        {
            IsSuccess = true,
            Result = countries
        };
    }

    public override async Task<ActionResponse<Country>> GetAsync(int id)
    {
        var country = await _context.Countries
             .Include(c => c.States!)
             .ThenInclude(s => s.Cities)
             .FirstOrDefaultAsync(c => c.Id == id);

        if (country == null)
        {
            return new ActionResponse<Country>
            {
                IsSuccess = false,
                Message = "País no existe"
            };
        }

        return new ActionResponse<Country>
        {
            IsSuccess = true,
            Result = country
        };
    }

    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Countries
            .Include(c => c.States)
            .AsQueryable();
        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }
        return new ActionResponse<IEnumerable<Country>>
        {
            IsSuccess = true,
            Result = await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync()
        };
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.Countries.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            IsSuccess = true,
            Result = (int)count
        };
    }
}