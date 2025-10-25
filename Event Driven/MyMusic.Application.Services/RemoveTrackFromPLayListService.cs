using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain.Error;

namespace MyMusic.Application.Services {
    public class RemoveTrackFromPlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventPublisherPort eventPublisher;
        
        public RemoveTrackFromPlayListService(PlayListPersistencePort playListPersistence, EventPublisherPort eventPublisher) {
            this.playListPersistence = playListPersistence;
            this.eventPublisher = eventPublisher;
        }

        public Either<DomainError, ServiceResponse> Execute(string trackId, string playlistId) {
            var playList = playListPersistence.GetPlayList(playlistId);
            var error = playList.Remove(trackId);
            if (error.IsSome) return error.ValueUnsafe();
            
            playListPersistence.Persist(playList);
            eventPublisher.Publish(playList.Events());
            return ServiceResponse.Success;
        }

    }
}