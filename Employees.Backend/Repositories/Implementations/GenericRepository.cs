using Employees.Backend.Data;
using Employees.Backend.Repositories.Interfaces;
using Employees.Shared.DTOs;
using Employees.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using Employees.Backend.Helpers;

namespace Employees.Backend.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DataContext _context;
    private readonly DbSet<T> _entity;

    public GenericRepository(DataContext context)
    {
        _context = context;
        _entity = _context.Set<T>();
    }

    public virtual async Task<ActionResponse<T>> AddAsync(T entity)
    {
        _context.Add(entity);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                IsSuccess = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResposnse();
        }
        catch (Exception ex)
        {
            return ExceptionActionResponse(ex);
        }
    }

    public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponse<T>
            {
                Message = "Not found."
            };
        }
        _entity.Remove(row);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                IsSuccess = true
            };
        }
        catch (Exception)
        {
            return new ActionResponse<T>
            {
                Message = "There was a problem deleting the record. Please try again."
            };
        }
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAllAsync() =>
        new ActionResponse<IEnumerable<T>>
        {
            IsSuccess = true,
            Result = await _entity.ToListAsync()
        };

    public virtual async Task<ActionResponse<T>> GetByIdAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponse<T>
            {
                Message = "Not found."
            };
        }
        return new ActionResponse<T>
        {
            IsSuccess = true,
            Result = row
        };
    }

    public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
    {
        _context.Update(entity);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                IsSuccess = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResposnse();
        }
        catch (Exception ex)
        {
            return ExceptionActionResponse(ex);
        }
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _entity.AsQueryable();

        return new ActionResponse<IEnumerable<T>>
        {
            IsSuccess = true,
            Result = await queryable
                .Paginate(pagination)
                .ToListAsync()
        };
    }

    public virtual async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _entity.AsQueryable();
        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            IsSuccess = true,
            Result = (int)count
        };
    }

    private ActionResponse<T> DbUpdateExceptionActionResposnse() => new ActionResponse<T>
    {
        Message = "There was a problem saving the information. Please try again."
    };

    private ActionResponse<T> ExceptionActionResponse(Exception ex) => new ActionResponse<T>
    {
        Message = "There was a problem: " + ex.Message
    };
}