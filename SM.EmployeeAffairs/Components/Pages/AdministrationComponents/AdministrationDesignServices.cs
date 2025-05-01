using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Components.Pages.AdministrationComponents
{
    public class AdministrationDesignServices : IAdministrationServices
    {
        public List<Administration> GetAdministrations()
        {

            return
            [
                new Administration
                {
                    Id = Guid.NewGuid(),
                    Name = "Administration 1",
                    Description = "وصف مختصر لعمل الادارة"
                },
                new Administration
                {
                    Id = Guid.NewGuid(),
                    Name = "Administration 2",
                    Description = "Description for Administration 2"
                }
            ];
        }
        public Administration GetAdministrationById(Guid id)
        {
            // Simulate fetching the administration from a database
            // In a real application, you would use a database context to fetch the entity
            return new Administration
            {
                Id = id,
                Name = "Administration 1",
                Description = "وصف مختصر لعمل الادارة"
            };
        }
        public Administration Save(Administration administration)
        {
            // Simulate saving the administration to a database
            // In a real application, you would use a database context to save the entity
            return administration;
        }
        public void Delete(Administration administration)
        {
            // Simulate deleting the administration from a database
            // In a real application, you would use a database context to delete the entity
        }
        public Administration Update(Administration administration)
        {
            // Simulate updating the administration in a database
            // In a real application, you would use a database context to update the entity
            return administration;
        }
    }
}
