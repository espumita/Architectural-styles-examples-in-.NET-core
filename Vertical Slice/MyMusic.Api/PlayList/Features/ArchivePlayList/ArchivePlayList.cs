using MyMusic.Shared.Commands;

namespace MyMusic.PlayList.Features.ArchivePlayList {
    public class ArchivePlayList : Command {
        public string PlaylistId { get; }

        public ArchivePlayList(string playlistId) {
            PlaylistId = playlistId;
        }
    }
}