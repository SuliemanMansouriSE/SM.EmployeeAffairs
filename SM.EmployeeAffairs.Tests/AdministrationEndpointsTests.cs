using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using SM.EmployeeAffairs.Data.Entities;
using Xunit;

namespace SM.EmployeeAffairs.Tests
{
    public class AdministrationEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AdministrationEndpointsTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
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
            var response = await _client.GetAsync($"/api/administrations/{Guid.NewGuid()}");
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
            var newAdmin = new Administration
            {
                Name = "Admin to Delete",
                Description = "Description to Delete"
            };

            var createResponse = await _client.PostAsJsonAsync("/api/administrations", newAdmin);
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