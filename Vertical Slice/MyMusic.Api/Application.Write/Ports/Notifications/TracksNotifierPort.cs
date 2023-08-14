namespace MyMusic.Application.Write.Ports.Notifications {
    public interface TracksNotifierPort {
        
        void NotifyTrackHasBeenAddedToPlayList(string trackId, string playlistId);
        void NotifyTrackHasRemovedFromPlayList(string trackId, string playlistId);
    }
}