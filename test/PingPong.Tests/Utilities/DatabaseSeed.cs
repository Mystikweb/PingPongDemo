using System;
using System.Collections.Generic;
using PingPong.Models;

namespace PingPong.Tests.Utilities
{
    public static class DatabaseSeed
    {
        private static string[] firstNames = new[]
        {
            "John", "Jane", "Cedric", "Otto", "Carol", "Nell", "Sherwood", "Norma", "Johnie", "Hugo", "Bennie", "Florine"
        };

        private static string[] lastNames = new[]
        {
            "Doe", "Duran", "Nelson", "Keith", "Conner", "Hays", "Mcgrath", "Perez", "Payne", "Mcclain", "Chaney"
        };

        private static string[] organizations = new[]
        {
            "historical", "eastern", "suspicious", "successful", "serious", "reasonable", "administrative", "legal", "electrical", "intelligent"
        };

        private static string[] domainType = new[]
        {
            "com", "org", "net", "biz"
        };

        public static void InitializeMockDatabaseRecords(PingPongContext context, int count = 5)
        {
            context.Players.AddRange(GetSeedData(count));
            context.SaveChanges();
        }

        public static List<Player> GetSeedData(int count)
        {
            List<Player> results = new List<Player>();

            for (int i = 0; i < count; i++)
            {
                results.Add(GenerateRandomPlayer());
            }

            return results;
        }

        public static Player GenerateRandomPlayer()
        {
            string firstName = GetRandomFirstName();
            string lastName = GetRandomLastName();

            return new Player
            {
                FirstName = firstName,
                LastName = lastName,
                Email = GetRandomEmail(firstName, lastName),
                SkillLevel = GetRandomSkillLevel(),
                Age = GetRandomAge()
            };
        }

        public static string GetRandomFirstName()
        {
            return GetRandomString(firstNames);
        }

        public static string GetRandomLastName()
        {
            return GetRandomString(lastNames);
        }

        public static string GetRandomEmail(string firstName, string lastName)
        {
            Random random = new Random();
            return $"{firstName}.{lastName}@{organizations[random.Next(organizations.Length)]}.{domainType[random.Next(domainType.Length)]}";
        }

        public static SkillLevel GetRandomSkillLevel()
        {
            Random random = new Random();
            Array values = Enum.GetValues(typeof(SkillLevel));
            return (SkillLevel)values.GetValue(random.Next(values.Length));
        }

        public static int GetRandomAge()
        {
            Random random = new Random();
            return random.Next(25, 85);
        }

        private static string GetRandomString(string[] values)
        {
            Random random = new Random();
            return values[random.Next(values.Length)];
        }
    }
}