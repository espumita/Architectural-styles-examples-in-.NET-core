using MyMusic.Domain;

namespace MyMusic.Application.Services.Tests {
    public class TrackBuilder {
        
        private string id;

        public TrackBuilder WithId(string id) {
            this.id = id;
            return this;
        }

        public Track Build() {
            return new Track(
                id: id ?? ATrack.Id
            );
        }
    }
}