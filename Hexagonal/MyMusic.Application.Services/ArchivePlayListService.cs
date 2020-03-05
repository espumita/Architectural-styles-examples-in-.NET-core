using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;

namespace MyMusic.Application.Services {
    
    public class ArchivePlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly PlayListNotifierPort playListNotifier;
        private const string OperationSuccess = "OperationSuccess";
        public ArchivePlayListService(PlayListPersistencePort playListPersistence, PlayListNotifierPort playListNotifier) {
            this.playListPersistence = playListPersistence;
            this.playListNotifier = playListNotifier;
        }

        public Either<PlayListError, string> Execute(string playListId) {
            var playList = playListPersistence.GetPlayList(playListId);
            playList.Archive();
            playListPersistence.Persist(playList);
            playListNotifier.NotifyPlayListHasBeenArchived(playListId);
            return OperationSuccess;
        }
    }
}