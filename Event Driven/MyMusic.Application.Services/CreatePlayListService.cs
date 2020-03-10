using LanguageExt;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Successes;
using MyMusic.Application.SharedKernel.Model;
using MyMusic.Domain;
using MyMusic.Domain.Events;

namespace MyMusic.Application.Services {
    public class CreatePlayListService {
        
        private readonly UniqueIdentifiersPort uniqueIdentifiersPort;
        private readonly PlayListPersistencePort playListPersistence;
        private readonly PlayListNotifierPort playListNotifier;
        private readonly EventBus eventBus;

        public CreatePlayListService(UniqueIdentifiersPort uniqueIdentifiersPort,
            PlayListPersistencePort playListPersistence, PlayListNotifierPort playListNotifier, EventBus eventBus) {
            this.uniqueIdentifiersPort = uniqueIdentifiersPort;
            this.playListPersistence = playListPersistence;
            this.playListNotifier = playListNotifier;
            this.eventBus = eventBus;
        }

        public Either<Error, ServiceResponse> Execute(string playListName) {
            var newPlayListId = uniqueIdentifiersPort.GetNewGuid();
            var playList = PlayList.Create(newPlayListId, playListName);
            playListPersistence.Persist(playList);
            playListNotifier.NotifyPlayListHasBeenCreated(playList.Id, playList.Name);
            eventBus.Raise(new PlayListHasBeenCreated(playList.Id, playList.Name));
            return ServiceResponse.Success;
        }
        
    }
}