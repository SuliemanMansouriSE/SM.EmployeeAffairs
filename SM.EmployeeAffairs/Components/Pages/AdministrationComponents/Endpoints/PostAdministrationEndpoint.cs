using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SM.EmployeeAffairs.Data;
using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Components.Pages.AdministrationComponents.Endpoints
{
    public static class PostAdministrationEndpoint
    {
        public static void MapPostAdministrationEndpoint(this IEndpointRouteBuilder adminApi)
        {
            adminApi.MapPost("/", async ([FromServices] IDbContextFactory<ApplicationDbContext> dbContextFactory, Administration administration) =>
            {
                var db = dbContextFactory.CreateDbContext();
                await db.Administrations.AddAsync(administration);
                await db.SaveChangesAsync();
                return Results.Created($"/api/administrations/{administration.Id}", administration);
            });
        }
    }
}
