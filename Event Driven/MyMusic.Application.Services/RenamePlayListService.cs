using LanguageExt;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain.Events;

namespace MyMusic.Application.Services {
    public class RenamePlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventBusPort eventBus;

        public RenamePlayListService(PlayListPersistencePort playListPersistence, EventBusPort eventBus) {
            this.playListPersistence = playListPersistence;
            this.eventBus = eventBus;
        }

        public Either<ServiceError, ServiceResponse> Execute(string playListId, string newPlayListName) {
            var playList = playListPersistence.GetPlayList(playListId);
            playList.Rename(newPlayListName);
            playListPersistence.Persist(playList);
            eventBus.Raise(new PlayListHasBeenRenamed(playList.Id, playList.Name));
            return ServiceResponse.Success;
        }
    }
}