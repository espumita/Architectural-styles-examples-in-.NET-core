namespace MyMusic.Model.PortsContracts.Http {

    public interface MusicHttpPort {
        void NotifyPlayListHasBeenCreated(string playListId, string playListName);
        void NotifyPlayListNameHasChanged(string playListId, string newPlayListName);
        void NotifyPlayListHasBeenDeleted(string playListId);
        void NotifyTrackHasBeenAddedToPlayList(string trackId, string playlistId);
        void NotifyTrackHasRemovedFromPlayList(string trackId, string playlistId);
    }
}