using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using NUnit.Framework;

namespace Practice5API.Practice5API.Tests.Controllers
{
    public class AuthControllerTests
    {
        private HttpClient _client;
        private CustomWebApplicationFactory<Program> _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task Login_ReturnsJwtToken()
        {
            // Arrange
            var loginRequest = new
            {
                Username = "jriosayala",
                Password = "mySuperHardToRememberPassword"
            };

            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/auth/login", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString.Contains("token"), Is.True);
        }

        [Test]
        public async Task Get_Products_ReturnsUnauthorized_WithoutToken()
        {
            // Act
            var response = await _client.GetAsync("/api/products");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Get_Products_ReturnsOk_WithValidToken()
        {
            // Arrange
            var loginRequest = new
            {
                Username = "jriosayala",
                Password = "mySuperHardToRememberPassword"
            };

            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");
            var loginResponse = await _client.PostAsync("/api/auth/login", content);
            loginResponse.EnsureSuccessStatusCode();

            var loginResponseString = await loginResponse.Content.ReadAsStringAsync();
            var token = JsonDocument.Parse(loginResponseString).RootElement.GetProperty("token").GetString();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync("/api/products");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
