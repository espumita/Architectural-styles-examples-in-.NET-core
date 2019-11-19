using MyMusic.Model.PortsContracts.Http;

namespace MyMusic.Infrastructure.Http {

    public class MusicCloudApiHttpAdapter : MusicHttpPort {
        public void NotifyPlayListHasBeenCreated(string playListId, string playListName) {
            //This should Notify to another Api
        }

        public void NotifyPlayListNameHasChanged(string playListId, string newPlayListName) {
            //This should Notify to another Api
        }

        public void NotifyPlayListHasBeenDeleted(string playListId) {
            //This should Notify to another Api
        }

        public void NotifyTrackHasBeenAddedToPlayList(string trackId, string playlistId) {
            //This should Notify to another Api
        }

        public void NotifyTrackHasRemovedFromPlayList(string trackId, string playlistId) {
            //This should Notify to another Api
        }
    }
}