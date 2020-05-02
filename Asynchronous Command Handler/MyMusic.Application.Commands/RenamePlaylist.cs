namespace MyMusic.Application.Commands {
    public class RenamePlaylist: Command{
        public string playlistId { get; }
        public string newPlayListName { get; }

        public RenamePlaylist(string playlistId, string newPlayListName) {
            this.playlistId = playlistId;
            this.newPlayListName = newPlayListName;
        }
    }
}