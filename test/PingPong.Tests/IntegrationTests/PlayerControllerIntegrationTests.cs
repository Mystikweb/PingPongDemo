using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PingPong.Models;
using PingPong.Tests.Mocks;
using PingPong.Tests.Utilities;
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
        public async Task get_returns_ok_status(string url)
        {
            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task get_returns_application_json(string url)
        {
            // Act
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task get_returns_list_of_players(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsAsync<List<Player>>();

            Assert.IsType<List<Player>>(data);
        }

        [Theory]
        [InlineData("/api/Player/1")]
        public async Task get_by_id_returns_ok(string url)
        {
            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Player/9999999")]
        public async Task get_by_id_invalid_returns_not_found(string url)
        {
            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Player/1")]
        public async Task get_by_id_returns_application_json(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/api/Player/1")]
        public async Task get_by_id_returns_player(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsAsync<Player>();

            Assert.IsType<Player>(data);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task post_invalid_returns_bad_request_status(string url)
        {
            var player = new Player();

            var response = await client.PostAsJsonAsync(url, player);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task post_returns_created_status(string url)
        {
            var player = DatabaseSeed.GenerateRandomPlayer();

            var response = await client.PostAsJsonAsync(url, player);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task post_returns_application_data_in_response(string url)
        {
            var player = DatabaseSeed.GenerateRandomPlayer();

            var response = await client.PostAsJsonAsync(url, player);

            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task post_returns_player(string url)
        {
            var player = DatabaseSeed.GenerateRandomPlayer();

            var response = await client.PostAsJsonAsync(url, player);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsAsync<Player>();

            Assert.IsType<Player>(data);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task post_returns_route_for_created_player(string url)
        {
            var player = DatabaseSeed.GenerateRandomPlayer();

            var response = await client.PostAsJsonAsync(url, player);
            response.EnsureSuccessStatusCode();

            Assert.NotNull(response.Headers.Location);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task post_created_route_matches_get_by_id_for_player(string url)
        {
            var player = DatabaseSeed.GenerateRandomPlayer();

            var response = await client.PostAsJsonAsync(url, player);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsAsync<Player>();

            Assert.Equal($"{url}/{data.PlayerId}", response.Headers.Location.AbsolutePath);
        }
    }
}