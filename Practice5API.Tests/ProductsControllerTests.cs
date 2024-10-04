using Practice5API.Data;
using System.Text;
using System.Text.Json;
using NUnit.Framework;
using System.Net.Http.Headers;

namespace Practice5API.Practice5API.Tests
{
    public class ProductsControllerTests
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
        public async Task Get_Products_ReturnsOk()
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


        [Test]
        public async Task Post_Product_CreatesProduct()
        {
            // Arrange
            var product = new Product
            {
                ProductID = 1,
                Name = "Test Product",
                Price = 50.0m
            };

            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/products", content);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
