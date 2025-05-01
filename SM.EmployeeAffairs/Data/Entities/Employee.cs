namespace SM.EmployeeAffairs.Data.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEng { get; set; }
        public int EmployementNumber { get; set; }
        public string NID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid AdministrationId { get; set; }
        public Administration Administration { get; set; } = new();
    }
}
