using MyMusic.Domain;

namespace MyMusic.Responses {
    public class TrackResponse : ResponseMapper<TrackResponse, Track> {
        public string Id { get; }
        public string Name { get; }
        public string Artist { get; }
        public int DurationInMs { get; }

        public TrackResponse() { }

        private TrackResponse(string id, string name, string artist, in int durationInMs) {
            Id = id;
            Name = name;
            Artist = artist;
            DurationInMs = durationInMs;
        }

        public TrackResponse From(Track track) {
            return new TrackResponse(track.Id, track.Name, track.Artist, track.DurationInMs);
        }
    }
}