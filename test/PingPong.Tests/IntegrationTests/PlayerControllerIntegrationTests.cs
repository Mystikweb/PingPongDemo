using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PingPong.Models;
using PingPong.Tests.Mocks;
using PingPong.Tests.Utilities;
using Xunit;

namespace PingPong.Tests.IntegrationTests
{
    public class PlayerControllerIntegrationTests
        : IClassFixture<MockWebApplicationFactory<Startup>>
    {
        private readonly MockWebApplicationFactory<Startup> mockFactory;
        private readonly HttpClient client;

        public PlayerControllerIntegrationTests(MockWebApplicationFactory<Startup> mockFactory)
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

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<Player>>(content);

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

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Player>(content);

            Assert.IsType<Player>(data);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task post_invalid_returns_bad_request_status(string url)
        {
            var player = new Player();

            var content = new StringContent(JsonConvert.SerializeObject(player),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(url, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task post_invalid_returns_validation_problem_data(string url)
        {
            var player = new Player();

            var content = new StringContent(JsonConvert.SerializeObject(player),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(url, content);

            var responseConent = await response.Content.ReadAsStringAsync();
            var modelState = JsonConvert.DeserializeObject<ValidationProblemDetails>(responseConent);

            Assert.IsType<ValidationProblemDetails>(modelState);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task post_returns_created_status(string url)
        {
            var player = DatabaseSeed.GenerateRandomPlayer();

            var content = new StringContent(JsonConvert.SerializeObject(player),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(url, content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task post_returns_application_data_in_response(string url)
        {
            var player = DatabaseSeed.GenerateRandomPlayer();

            var content = new StringContent(JsonConvert.SerializeObject(player),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(url, content);

            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task post_returns_player(string url)
        {
            var player = DatabaseSeed.GenerateRandomPlayer();

            var content = new StringContent(JsonConvert.SerializeObject(player),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var playerResponse = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Player>(playerResponse);

            Assert.IsType<Player>(data);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task post_returns_route_for_created_player(string url)
        {
            var player = DatabaseSeed.GenerateRandomPlayer();

            var content = new StringContent(JsonConvert.SerializeObject(player),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            Assert.NotNull(response.Headers.Location);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task post_created_route_matches_get_by_id_for_player(string url)
        {
            var player = DatabaseSeed.GenerateRandomPlayer();

            var content = new StringContent(JsonConvert.SerializeObject(player),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var playerResponse = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Player>(playerResponse);

            Assert.Equal($"{url}/{data.PlayerId}", response.Headers.Location.AbsolutePath);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task put_invalid_player_returns_bad_request(string url)
        {
            var generated = DatabaseSeed.GenerateRandomPlayer();

            var postContent = new StringContent(JsonConvert.SerializeObject(generated),
                System.Text.Encoding.UTF8,
                "application/json");

            var postResponse = await client.PostAsync(url, postContent);
            postResponse.EnsureSuccessStatusCode();

            var playerContent = await postResponse.Content.ReadAsStringAsync();
            var player = JsonConvert.DeserializeObject<Player>(playerContent);

            player.LastName = null;

            var putContent = new StringContent(JsonConvert.SerializeObject(player),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync($"{url}/{player.PlayerId}", putContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task put_invalid_player_returns_validation_problem_data(string url)
        {
            var generated = DatabaseSeed.GenerateRandomPlayer();

            var postContent = new StringContent(JsonConvert.SerializeObject(generated),
                System.Text.Encoding.UTF8,
                "application/json");

            var postResponse = await client.PostAsync(url, postContent);
            postResponse.EnsureSuccessStatusCode();

            var playerContent = await postResponse.Content.ReadAsStringAsync();
            var player = JsonConvert.DeserializeObject<Player>(playerContent);

            player.LastName = null;

            var putContent = new StringContent(JsonConvert.SerializeObject(player),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync($"{url}/{player.PlayerId}", putContent);

            var responseConent = await response.Content.ReadAsStringAsync();
            var modelState = JsonConvert.DeserializeObject<ValidationProblemDetails>(responseConent);

            Assert.IsType<ValidationProblemDetails>(modelState);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task put_invalid_id_returns_not_found(string url)
        {
            var generated = DatabaseSeed.GenerateRandomPlayer();

            var postContent = new StringContent(JsonConvert.SerializeObject(generated),
                System.Text.Encoding.UTF8,
                "application/json");

            var postResponse = await client.PostAsync(url, postContent);
            postResponse.EnsureSuccessStatusCode();

            var playerContent = await postResponse.Content.ReadAsStringAsync();
            var player = JsonConvert.DeserializeObject<Player>(playerContent);

            player.Age = DatabaseSeed.GetRandomAge();

            var putContent = new StringContent(JsonConvert.SerializeObject(player),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync($"{url}/9999", putContent);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task put_valid_player_returns_no_content(string url)
        {
            var generated = DatabaseSeed.GenerateRandomPlayer();

            var postContent = new StringContent(JsonConvert.SerializeObject(generated),
                System.Text.Encoding.UTF8,
                "application/json");

            var postResponse = await client.PostAsync(url, postContent);
            postResponse.EnsureSuccessStatusCode();

            var playerContent = await postResponse.Content.ReadAsStringAsync();
            var player = JsonConvert.DeserializeObject<Player>(playerContent);

            player.Age = DatabaseSeed.GetRandomAge();

            var putContent = new StringContent(JsonConvert.SerializeObject(player),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync($"{url}/{player.PlayerId}", putContent);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Player")]
        public async Task put_valid_player_is_updated(string url)
        {
            var generated = DatabaseSeed.GenerateRandomPlayer();

            var postContent = new StringContent(JsonConvert.SerializeObject(generated),
                System.Text.Encoding.UTF8,
                "application/json");

            var postResponse = await client.PostAsync(url, postContent);
            postResponse.EnsureSuccessStatusCode();

            var playerContent = await postResponse.Content.ReadAsStringAsync();
            var player = JsonConvert.DeserializeObject<Player>(playerContent);

            player.LastName = DatabaseSeed.GetRandomLastName();
            player.Age = DatabaseSeed.GetRandomAge();

            var putContent = new StringContent(JsonConvert.SerializeObject(player),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync($"{url}/{player.PlayerId}", putContent);

            var getResponse = await client.GetAsync($"{url}/{player.PlayerId}");
            getResponse.EnsureSuccessStatusCode();

            var getContent = await getResponse.Content.ReadAsStringAsync();
            var resultPlayer = JsonConvert.DeserializeObject<Player>(getContent);

            Assert.Equal(player.LastName, resultPlayer.LastName);
            Assert.Equal(player.Age, resultPlayer.Age);
        }

        [Theory]
        [InlineData("/api/Player/99999")]
        public async Task delete_invalid_id_returns_not_found(string url)
        {
            var response = await client.DeleteAsync(url);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Player/1")]
        public async Task delete_valid_returns_no_content(string url)
        {
            var response = await client.DeleteAsync(url);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Player/2")]
        public async Task delete_valid_removes_player(string url)
        {
            var deleteResponse = await client.DeleteAsync(url);

            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            var getResponse = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}