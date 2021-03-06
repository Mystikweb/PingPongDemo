using System;
using Xunit;
using PingPong.Tests.Mocks;
using PingPong.Tests.Utilities;
using System.Linq;
using PingPong.Models;

namespace PingPong.Tests.UnitTests
{
    [TestCaseOrderer("PingPong.Tests.Utilities.PriorityOrderer", "PingPong.Tests")]
    public class DatabaseTests : 
        IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture dbFixture;

        public DatabaseTests(DatabaseFixture dbFixture)
        {
            this.dbFixture = dbFixture;
        }

        [Fact, Priority(1)]
        public void Use_Database_To_Create_Player()
        {
            dbFixture.DbContext.Players.Add(dbFixture.TestPlayer);
            int result = dbFixture.DbContext.SaveChanges();

            Assert.NotEqual(-1, result);
        }

        [Fact, Priority(2)]
        public void Use_Database_To_Read_Player()
        {
            Player result = dbFixture.DbContext.Players.FirstOrDefault(p => p.Email == dbFixture.TestPlayer.Email);

            Assert.NotNull(result);
        }

        [Fact, Priority(3)]
        public void Use_Database_To_Update_Player()
        {
            Player player = dbFixture.DbContext.Players.FirstOrDefault(p => p.Email == dbFixture.TestPlayer.Email);

            Assert.NotNull(player);
            Assert.Equal(99, player.Age);

            player.Age = 40;

            dbFixture.DbContext.Entry(player).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            int saveResult = dbFixture.DbContext.SaveChanges();

            Assert.NotEqual(-1, saveResult);

            Player result = dbFixture.DbContext.Players.FirstOrDefault(p => p.Email == dbFixture.TestPlayer.Email);

            Assert.NotNull(result);
            Assert.Equal(40, result.Age);
        }

        [Fact, Priority(4)]
        public void Use_Database_To_Remove_Player()
        {
            Player item = dbFixture.DbContext.Players.FirstOrDefault(p => p.Email == dbFixture.TestPlayer.Email);

            Assert.NotNull(item);

            dbFixture.DbContext.Players.Remove(item);
            dbFixture.DbContext.SaveChanges();

            Player result = dbFixture.DbContext.Players.FirstOrDefault(t => t.PlayerId == item.PlayerId);

            Assert.Null(result);
        }
    }
}
