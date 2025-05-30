using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SM.EmployeeAffairs.Data;
using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Components.Pages.EmployeesComponents.Endpoints;

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
        
        empApi.MapGet("/{Id:Guid}", async ([FromServices] IDbContextFactory<ApplicationDbContext> dbContextFactory, Guid Id) =>
        {
            var db = dbContextFactory.CreateDbContext();
            return await db.Employees
            .FirstOrDefaultAsync(e => e.Id == Id) is { } employee ? Results.Ok(employee) : Results.NotFound();
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
            try
            {
                await db.Employees.AddAsync(employee);
                await db.SaveChangesAsync();
                return Results.Created($"/api/employees/{employee.Id}", employee);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                // 2601: Cannot insert duplicate key row in object
                // 2627: Violation of unique constraint
                return Results.BadRequest("ÌÊÃœ „ÊŸ› »‰›” —ﬁ„ «·Â« › Ê «·»—Ìœ «·≈·ﬂ —Ê‰Ì Ê «·—ﬁ„ «·Êÿ‰Ì.");
            }
            catch (Exception)
            {
                return Results.BadRequest("ÕœÀ Œÿ√ √À‰«¡ Õ›Ÿ «·„ÊŸ›.");
            }
        });
    }
}
