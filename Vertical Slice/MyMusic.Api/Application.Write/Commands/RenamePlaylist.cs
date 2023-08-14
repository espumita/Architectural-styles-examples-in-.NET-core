namespace MyMusic.Application.Write.Commands {
    public class RenamePlaylist: Command{
        public string PlaylistId { get; }
        public string NewPlayListName { get; }

        public RenamePlaylist(string playlistId, string newPlayListName) {
            PlaylistId = playlistId;
            NewPlayListName = newPlayListName;
        }
    }
}