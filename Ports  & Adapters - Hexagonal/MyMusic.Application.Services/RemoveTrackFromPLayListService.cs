using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain.Errors;

namespace MyMusic.Application.Services {
    public class RemoveTrackFromPLayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly TracksNotifierPort tracksNotifier;
        
        public RemoveTrackFromPLayListService(PlayListPersistencePort playListPersistence, TracksNotifierPort tracksNotifier) {
            this.playListPersistence = playListPersistence;
            this.tracksNotifier = tracksNotifier;
        }

        public Either<DomainError, ServiceResponse> Execute(string trackId, string playlistId) {
            var playList = playListPersistence.GetPlayList(playlistId);
            var containsTrack = playList.ContainsTrack(trackId);
            if (!containsTrack) return DomainError.TrackIsNotInThePlayList;
            playList.Remove(trackId);
            playListPersistence.Persist(playList);
            tracksNotifier.NotifyTrackHasRemovedFromPlayList(trackId, playlistId);
            return ServiceResponse.Success;
        }

    }
}