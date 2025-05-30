namespace SM.EmployeeAffairs.Components.Pages.AdministrationComponents.Endpoints
{
    public static class AdministrationEndpoints
    {
        public static void MapAdministrationEndpoints(this IEndpointRouteBuilder app)
        {
            var adminApi = app.MapGroup("/api/administrations");
            adminApi.MapGetAllAdministrationsEndpoint();
            adminApi.MapGetAdministrationByIdEndpoint();
            adminApi.MapPostAdministrationEndpoint();
            adminApi.MapPutAdministrationEndpoint();
            adminApi.MapDeleteAdministrationEndpoint();
            // Register employee endpoints (updated namespace)
            SM.EmployeeAffairs.Components.Pages.EmployeesComponents.Endpoints.EmployeeEndpoints.MapEmployeeEndpoints(app);
        }
    }
}
