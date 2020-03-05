using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;

namespace MyMusic.Application.Services {
    public class ChangePlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly PlayListNotifierPort playListNotifier;
        private const string OperationSuccess = "OperationSuccess";
        
        public ChangePlayListService(PlayListPersistencePort playListPersistence, PlayListNotifierPort playListNotifier) {
            this.playListPersistence = playListPersistence;
            this.playListNotifier = playListNotifier;
        }

        public Either<PlayListError, string> Execute(string playListId, string newPlayListName) {
            var playList = playListPersistence.GetPlayList(playListId);
            playList.Rename(newPlayListName);
            playListPersistence.Persist(playList);
            playListNotifier.NotifyPlayListNameHasChanged(playListId, newPlayListName);
            return OperationSuccess;
        }
    }
}