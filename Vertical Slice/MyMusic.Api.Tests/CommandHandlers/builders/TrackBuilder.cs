using MyMusic.Domain;

namespace MyMusic.Api.Tests.CommandHandlers.builders {
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