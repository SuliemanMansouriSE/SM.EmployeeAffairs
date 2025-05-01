using SM.EmployeeAffairs.Data.Entities;

namespace SM.EmployeeAffairs.Components.Pages.AdministrationComponents
{
    public class AdministrationDesignServices
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
    }
}
