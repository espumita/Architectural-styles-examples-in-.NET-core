using MyMusic.Model;

namespace MyMusic.Responses {
    public class TrackResponse {
        public string TrackTrackId { get; }

        private TrackResponse(string trackTrackId) {
            TrackTrackId = trackTrackId;
        }

        public static TrackResponse From(Track track) {
            return new TrackResponse(track.trackId);
        }
    }
}