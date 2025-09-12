using Employees.Backend.Data;
using Employees.Backend.Repositories.Implementations;
using Employees.Backend.Repositories.Interfaces;
using Employees.Backend.UnitOfWork.Implementations;
using Employees.Backend.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalDb"));
builder.Services.AddTransient<SeedDB>();

builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IEmployeeUnitOfWork, EmployeeUnitOfWork>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();

SeedDatabase(app);

void SeedDatabase(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using var scope = scopedFactory!.CreateScope();
    var services = scope.ServiceProvider.GetService<SeedDB>();
    services!.SeedAsync().Wait();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();