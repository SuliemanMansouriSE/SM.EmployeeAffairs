using Microsoft.Extensions.DependencyInjection;
using SM.EmployeeAffairs.Data;
using SM.EmployeeAffairs.Data.Entities;
using System.Net.Http.Json;

namespace SM.EmployeeAffairs.Tests
{
    public class AdministrationEndpointsTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AdministrationEndpointsTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();

        }

        [Fact]
        public void CanResolveDbContext()
        {
            var factory = new CustomWebApplicationFactory();
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
            Assert.NotNull(db);
        }

        [Fact]
        public async Task GetAllAdministrations_ReturnsSuccess()
        {
            var response = await _client.GetAsync("/api/administrations");
            response.EnsureSuccessStatusCode();

            var administrations = await response.Content.ReadFromJsonAsync<List<Administration>>();
            Assert.NotNull(administrations);
        }

        [Fact]
        public async Task GetAdministrationById_ReturnsNotFound_ForInvalidId()
        {
            //Arrange: Use a GUID that does not exist in the database
            var id = Guid.NewGuid();
            //Act: Attempt to get the administration by the invalid ID
            var response = await _client.GetAsync($"/api/administrations/{id}");
            //Assert: Verify that the response status code is NotFound (404)
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostAdministration_CreatesNewAdministration()
        {
            var newAdmin = new Administration
            {
                Name = "Test Admin",
                Description = "Test Description"
            };

            var response = await _client.PostAsJsonAsync("/api/administrations", newAdmin);
            response.EnsureSuccessStatusCode();

            var createdAdmin = await response.Content.ReadFromJsonAsync<Administration>();
            Assert.NotNull(createdAdmin);
            Assert.Equal(newAdmin.Name, createdAdmin.Name);
        }

        [Fact]
        public async Task DeleteAdministration_RemovesAdministration()
        {
            // Arrange: Create a new administration to delete
            var id = Guid.NewGuid();
            var newAdmin = new Administration
            { 
                Id = id,
                Name = "Admin to Delete",
                Description = "Description to Delete"
            };

            var createResponse = await _client.PostAsJsonAsync("/api/administrations", newAdmin);
            if (!createResponse.IsSuccessStatusCode)
            {
                var errorContent = await createResponse.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
            createResponse.EnsureSuccessStatusCode();

            var createdAdmin = await createResponse.Content.ReadFromJsonAsync<Administration>();
            Assert.NotNull(createdAdmin);

            // Act: Delete the created administration
            var deleteResponse = await _client.DeleteAsync($"/api/administrations/{createdAdmin.Id}");

            // Assert: Verify the deletion was successful
            Assert.Equal(System.Net.HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // Verify the administration no longer exists
            var getResponse = await _client.GetAsync($"/api/administrations/{createdAdmin.Id}");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, getResponse.StatusCode);
        }

        [Fact]
        public async Task PutAdministration_UpdatesAdministration()
        {
            // Arrange: Create a new administration to update
            var newAdmin = new Administration
            {
                Name = "Admin to Update",
                Description = "Description to Update"
            };

            var createResponse = await _client.PostAsJsonAsync("/api/administrations", newAdmin);
            createResponse.EnsureSuccessStatusCode();

            var createdAdmin = await createResponse.Content.ReadFromJsonAsync<Administration>();
            Assert.NotNull(createdAdmin);

            // Act: Update the created administration
            var updatedAdmin = new Administration
            {
                Name = "Updated Admin",
                Description = "Updated Description"
            };

            var updateResponse = await _client.PutAsJsonAsync($"/api/administrations/{createdAdmin.Id}", updatedAdmin);

            // Assert: Verify the update was successful
            updateResponse.EnsureSuccessStatusCode();

            var updatedAdminResponse = await updateResponse.Content.ReadFromJsonAsync<Administration>();
            Assert.NotNull(updatedAdminResponse);
            Assert.Equal(updatedAdmin.Name, updatedAdminResponse.Name);
            Assert.Equal(updatedAdmin.Description, updatedAdminResponse.Description);
        }
    }
}