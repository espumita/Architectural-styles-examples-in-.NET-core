using Bogus;

namespace MyMusic.Application.EventHandlers.Tests {

    public class ATrack {
        public static string Id = new Faker().Random.String2(64);
    }
}