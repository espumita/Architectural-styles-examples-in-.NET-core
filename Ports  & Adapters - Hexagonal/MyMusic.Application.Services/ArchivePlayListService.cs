using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;

namespace MyMusic.Application.Services {
    public class ArchivePlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly PlayListNotifierPort playListNotifier;
        
        public ArchivePlayListService(PlayListPersistencePort playListPersistence, PlayListNotifierPort playListNotifier) {
            this.playListPersistence = playListPersistence;
            this.playListNotifier = playListNotifier;
        }

        public Either<ServiceError, ServiceResponse> Execute(string playListId) {
            var playList = playListPersistence.GetPlayList(playListId);
            playList.Archive();
            playListPersistence.Persist(playList);
            playListNotifier.NotifyPlayListHasBeenArchived(playListId);
            return ServiceResponse.Success;
        }
    }
}