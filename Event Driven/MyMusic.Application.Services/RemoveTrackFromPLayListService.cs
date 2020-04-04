using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain.Error;

namespace MyMusic.Application.Services {
    public class RemoveTrackFromPLayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventBusPort eventBus;
        
        public RemoveTrackFromPLayListService(PlayListPersistencePort playListPersistence, EventBusPort eventBus) {
            this.playListPersistence = playListPersistence;
            this.eventBus = eventBus;
        }

        public Either<DomainError, ServiceResponse> Execute(string trackId, string playlistId) {
            var playList = playListPersistence.GetPlayList(playlistId);
            var error = playList.Remove(trackId);
            if (error.IsSome) return error.ValueUnsafe();
            
            playListPersistence.Persist(playList);
            eventBus.Raise(playList.Events());
            return ServiceResponse.Success;
        }

    }
}