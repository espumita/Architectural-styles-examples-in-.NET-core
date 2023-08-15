using Bogus;

namespace MyMusic.Api.Tests.PlayLists.GetAllPlayListQuery {
    public static class ATrack {
        
        public static string Id = new Faker().Random.String2(64);
        public static string Name = new Faker().Random.String2(64);
        public static string Artist = new Faker().Random.String2(64);
        public static int DurationInMs = new Faker().Random.Int(1);
    }
}