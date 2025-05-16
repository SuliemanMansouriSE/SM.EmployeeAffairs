using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using SM.EmployeeAffairs.Data;
using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Components.Pages.AdministrationComponents.Endpoints
{
    public static class GetAdministrationByIdEndpoint
    {
        public static void MapGetAdministrationByIdEndpoint(this IEndpointRouteBuilder adminApi)
        {
            adminApi.MapGet("/{id:guid}", async ([FromServices] IDbContextFactory<ApplicationDbContext> dbContextFactory, Guid id) =>
            {
                var db = dbContextFactory.CreateDbContext();
                return await db.Administrations.FirstOrDefaultAsync(a => a.Id == id) is { } admin ? Results.Ok(admin) : Results.NotFound();
            });
        }
    }
}
