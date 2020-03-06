using LanguageExt;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Domain;

namespace MyMusic.Application.Services {
    public class CreatePlayListService {
        
        private readonly UniqueIdentifiersPort uniqueIdentifiersPort;
        private readonly PlayListPersistencePort playListPersistence;
        private readonly PlayListNotifierPort playListNotifier;
        private const string OperationSuccess = "OperationSuccess";
        
        public CreatePlayListService(UniqueIdentifiersPort uniqueIdentifiersPort, PlayListPersistencePort playListPersistence, PlayListNotifierPort playListNotifier) {
            this.uniqueIdentifiersPort = uniqueIdentifiersPort;
            this.playListPersistence = playListPersistence;
            this.playListNotifier = playListNotifier;
        }

        public Either<PlayListError, string> Execute(string playListName) {
            var newPlayListId = uniqueIdentifiersPort.GetNewGuid();
            var playList = PlayList.Create(newPlayListId, playListName);
            playListPersistence.Persist(playList);
            playListNotifier.NotifyPlayListHasBeenCreated(playList.Id, playListName);
            return OperationSuccess;
        }
        
    }
}