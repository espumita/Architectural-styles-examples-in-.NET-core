namespace MyMusic.Application.Write.Commands {
    public class AddTrackToPLayList : Command {
        public string PlaylistId { get; }
        public string TrackId { get; }

        public AddTrackToPLayList(string trackId, string playlistId) {
            PlaylistId = playlistId;
            TrackId = trackId;
        }
    }
}