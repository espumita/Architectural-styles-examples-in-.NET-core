using LanguageExt;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain;
using MyMusic.Domain.Error;

namespace MyMusic.Application.Services {
    public class CreatePlayListService {
        
        private readonly UniqueIdentifiersPort uniqueIdentifiers;
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventBusPort eventBus;

        public CreatePlayListService(UniqueIdentifiersPort uniqueIdentifiers, PlayListPersistencePort playListPersistence, EventBusPort eventBus) {
            this.uniqueIdentifiers = uniqueIdentifiers;
            this.playListPersistence = playListPersistence;
            this.eventBus = eventBus;
        }

        public Either<DomainError, ServiceResponse> Execute(string playListName) {
            var newPlayListId = uniqueIdentifiers.GetNewUniqueIdentifier();
            var playList = PlayList.Create(newPlayListId, playListName);
            
            playListPersistence.Persist(playList);
            eventBus.Raise(playList.Events());
            return ServiceResponse.Success;
        }
        
    }
}