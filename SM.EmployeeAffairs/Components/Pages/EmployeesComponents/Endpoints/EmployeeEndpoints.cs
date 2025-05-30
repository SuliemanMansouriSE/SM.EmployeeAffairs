using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using SM.EmployeeAffairs.Data;
using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Components.Pages.EmployeesComponents.Endpoints
{
    public static class EmployeeEndpoints
    {
        public static void MapEmployeeEndpoints(this IEndpointRouteBuilder app)
        {
            var empApi = app.MapGroup("/api/employees");
            empApi.MapGet("/by-administration/{administrationId:guid}", async ([FromServices] IDbContextFactory<ApplicationDbContext> dbContextFactory, Guid administrationId) =>
            {
                var db = dbContextFactory.CreateDbContext();
                return await db.Employees
                .Where(e => e.AdministrationId == administrationId)
                .ToListAsync();
            });
            empApi.MapDelete("/{id:guid}", async ([FromServices] IDbContextFactory<ApplicationDbContext> dbContextFactory, Guid id) =>
            {
                var db = dbContextFactory.CreateDbContext();
                var employee = await db.Employees.FirstOrDefaultAsync(e => e.Id == id);
                if (employee is null) return Results.NotFound();
                db.Employees.Remove(employee);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            empApi.MapPost("/", async ([FromServices] IDbContextFactory<ApplicationDbContext> dbContextFactory, Employee employee) =>
            {
                var db = dbContextFactory.CreateDbContext();
                var tmp = db.Employees.FirstOrDefault(x => x.Id == employee.Id);
                if (tmp is null)
                {
                    await db.Employees.AddAsync(employee);

                }
                else
                {
                    db.Employees.Update(employee);
                }
                await db.SaveChangesAsync();
                return Results.Created($"/api/employees/{employee.Id}", employee);
            });
        }
    }
}
