using LanguageExt;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;

namespace MyMusic.Application.Services {
    public class ArchivePlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        
        public ArchivePlayListService(PlayListPersistencePort playListPersistence) {
            this.playListPersistence = playListPersistence;
        }

        public Either<ServiceError, ServiceResponse> Execute(string playListId) {
            var playList = playListPersistence.GetPlayList(playListId);
            playList.Archive();
            playListPersistence.Persist(playList);
            return ServiceResponse.Success;
        }
    }
}