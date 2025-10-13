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
            var random = new Random();
            var firstNames = new[]
            {
            "John", "Jane", "Carlos", "Maria", "David", "Laura",
            "Andres", "Sofia", "Michael", "Daniela", "Pedro", "Lucia",
            "Camila", "Sebastian", "Valentina", "Diego", "Isabella",
            "Felipe", "Paula", "Mateo"
        };

            var lastNames = new[]
            {
            "Gomez", "Lopez", "Ramirez", "Torres", "Martinez", "Gonzalez",
            "Rodriguez", "Perez", "Garcia", "Fernandez", "Castro",
            "Hernandez", "Diaz", "Jimenez", "Moreno", "Ruiz", "Silva",
            "Vargas", "Rojas", "Ortega"
        };

            var employees = new List<Employee>();

            for (int i = 1; i <= 60; i++)
            {
                string firstName = firstNames[random.Next(firstNames.Length)];
                string lastName = lastNames[random.Next(lastNames.Length)];
                bool isActive = random.Next(0, 2) == 1;
                decimal salary = random.Next(1_500_000, 3_500_000);
                DateTime hireDate = DateTime.UtcNow.AddDays(-random.Next(0, 1000));

                employees.Add(new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    IsActive = isActive,
                    HireDate = hireDate,
                    Salary = salary
                });
            }

            _context.Employees.AddRange(employees);
            await _context.SaveChangesAsync();
        }
    }

}