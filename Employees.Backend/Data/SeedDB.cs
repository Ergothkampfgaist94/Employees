using Employees.Shared.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Employees.Backend.Data;

public class SeedDB
{
    private readonly DataContext _context;

    public SeedDB(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckEmployeesAsync();
    }

    private async Task CheckEmployeesAsync()
    {
        if (!_context.Employees.Any())
        {
            _context.Employees.Add(new Employee
            {
                FirstName = "John",
                LastName = "Doe",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 2000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Jane",
                LastName = "Smith",
                IsActive = true,
                HireDate = DateTime.UtcNow.AddDays(-10),
                Salary = 2500000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Carlos",
                LastName = "Gonzalez",
                IsActive = true,
                HireDate = DateTime.UtcNow.AddDays(-30),
                Salary = 2200000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Maria",
                LastName = "Lopez",
                IsActive = false,
                HireDate = DateTime.UtcNow.AddMonths(-2),
                Salary = 1800000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "David",
                LastName = "Brown",
                IsActive = true,
                HireDate = DateTime.UtcNow.AddMonths(-6),
                Salary = 3000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Laura",
                LastName = "Martinez",
                IsActive = true,
                HireDate = DateTime.UtcNow.AddYears(-1),
                Salary = 2100000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Andres",
                LastName = "Torres",
                IsActive = false,
                HireDate = DateTime.UtcNow.AddYears(-3),
                Salary = 1700000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Sofia",
                LastName = "Ramirez",
                IsActive = true,
                HireDate = DateTime.UtcNow.AddMonths(-8),
                Salary = 2400000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Michael",
                LastName = "Johnson",
                IsActive = true,
                HireDate = DateTime.UtcNow.AddDays(-60),
                Salary = 2600000
            });
            _context.Employees.Add(new Employee
            {
                FirstName = "Michael",
                LastName = "Jackson",
                IsActive = true,
                HireDate = DateTime.UtcNow.AddDays(-300),
                Salary = 2600000
            });
            await _context.SaveChangesAsync();
        }
    }
}