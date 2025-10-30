using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using Xunit;

namespace SimpleAPI.Test
{
    public class UsersControllerTests:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public UsersControllerTests(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();// ← this talks to your in-memory API
        }
        [Fact]
        public async Task GetAllUsers_ReturnsList()
        {
            var response = await _httpClient.GetAsync("/api/users");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Assert.Contains("Ravinder", json);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetUser_ById_ReturnsCorrectUser(int id)
        {
            // Act
            var response = await _httpClient.GetAsync($"/api/users/{id}");
            response.EnsureSuccessStatusCode();

            // Deserialize JSON to a C# object
            var user = await response.Content.ReadFromJsonAsync<UserDto>();

            // Assert
            Assert.NotNull(user);
            Assert.Equal(id, user!.Id);
            Assert.Equal($"User {id}", user.Name);
        }
    }
    public class UserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
