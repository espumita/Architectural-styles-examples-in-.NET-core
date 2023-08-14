namespace MyMusic.Application.Write.Commands {
    public class ArchivePlayList : Command {
        public string PlaylistId { get; }

        public ArchivePlayList(string playlistId) {
            PlaylistId = playlistId;
        }
    }
}