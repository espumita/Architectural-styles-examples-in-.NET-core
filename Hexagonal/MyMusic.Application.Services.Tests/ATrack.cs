using Bogus;

namespace MyMusic.Application.Services.Tests {
    public static class ATrack {
        public static string Id = new Faker().Random.String2(64);
    }
}