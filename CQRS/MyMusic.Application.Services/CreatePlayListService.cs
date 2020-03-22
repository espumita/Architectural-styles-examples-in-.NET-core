using LanguageExt;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain;

namespace MyMusic.Application.Services {
    public class CreatePlayListService {
        
        private readonly UniqueIdentifiersPort uniqueIdentifiers;
        private readonly PlayListPersistencePort playListPersistence;
        private readonly PlayListNotifierPort playListNotifier;
        
        public CreatePlayListService(UniqueIdentifiersPort uniqueIdentifiers, PlayListPersistencePort playListPersistence, PlayListNotifierPort playListNotifier) {
            this.uniqueIdentifiers = uniqueIdentifiers;
            this.playListPersistence = playListPersistence;
            this.playListNotifier = playListNotifier;
        }

        public Either<ServiceError, ServiceResponse> Execute(string playListName) {
            var newPlayListId = uniqueIdentifiers.GetNewUniqueIdentifier();
            var playList = PlayList.Create(newPlayListId, playListName);
            playListPersistence.Persist(playList);
            playListNotifier.NotifyPlayListHasBeenCreated(playList.Id, playListName);
            return ServiceResponse.Success;
        }
        
    }
}