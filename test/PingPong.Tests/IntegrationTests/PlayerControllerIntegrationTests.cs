using System.Net.Http;
using System.Threading.Tasks;
using PingPong.Tests.Mocks;
using Xunit;

namespace PingPong.Tests.IntegrationTests
{
    public class PlayerControllerIntegrationTests
        : IClassFixture<MockWebApplicationFactory<PingPong.Startup>>
    {
        private readonly MockWebApplicationFactory<PingPong.Startup> mockFactory;
        private readonly HttpClient client;

        public PlayerControllerIntegrationTests(MockWebApplicationFactory<PingPong.Startup> mockFactory)
        {
            this.mockFactory = mockFactory;
            client = mockFactory.CreateClient();
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task get_returns_application_json(string url)
        {
            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }
    }
}