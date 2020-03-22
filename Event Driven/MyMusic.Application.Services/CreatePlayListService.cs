using LanguageExt;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain;
using MyMusic.Domain.Events;

namespace MyMusic.Application.Services {
    public class CreatePlayListService {
        
        private readonly UniqueIdentifiersPort uniqueIdentifiersPort;
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventBusPort eventBus;

        public CreatePlayListService(UniqueIdentifiersPort uniqueIdentifiersPort, PlayListPersistencePort playListPersistence, EventBusPort eventBus) {
            this.uniqueIdentifiersPort = uniqueIdentifiersPort;
            this.playListPersistence = playListPersistence;
            this.eventBus = eventBus;
        }

        public Either<ServiceError, ServiceResponse> Execute(string playListName) {
            var newPlayListId = uniqueIdentifiersPort.GetNewUniqueIdentifier();
            var playList = PlayList.Create(newPlayListId, playListName);
            playListPersistence.Persist(playList);
            eventBus.Raise(new PlayListHasBeenCreated(playList.Id, playList.Name));
            return ServiceResponse.Success;
        }
        
    }
}