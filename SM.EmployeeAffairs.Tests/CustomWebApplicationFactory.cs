using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SM.EmployeeAffairs.Data;
using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {

        protected override void ConfigureClient(HttpClient client)
        {
            // Use the in-memory server's base address
            client.BaseAddress = this.Server.BaseAddress; // or use the value from the test server
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove old DbContext registration
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IDbContextOptionsConfiguration<ApplicationDbContext>));
                if (dbContextDescriptor != null)
                    services.Remove(dbContextDescriptor);

                // Remove any provider-specific services (e.g., SqlServer)
                var providerDescriptors = services
                    .Where(d => d.ImplementationType?.Assembly.FullName?.Contains("Microsoft.EntityFrameworkCore.SqlServer") == true)
                    .ToList();
                foreach (var descriptor in providerDescriptors)
                    services.Remove(descriptor);

                // Remove DbContextFactory if registered
                var dbContextFactoryDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IDbContextFactory<ApplicationDbContext>));
                if (dbContextFactoryDescriptor != null)
                    services.Remove(dbContextFactoryDescriptor);


                // Register in-memory DbContext (no internal service provider)
                services.AddDbContextFactory<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                // Seed data using dependency injection
                
                builder.ConfigureServices(services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    db.Database.EnsureCreated();

                    db.Administrations.Add(new Administration { Id = Guid.NewGuid(), Name = "Seed Administration", Description = "Seed Description" });
                    db.SaveChanges();
                });
            });
        }

        


    }
}
