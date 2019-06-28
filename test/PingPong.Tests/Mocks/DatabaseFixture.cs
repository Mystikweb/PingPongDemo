using System;
using Microsoft.EntityFrameworkCore;
using PingPong.Models;

namespace PingPong.Tests.Mocks
{
    public class DatabaseFixture : IDisposable
    {
        public PingPongContext DbContext { get; private set; }
        public Player TestPlayer { get; private set; }
        
        public DatabaseFixture()
        {
            DbContextOptions<PingPongContext> options = new DbContextOptionsBuilder<PingPongContext>()
                .UseInMemoryDatabase(databaseName: "PingPongTesting")
                .Options;

            DbContext = new PingPongContext(options);

            TestPlayer = new Player
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@somewhere.org",
                Age = 99,
                SkillLevel = SkillLevel.Beginner
            };
        }

        public void Dispose()
        {
            DbContext.Dispose();
            TestPlayer = null;
        }
    }
}