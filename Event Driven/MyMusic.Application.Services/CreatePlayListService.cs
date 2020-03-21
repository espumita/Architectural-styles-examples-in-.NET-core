using LanguageExt;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain;
using MyMusic.Domain.Events;

namespace MyMusic.Application.Services {
    public class CreatePlayListService {
        
        private readonly UniqueIdentifiersPort uniqueIdentifiersPort;
        private readonly PlayListPersistencePort playListPersistence;
        private readonly PlayListNotifierPort playListNotifier;
        private readonly EventBusPort eventBusPort;

        public CreatePlayListService(UniqueIdentifiersPort uniqueIdentifiersPort, PlayListPersistencePort playListPersistence, PlayListNotifierPort playListNotifier, EventBusPort eventBusPort) {
            this.uniqueIdentifiersPort = uniqueIdentifiersPort;
            this.playListPersistence = playListPersistence;
            this.playListNotifier = playListNotifier;
            this.eventBusPort = eventBusPort;
        }

        public Either<ServiceError, ServiceResponse> Execute(string playListName) {
            var newPlayListId = uniqueIdentifiersPort.GetNewUniqueIdentifier();
            var playList = PlayList.Create(newPlayListId, playListName);
            playListPersistence.Persist(playList);
            playListNotifier.NotifyPlayListHasBeenCreated(playList.Id, playList.Name);
            eventBusPort.Raise(new PlayListHasBeenCreated(playList.Id, playList.Name));
            return ServiceResponse.Success;
        }
        
    }
}