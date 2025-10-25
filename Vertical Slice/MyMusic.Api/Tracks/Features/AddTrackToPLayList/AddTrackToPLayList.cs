using MyMusic.Shared;

namespace MyMusic.Tracks.Features.AddTrackToPlayList {
    public class AddTrackToPlayList : Command {
        public string PlaylistId { get; }
        public string TrackId { get; }

        public AddTrackToPlayList(string trackId, string playlistId) {
            PlaylistId = playlistId;
            TrackId = trackId;
        }
    }
}