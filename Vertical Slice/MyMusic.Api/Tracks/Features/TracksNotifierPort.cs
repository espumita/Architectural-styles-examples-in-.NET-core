namespace MyMusic.Tracks.Features {
    public interface TracksNotifierPort {
        
        void NotifyTrackHasBeenAddedToPlayList(string trackId, string playlistId);
        void NotifyTrackHasRemovedFromPlayList(string trackId, string playlistId);
    }
}