using Bogus;

namespace MyMusic.Api.Tests.EventHandlers {

    public class ATrack {
        public static string Id = new Faker().Random.String2(64);
    }
}