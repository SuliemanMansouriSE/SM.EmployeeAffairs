using Microsoft.EntityFrameworkCore;
using SM.EmployeeAffairs.Data;
using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Components.Pages.AdministrationComponents
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public AdministrationService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task DeleteAsync(Administration administration)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            var existingAdministration = _dbContext.Administrations
                .Find(administration.Id);
            if (existingAdministration != null)
            {
                _dbContext.Administrations.Remove(existingAdministration);
                await _dbContext.SaveChangesAsync();
            }
        }

        public Task<Administration?> GetAdministrationById(Guid id)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return _dbContext.Administrations
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task<List<Administration>> GetAdministrations()
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return _dbContext.Administrations.ToListAsync();

        }

        public async Task<Administration> Upsert(Administration administration)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            var existingAdministration = await _dbContext.Administrations
                .FirstOrDefaultAsync(a => a.Id == administration.Id);
            if (existingAdministration != null)
            {
                // Update existing administration
                existingAdministration.Name = administration.Name;
                existingAdministration.Description = administration.Description;
                _dbContext.Administrations.Update(existingAdministration);
            }
            else
            {
                // Add new administration
                await _dbContext.Administrations.AddAsync(administration);
            }
            await _dbContext.SaveChangesAsync();
            return administration;
        }
    }
}
