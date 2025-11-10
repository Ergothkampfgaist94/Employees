using Employees.Backend.UnitOfWork.Interfaces;
using Employees.Shared.Entities;
using Employees.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Employees.Backend.Data;

public class SeedDB
{
    private readonly DataContext _context;
    private readonly IUsersUnitOfWork _usersUnitOfWork;

    public SeedDB(DataContext context, IUsersUnitOfWork usersUnitOfWork)
    {
        _context = context;
        _usersUnitOfWork = usersUnitOfWork;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckEmployeesAsync();
        await CheckCountriesFullAsync();
        await CheckRolesAsync();
        await CheckUserAsync("1010",
            "Andrés",
            "López",
            "alopez@yopmail.com",
            "3054358079",
            "Calle 1 carrera 1",
            UserType.Admin);
    }

    private async Task CheckRolesAsync()
    {
        await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
        await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
    }

    private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
    {
        var user = await _usersUnitOfWork.GetUserAsync(email);
        if (user == null)
        {
            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Address = address,
                Document = document,
                City = _context.Cities.FirstOrDefault(),
                UserType = userType,
            };

            await _usersUnitOfWork.AddUserAsync(user, "123456");
            await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());
        }

        return user;
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

    private async Task CheckCountriesFullAsync()
    {
        if (!_context.Countries.Any())
        {
            var countriesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
            await _context.Database.ExecuteSqlRawAsync(countriesSQLScript);
        }
    }
}