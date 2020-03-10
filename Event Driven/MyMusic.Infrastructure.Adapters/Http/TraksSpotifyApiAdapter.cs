using MyMusic.Application.Ports.Notifications;

namespace MyMusic.Infrastructure.Adapters.Http {

    public class TraksSpotifyApiAdapter : TracksNotifierPort {
        
        public void NotifyTrackHasBeenAddedToPlayList(string trackId, string playlistId) {
            //This should Notify to Spotify Api
        }

        public void NotifyTrackHasRemovedFromPlayList(string trackId, string playlistId) {
            //This should Notify to Spotify Api
        }
    }
}