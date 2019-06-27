using System.Collections.Generic;
using PingPong.Models;

namespace PingPong.Tests.Utilities
{
    public static class DatabaseSeed
    {
        public static void InitializeMockDatabaseRecords(PingPongContext context)
        {
            context.Players.AddRange(GetSeedData());
            context.SaveChanges();
        }

        public static List<Player> GetSeedData()
        {
            return new List<Player>
            {
                new Player { FirstName = "John", LastName = "Doe", Email = "john.doe@somewhere.org", SkillLevel = SkillLevel.Beginner, Age = 99 },
                new Player { FirstName = "Jane", LastName = "Doe", Email = "jane.doe@someone.org", SkillLevel = SkillLevel.Intermediate, Age = 88 },
                new Player { FirstName = "Foo", LastName = "Bar", Email = "foo.bar@something.org", SkillLevel = SkillLevel.Advanced, Age = 77 }
            };
        }
    }
}