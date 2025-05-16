using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Administration> Administrations { get; set; } = default!;
        public DbSet<Employee> Employees { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Administration>(entity =>
            {
                entity.ToTable("Administration");

                entity.HasKey(a => a.Id);

                entity.Property(a => a.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(a => a.Description)
                    .HasMaxLength(1000);

                entity.HasMany(a => a.Employees)
                    .WithOne(e => e.Administration)
                    .HasForeignKey(e => e.AdministrationId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasKey(a => a.Id);

                entity.Property(a => a.NameAr)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(a => a.NameEng)
                    .HasMaxLength(100);

                entity.HasIndex(e => e.NameAr);

                entity.HasIndex(e => new { e.Phone, e.Email, e.NID })
                    .IsUnique();
            });
        }
    }
}
