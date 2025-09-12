using Employees.Backend.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Backend.Controllers;

public class GenericController<T> : Controller where T : class
{
    private readonly IGenericUnitOfWork<T> _unitOfWork;

    public GenericController(IGenericUnitOfWork<T> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAllAsync()
    {
        var response = await _unitOfWork.GetAllAsync();
        if (response.IsSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetByIdAsync(int id)
    {
        var response = await _unitOfWork.GetByIdAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound();
    }

    [HttpPost]
    public virtual async Task<IActionResult> PostAsync(T entity)
    {
        var response = await _unitOfWork.AddAsync(entity);
        if (response.IsSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest(response.Message);
    }

    [HttpPut]
    public virtual async Task<IActionResult> PutAsync(T entity)
    {
        var response = await _unitOfWork.UpdateAsync(entity);
        if (response.IsSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest(response.Message);
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(int id)
    {
        var response = await _unitOfWork.DeleteAsync(id);
        if (response.IsSuccess)
        {
            return NoContent();
        }
        return BadRequest(response.Message);
    }
}