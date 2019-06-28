using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PingPong.Controllers;
using PingPong.Models;
using PingPong.Tests.Mocks;
using Xunit;

namespace PingPong.Tests.UnitTests
{
    public class PlayerControllerTests :
        IClassFixture<MockControllerFixture>
    {
        private readonly MockControllerFixture mocks;
        private readonly PlayerController controller;

        public PlayerControllerTests(MockControllerFixture mocks)
        {
            this.mocks = mocks;
            controller = new PlayerController(mocks.DbContext);
        }

        [Fact]
        public async Task get_returns_action_result_with_players_list()
        {
            var result = await controller.GetAll();

            var actionResult = Assert.IsType<ActionResult<List<Player>>>(result);
            Assert.IsType<List<Player>>(actionResult.Value);
        }

        [Fact]
        public async Task get_by_id_returns_action_result_with_player()
        {
            Player dbPlayer = await mocks.DbContext.Players.FirstOrDefaultAsync();

            var result = await controller.GetById(dbPlayer.PlayerId);

            var actionResult = Assert.IsType<ActionResult<Player>>(result);
            var playerResult = Assert.IsType<Player>(actionResult.Value);

            Assert.Equal(dbPlayer.PlayerId, playerResult.PlayerId);
        }

        [Fact]
        public async Task post_returns_created_at_route_action_result()
        {
            Player newPlayer = mocks.GetNewPlayer();

            var result = await controller.Create(newPlayer);

            var actionResult = Assert.IsType<ActionResult<Player>>(result);
            var createdActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var playerResult = Assert.IsType<Player>(createdActionResult.Value);
            
            Assert.StrictEqual(newPlayer, playerResult);
        }

        [Fact]
        public async Task put_update_returns_no_content()
        {
            Player playerToUpdate = await mocks.GetPlayerToUpdate();

            var result = await controller.Update(playerToUpdate.PlayerId, playerToUpdate);

            var noActionResult = Assert.IsType<NoContentResult>(result);

            Player playerResult = await mocks.GetPlayerFromContext(playerToUpdate.PlayerId);

            Assert.StrictEqual(playerToUpdate, playerResult);
        }

        [Fact]
        public async Task delete_returns_no_content()
        {
            Player playerToRemove = await mocks.DbContext.Players.LastOrDefaultAsync();

            var result = await controller.Delete(playerToRemove.PlayerId);

            var noActionResult = Assert.IsType<NoContentResult>(result);

            Player playerResult = await mocks.GetPlayerFromContext(playerToRemove.PlayerId);

            Assert.Null(playerResult);
        }
    }
}