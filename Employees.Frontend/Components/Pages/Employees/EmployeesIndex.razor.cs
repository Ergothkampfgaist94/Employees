using Employees.Frontend.Repositories;
using Employees.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace Employees.Frontend.Components.Pages.Employees;

public partial class EmployeesIndex
{
    [Inject] private IRepository Repository { get; set; } = null!;
    private List<Employee>? Employees;

    protected override async Task OnInitializedAsync()
    {
        var httpResult = await Repository.GetAsync<List<Employee>>("/api/Employees");
        Thread.Sleep(3000);
        Employees = httpResult.Response;
    }
}