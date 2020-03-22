using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;

namespace MyMusic.Application.Services {
    public class RenamePlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        
        public RenamePlayListService(PlayListPersistencePort playListPersistence) {
            this.playListPersistence = playListPersistence;
        }

        public Either<ServiceError, ServiceResponse> Execute(string playListId, string newPlayListName) {
            var playList = playListPersistence.GetPlayList(playListId);
            playList.Rename(newPlayListName);
            playListPersistence.Persist(playList);
            return ServiceResponse.Success;
        }
    }
}