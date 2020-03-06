using LanguageExt;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Read.Ports;
using MyMusic.Application.SharedKernel.Model;

namespace MyMusic.Application.Queries {
    public class GetPlayListService {
        
        private readonly PlayListQueryPort playListPersistence;
        
        public GetPlayListService(PlayListQueryPort playListPersistence) {
            this.playListPersistence = playListPersistence;
        }

        public Either<Error, PlayList> Get(string playlistId) {
            return playListPersistence.GetPlayList(playlistId);
        }
    }
}