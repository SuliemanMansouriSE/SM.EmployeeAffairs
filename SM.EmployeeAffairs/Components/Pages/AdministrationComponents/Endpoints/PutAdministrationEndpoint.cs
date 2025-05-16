using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using SM.EmployeeAffairs.Data;
using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Components.Pages.AdministrationComponents.Endpoints
{
    public static class PutAdministrationEndpoint
    {
        public static void MapPutAdministrationEndpoint(this IEndpointRouteBuilder adminApi)
        {
            adminApi.MapPut("/{id:guid}", async ([FromServices] IDbContextFactory<ApplicationDbContext> dbContextFactory, Guid id, Administration administration) =>
            {
                var db = dbContextFactory.CreateDbContext();
                var existing = await db.Administrations.FirstOrDefaultAsync(a => a.Id == id);
                if (existing is null) return Results.NotFound();
                existing.Name = administration.Name;
                existing.Description = administration.Description;
                db.Administrations.Update(existing);
                await db.SaveChangesAsync();
                return Results.Ok(existing);
            });
        }
    }
}
