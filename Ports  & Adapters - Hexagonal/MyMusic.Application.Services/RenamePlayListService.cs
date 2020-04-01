using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain.Errors;

namespace MyMusic.Application.Services {
    public class RenamePlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly PlayListNotifierPort playListNotifier;
        
        public RenamePlayListService(PlayListPersistencePort playListPersistence, PlayListNotifierPort playListNotifier) {
            this.playListPersistence = playListPersistence;
            this.playListNotifier = playListNotifier;
        }

        public Either<DomainError, ServiceResponse> Execute(string playListId, string newPlayListName) {
            var playList = playListPersistence.GetPlayList(playListId);
            playList.Rename(newPlayListName);
            playListPersistence.Persist(playList);
            playListNotifier.NotifyPlayListHasBeenRenamed(playListId, newPlayListName);
            return ServiceResponse.Success;
        }
    }
}