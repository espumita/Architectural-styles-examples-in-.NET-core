using Bogus;
using MyMusic.PlayList.Features.GetPlayListQuery;

namespace MyMusic.Api.Tests.Queries.builders {
    public class TrackBuilder {
        
        private string id;
        private string name;
        private string artist;
        private int? durationInMs;

        public TrackBuilder WithId(string id) {
            this.id = id;
            return this;
        }

        public TrackBuilder WithName(string name) {
            this.name = name;
            return this;
        }

        public TrackBuilder WithArtist(string artist) {
            this.artist = artist;
            return this;
        }

        public TrackBuilder WithDuration(int durationInMs) {
            this.durationInMs = durationInMs;
            return this;
        }

        public Track Build() {
            var faker = new Faker();
            return new Track(
                id: id ?? ATrack.Id,
                name: name ?? ATrack.Name,
                artist: artist ?? ATrack.Artist,
                durationInMs: durationInMs ?? ATrack.DurationInMs
            );
        }
    }
}