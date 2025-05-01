using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Components.Pages.AdministrationComponents
{
    public interface IAdministrationServices
    {
        void Delete(Administration administration);
        Administration GetAdministrationById(Guid id);
        List<Administration> GetAdministrations();
        Administration Save(Administration administration);
        Administration Update(Administration administration);
    }
}