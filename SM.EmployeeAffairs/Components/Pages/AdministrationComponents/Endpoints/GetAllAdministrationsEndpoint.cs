using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SM.EmployeeAffairs.Data;

namespace SM.EmployeeAffairs.Components.Pages.AdministrationComponents.Endpoints;

public static class GetAllAdministrationsEndpoint
{
    public static void MapGetAllAdministrationsEndpoint(this IEndpointRouteBuilder adminApi)
    {
        adminApi.MapGet("/", async ([FromServices] IDbContextFactory<ApplicationDbContext> dbContextFactory) =>
        {
            var db = dbContextFactory.CreateDbContext();
            return await db.Administrations.ToListAsync();
        });
    }
}
