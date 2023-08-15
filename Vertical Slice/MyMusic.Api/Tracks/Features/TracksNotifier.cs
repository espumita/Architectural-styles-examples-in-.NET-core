namespace MyMusic.Tracks.Features {
    public interface TracksNotifier {
        
        void NotifyTrackHasBeenAddedToPlayList(string trackId, string playlistId);
        void NotifyTrackHasRemovedFromPlayList(string trackId, string playlistId);
    }
}