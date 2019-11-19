using MyMusic.Model;

namespace MyMusic.Responses {
    public class TrackResponse {
        public string Id { get; }
        public string Name { get; }
        public string Artist { get; }
        public int DurationInMs { get; }


        private TrackResponse(string id, string name, string artist, in int durationInMs) {
            Id = id;
            Name = name;
            Artist = artist;
            DurationInMs = durationInMs;
        }

        public static TrackResponse From(Track track) {
            return new TrackResponse(track.Id, track.Name, track.Artist, track.DurationInMs);
        }
    }
}