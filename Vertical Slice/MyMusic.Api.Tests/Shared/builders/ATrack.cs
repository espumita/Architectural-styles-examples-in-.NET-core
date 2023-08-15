using Bogus;

namespace MyMusic.Api.Tests.Shared.builders {
    public static class ATrack {
        
        public static string Id = new Faker().Random.String2(64);
    }
}