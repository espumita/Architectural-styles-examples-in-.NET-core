using MyMusic.Model.PortsContracts.Notifications;

namespace MyMusic.Infrastructure.Http {

    public class PlayListSpotifyApiAdapter : PlayListNotifierPort {
        public void NotifyPlayListHasBeenCreated(string playListId, string playListName) {
            //This should Notify to Spotify Api
        }

        public void NotifyPlayListNameHasChanged(string playListId, string newPlayListName) {
            //This should Notify to Spotify Api
        }

        public void NotifyPlayListHasBeenDeleted(string playListId) {
            //This should Notify to Spotify Api
        }


    }
}