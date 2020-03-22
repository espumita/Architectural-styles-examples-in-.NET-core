using LanguageExt;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain.Events;

namespace MyMusic.Application.Services {
    public class ArchivePlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventBusPort eventBus;

        public ArchivePlayListService(PlayListPersistencePort playListPersistence, EventBusPort eventBus) {
            this.playListPersistence = playListPersistence;
            this.eventBus = eventBus;
        }

        public Either<ServiceError, ServiceResponse> Execute(string playListId) {
            var playList = playListPersistence.GetPlayList(playListId);
            playList.Archive();
            playListPersistence.Persist(playList);
            eventBus.Raise(new PlayListHasBeenArchived(playListId));
            return ServiceResponse.Success;
        }
    }
}