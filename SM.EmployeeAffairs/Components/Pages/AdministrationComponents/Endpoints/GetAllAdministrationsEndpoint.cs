using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using SM.EmployeeAffairs.Data;
using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Components.Pages.AdministrationComponents.Endpoints
{
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
}
