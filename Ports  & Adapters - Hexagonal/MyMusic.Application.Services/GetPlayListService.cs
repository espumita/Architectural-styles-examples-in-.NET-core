using LanguageExt;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Domain;

namespace MyMusic.Application.Services {
    public class GetPlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        
        public GetPlayListService(PlayListPersistencePort playListPersistence) {
            this.playListPersistence = playListPersistence;
        }

        public Either<PlayListError, PlayList> Get(string playlistId) {
            return playListPersistence.GetPlayList(playlistId);
        }
    }
}