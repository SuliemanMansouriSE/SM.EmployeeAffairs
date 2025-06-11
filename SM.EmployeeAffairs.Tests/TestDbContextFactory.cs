using Microsoft.EntityFrameworkCore;
using SM.EmployeeAffairs.Data;
using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Tests
{
    public class TestDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        private DbContextOptions<ApplicationDbContext> _options;
        public TestDbContextFactory(string databaseName = "InMemoryTest")
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        public ApplicationDbContext CreateDbContext()
        {
            var db = new ApplicationDbContext(_options);
            db.Database.EnsureCreated();
            SeedDatabase(db);
            return db;
        }

        private void SeedDatabase(ApplicationDbContext db)
        {
            // Add test data for Administrations
            var admin = new Administration
            {
                Id = Guid.NewGuid(),
                Name = "Test Administration",
                Description = "Test Description"
            };

            db.Administrations.Add(admin);

            // Add test data for Employees
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                NameAr = "Test Employee",
                NameEng = "Test Employee",
                NID = "123456789",
                Phone = "1234567890",
                Email = "test@example.com",
                PassportNumber = "A1234567",
                AdministrationId = admin.Id
            };

            db.Employees.Add(employee);

            db.SaveChanges();
        }
    }
}
