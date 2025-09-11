using Employees.Backend.Data;
using Employees.Backend.Repositories.Interfaces;
using Employees.Shared.Entities;
using Employees.Shared.Responses;
using Microsoft.EntityFrameworkCore;

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

    public async Task<ActionResponse<T>> AddAsync(T entity)
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

    public async Task<ActionResponse<T>> DeleteAsync(int id)
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

    public async Task<ActionResponse<IEnumerable<T>>> GetAllAsync() =>
        new ActionResponse<IEnumerable<T>>
        {
            IsSuccess = true,
            Result = await _entity.ToListAsync()
        };

    public async Task<ActionResponse<T>> GetByIdAsync(int id)
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

    public async Task<ActionResponse<T>> UpdateAsync(T entity)
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

    private ActionResponse<T> DbUpdateExceptionActionResposnse() => new ActionResponse<T>
    {
        Message = "There was a problem saving the information. Please try again."
    };

    private ActionResponse<T> ExceptionActionResponse(Exception ex) => new ActionResponse<T>
    {
        Message = "There was a problem: " + ex.Message
    };
}