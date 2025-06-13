using SM.EmployeeAffairs.Data.Entities;
using System.Net.Http.Json;

namespace SM.EmployeeAffairs.Tests
{
    public class EmployeeEndpointsTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public EmployeeEndpointsTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetEmployeeById_ReturnsNotFound_ForInvalidId()
        {
            var response = await _client.GetAsync($"/api/employees/{Guid.NewGuid()}");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostEmployee_CreatesNewEmployee()
        {
            // Arrange: Create a new administration to associate with the employee
            var newAdmin = new Administration
            {
                Name = "Test Admin",
                Description = "Test Description"
            };

            var adminResponse = await _client.PostAsJsonAsync("/api/administrations", newAdmin);
            adminResponse.EnsureSuccessStatusCode();

            var createdAdmin = await adminResponse.Content.ReadFromJsonAsync<Administration>();
            Assert.NotNull(createdAdmin);

            // Act: Create a new employee with unique values
            var uniqueId = Guid.NewGuid().ToString();
            var newEmployee = new Employee
            {
                NameAr = $"Test Employee {uniqueId}",
                NameEng = $"Test Employee {uniqueId}",
                NID = uniqueId.Substring(0, 9),
                Phone = $"12345{uniqueId.Substring(0, 5)}",
                Email = $"test{uniqueId}@example.com",
                PassportNumber = $"A{uniqueId.Substring(0, 7)}",
                AdministrationId = createdAdmin.Id
            };

            var response = await _client.PostAsJsonAsync("/api/employees", newEmployee);

            // Log response details for debugging
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.True(response.IsSuccessStatusCode, $"Failed to create employee. Status Code: {response.StatusCode}, Response: {responseContent}");

            var createdEmployee = await response.Content.ReadFromJsonAsync<Employee>();
            Assert.NotNull(createdEmployee);
            Assert.Equal(newEmployee.NameAr, createdEmployee.NameAr);
        }

        [Fact]
        public async Task DeleteEmployee_RemovesEmployee()
        {
            // Arrange: Create a new administration to associate with the employee
            var newAdmin = new Administration
            {
                Name = "Admin for Deletion",
                Description = "Description for Deletion"
            };

            var adminResponse = await _client.PostAsJsonAsync("/api/administrations", newAdmin);
            adminResponse.EnsureSuccessStatusCode();

            var createdAdmin = await adminResponse.Content.ReadFromJsonAsync<Administration>();
            Assert.NotNull(createdAdmin);

            // Create a new employee to delete
            var newEmployee = new Employee
            {
                NameAr = "Employee to Delete",
                NameEng = "Employee to Delete",
                NID = "987654321",
                Phone = "0987654321",
                Email = "delete@example.com",
                PassportNumber = "B7654321",
                AdministrationId = createdAdmin.Id
            };

            var createResponse = await _client.PostAsJsonAsync("/api/employees", newEmployee);
            createResponse.EnsureSuccessStatusCode();

            var createdEmployee = await createResponse.Content.ReadFromJsonAsync<Employee>();
            Assert.NotNull(createdEmployee);

            // Act: Delete the created employee
            var deleteResponse = await _client.DeleteAsync($"/api/employees/{createdEmployee.Id}");

            // Assert: Verify the deletion was successful
            Assert.Equal(System.Net.HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // Verify the employee no longer exists
            var getResponse = await _client.GetAsync($"/api/employees/{createdEmployee.Id}");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, getResponse.StatusCode);
        }

        [Fact]
        public async Task GetEmployeesByAdministration_ReturnsSuccess()
        {
            var administrationId = Guid.NewGuid();

            var response = await _client.GetAsync($"/api/employees/by-administration/{administrationId}");
            response.EnsureSuccessStatusCode();

            var employees = await response.Content.ReadFromJsonAsync<List<Employee>>();
            Assert.NotNull(employees);
        }
    }
}