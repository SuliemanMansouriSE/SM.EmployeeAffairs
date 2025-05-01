namespace SM.EmployeeAffairs.Data.Entities
{
    public class Administration
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Employee> Employees { get; set; } = [];
    }
}
