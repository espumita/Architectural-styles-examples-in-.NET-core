using MyMusic.Api.Tests.Tracks;
using MyMusic.Tracks.Domain;

namespace MyMusic.Api.Tests.Shared.builders {
    public class TrackBuilder {
        
        private string id;

        public TrackBuilder WithId(string id) {
            this.id = id;
            return this;
        }

        public Track Build() {
            return Track.With(id ?? ATrack.Id);
        }
    }
}