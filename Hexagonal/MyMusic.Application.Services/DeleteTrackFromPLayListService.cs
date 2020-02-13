using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;

namespace MyMusic.Application.Services {
    
    public class DeleteTrackFromPLayListService {
        
        private readonly TracksPersistencePort tracksPersistence;
        private readonly TracksNotifierPort tracksNotifier;

        public DeleteTrackFromPLayListService(TracksPersistencePort tracksPersistence, TracksNotifierPort tracksNotifier) {
            this.tracksPersistence = tracksPersistence;
            this.tracksNotifier = tracksNotifier;
        }

        public void DeleteFromPlayList(string trackId, string playlistId) {
            tracksPersistence.DeleteTrackFromPlayList(trackId, playlistId);
            tracksNotifier.NotifyTrackHasRemovedFromPlayList(trackId, playlistId);
        }
        
    }
}