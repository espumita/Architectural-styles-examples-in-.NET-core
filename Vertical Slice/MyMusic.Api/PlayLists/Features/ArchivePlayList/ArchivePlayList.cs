using MyMusic.Shared.Commands;

namespace MyMusic.PlayLists.Features.ArchivePlayList {
    public class ArchivePlayList : Command {
        public string PlaylistId { get; }

        public ArchivePlayList(string playlistId) {
            PlaylistId = playlistId;
        }
    }
}