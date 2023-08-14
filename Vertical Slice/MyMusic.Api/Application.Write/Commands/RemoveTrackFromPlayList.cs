namespace MyMusic.Application.Write.Commands {
    public class RemoveTrackFromPlayList : Command {
        public string TrackId { get; }
        public string PlaylistId { get; }

        public RemoveTrackFromPlayList(string trackId, string playlistId) {
            TrackId = trackId;
            PlaylistId = playlistId;
        }
    }
}