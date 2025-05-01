using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {

        public DbSet<Administration> Administrations { get; set; } = default!;
        public DbSet<Employee> Employees { get; set; } = default!;
    }
}
