namespace MyMusic.Tracks.Features {

    public class TraksSpotifyApiAdapter : TracksNotifierPort {
        
        public void NotifyTrackHasBeenAddedToPlayList(string trackId, string playlistId) {
            //This should Notify to Spotify Api
        }

        public void NotifyTrackHasRemovedFromPlayList(string trackId, string playlistId) {
            //This should Notify to Spotify Api
        }
    }
}