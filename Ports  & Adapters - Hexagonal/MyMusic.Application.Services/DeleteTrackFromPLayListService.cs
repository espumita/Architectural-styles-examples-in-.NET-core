using System.Linq;
using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Domain;

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
            if (TrackIsNotAlreadyIn(playList, trackId)) return PlayListError.TrackIsNotInThePlayList;
            playList.Remove(trackId);
            playListPersistencePort.Persist(playList);
            tracksNotifier.NotifyTrackHasRemovedFromPlayList(trackId, playlistId);
            return OperationSuccess;
        }
        
        private bool TrackIsNotAlreadyIn(PlayList playList, string trackId) {
            return playList.TrackList.FirstOrDefault(x => x.Id.Equals(trackId)) == null;
        }
        
    }
}