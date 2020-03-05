namespace MyMusic.Application.Ports.Notifications {

    public interface PlayListNotifierPort {
        void NotifyPlayListHasBeenCreated(string playListId, string playListName);
        void NotifyPlayListHasBeenRenamed(string playListId, string newPlayListName);
        void NotifyPlayListHasBeenArchived(string playListId);
        void NotifyPlayListUrlHasChanged(string aPlaylistId, string newImageUrl);
    }
}