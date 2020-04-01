using LanguageExt;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using MyMusic.Domain.Errors;

namespace MyMusic.Application.Services {
    public class GetPlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        
        public GetPlayListService(PlayListPersistencePort playListPersistence) {
            this.playListPersistence = playListPersistence;
        }

        public Either<DomainError, PlayList> Get(string playlistId) {
            return playListPersistence.GetPlayList(playlistId);
        }
    }
}