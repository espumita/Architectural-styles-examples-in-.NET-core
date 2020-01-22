using MyMusic.Model.PortsContracts;
using MyMusic.Model.PortsContracts.Notifications;
using MyMusic.Model.PortsContracts.Persistence;

namespace MyMusic.Application.Services {
    
    public class AddTrackToPlayListService {
        
        private readonly TracksPersistencePort tracksPersistence;
        private readonly TracksNotifierPort tracksNotifier;

        public AddTrackToPlayListService(TracksPersistencePort tracksPersistence, TracksNotifierPort tracksNotifier) {
            this.tracksPersistence = tracksPersistence;
            this.tracksNotifier = tracksNotifier;
        }

        public void AddToPlayList(string trackId, string playlistId) {
            tracksPersistence.AddTrackToPlayList(trackId, playlistId);
            tracksNotifier.NotifyTrackHasBeenAddedToPlayList(trackId, playlistId);
        }
        
    }
}