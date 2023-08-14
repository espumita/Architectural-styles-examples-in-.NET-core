using Bogus;

namespace MyMusic.Api.Tests.CommandHandlers.builders {
    public static class APlaylist {
        
        public static string Id = new Faker().Random.String2(64);
        public static string Name = new Faker().Random.String2(64);
        public static string AnotherName = new Faker().Random.String2(64);
        public static string ImageUrl = new Faker().Random.String2(64);
        public static string AnotherImageUrl = new Faker().Random.String2(64);
    }
}