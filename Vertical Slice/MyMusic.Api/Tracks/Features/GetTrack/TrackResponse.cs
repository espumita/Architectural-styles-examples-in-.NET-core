using MyMusic.PlayLists.Features.GetPlayListQuery;
using MyMusic.Shared;

namespace MyMusic.Tracks.Features.GetTrack {
    public class TrackResponse : ResponseBuilder<TrackResponse, Track> {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Artist { get; private set; }
        public int DurationInMs { get; private set; }

        public TrackResponse() { }

        public TrackResponse BuildFrom(Track track) {
            Id = track.Id;
            Name = track.Name;
            Artist = track.Artist;
            DurationInMs = track.DurationInMs;
            return this;
        }
    }
}