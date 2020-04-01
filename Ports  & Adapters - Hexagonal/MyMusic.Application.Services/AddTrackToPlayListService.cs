using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using LanguageExt.UnsafeValueAccess;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain.Errors;

namespace MyMusic.Application.Services {
    public class AddTrackToPlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly TracksNotifierPort tracksNotifier;
        
        public AddTrackToPlayListService(PlayListPersistencePort playListPersistence, TracksNotifierPort tracksNotifier) {
            this.playListPersistence = playListPersistence;
            this.tracksNotifier = tracksNotifier;
        }

        public Either<DomainError, ServiceResponse> Execute(string trackId, string playlistId) {
            var playList = playListPersistence.GetPlayList(playlistId);
            var error = playList.Add(new Track(trackId, "UNNECESSARY", "UNNECESSARY", 0));
            if (error.IsSome) return error.ValueUnsafe();
            
            playListPersistence.Persist(playList);
            tracksNotifier.NotifyTrackHasBeenAddedToPlayList(trackId, playlistId);
            return ServiceResponse.Success;
        }

    }

}