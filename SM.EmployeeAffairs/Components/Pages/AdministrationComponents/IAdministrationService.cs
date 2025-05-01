using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Components.Pages.AdministrationComponents
{
    public interface IAdministrationService
    {
        Task DeleteAsync(Administration administration);
        Task<Administration?> GetAdministrationById(Guid id);
        Task<List<Administration>> GetAdministrations();
        Task<Administration> Upsert(Administration administration);
        
    }
}