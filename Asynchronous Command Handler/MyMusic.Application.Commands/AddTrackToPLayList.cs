namespace MyMusic.Application.Commands {
    public class AddTrackToPLayList : Command {
        public string PlaylistId { get; }
        public string TrackId { get; }

        public AddTrackToPLayList(string playlistId, string trackId) {
            PlaylistId = playlistId;
            TrackId = trackId;
        }
    }
}