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
    }
}