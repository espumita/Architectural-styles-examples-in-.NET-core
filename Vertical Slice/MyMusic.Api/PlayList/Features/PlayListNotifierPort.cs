namespace MyMusic.PlayList.Features {
    public interface PlayListNotifierPort {
        
        void NotifyPlayListHasBeenCreated(string playListId, string playListName);
        void NotifyPlayListHasBeenRenamed(string playListId, string newPlayListName);
        void NotifyPlayListHasBeenArchived(string playListId);
        void NotifyPlayListImageUrlHasChanged(string aPlaylistId, string newImageUrl);
    }
}