using MyMusic.Application.Write.Ports.Notifications;

namespace MyMusic.Infrastructure.Http {

    public class PlayListSpotifyApiAdapter : PlayListNotifierPort {
        
        public void NotifyPlayListHasBeenCreated(string playListId, string playListName) {
            //This should Notify to Spotify Api
        }

        public void NotifyPlayListHasBeenRenamed(string playListId, string newPlayListName) {
            //This should Notify to Spotify Api
        }

        public void NotifyPlayListHasBeenArchived(string playListId) {
            //This should Notify to Spotify Api
        }

        public void NotifyPlayListImageUrlHasChanged(string aPlaylistId, string newImageUrl) {
            //This should Notify to Spotify Api
        }
    }
}