using MyMusic.Domain;

namespace MyMusic.Application.Services.Tests.builders {
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