using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;

namespace MyMusic.Application.Services {
    
    public class DeleteTrackFromPLayListService {
        
        private readonly PlayListPersistencePort playListPersistencePort;
        private readonly TracksNotifierPort tracksNotifier;
        private const string OperationSuccess = "OperationSuccess";
        
        public DeleteTrackFromPLayListService(PlayListPersistencePort playListPersistencePort, TracksNotifierPort tracksNotifier) {
            this.playListPersistencePort = playListPersistencePort;
            this.tracksNotifier = tracksNotifier;
        }

        public Either<PlayListError, string> Execute(string trackId, string playlistId) {
            var playList = playListPersistencePort.GetPlayList(playlistId);
            playList.Remove(trackId);
            playListPersistencePort.Persist(playList);
            tracksNotifier.NotifyTrackHasRemovedFromPlayList(trackId, playlistId);
            return OperationSuccess;
        }
        
    }
}