namespace MyMusic.Application.Write.Commands {
    public class ChangePlayListImageUrl : Command {
        public string PlaylistId { get; }
        public string NewImageUrl { get; }

        public ChangePlayListImageUrl(string playlistId, string newImageUrl) {
            PlaylistId = playlistId;
            NewImageUrl = newImageUrl;
        }
    }
}