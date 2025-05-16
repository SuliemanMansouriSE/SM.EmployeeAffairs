using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using SM.EmployeeAffairs.Data;
using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Components.Pages.AdministrationComponents.Endpoints
{
    public static class DeleteAdministrationEndpoint
    {
        public static void MapDeleteAdministrationEndpoint(this IEndpointRouteBuilder adminApi)
        {
            adminApi.MapDelete("/{id:guid}", async ([FromServices] IDbContextFactory<ApplicationDbContext> dbContextFactory, Guid id) =>
            {
                var db = dbContextFactory.CreateDbContext();
                var existing = await db.Administrations.FirstOrDefaultAsync(a => a.Id == id);
                if (existing is null) return Results.NotFound();
                db.Administrations.Remove(existing);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
