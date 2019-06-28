using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PingPong.Models;
using PingPong.Tests.Utilities;

namespace PingPong.Tests.Mocks
{
    public class MockControllerFixture : IDisposable
    {
        public PingPongContext DbContext { get; private set; }

        public MockControllerFixture()
        {
            DbContextOptions<PingPongContext> options = new DbContextOptionsBuilder<PingPongContext>()
                .UseInMemoryDatabase(databaseName: "PingPongTesting")
                .Options;

            DbContext = new PingPongContext(options);
            DatabaseSeed.InitializeMockDatabaseRecords(DbContext);
        }

        public Player GetNewPlayer()
        {
            return DatabaseSeed.GenerateRandomPlayer();
        }

        public async Task<Player> GetPlayerToUpdate()
        {
            Player result = await DbContext.Players.FirstOrDefaultAsync();

            result.SkillLevel = DatabaseSeed.GetRandomSkillLevel();
            result.Age = DatabaseSeed.GetRandomAge();

            return result;
        }

        public async Task<Player> GetPlayerFromContext(int playerId)
        {
            return await DbContext.Players.FirstOrDefaultAsync(p => p.PlayerId == playerId);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}