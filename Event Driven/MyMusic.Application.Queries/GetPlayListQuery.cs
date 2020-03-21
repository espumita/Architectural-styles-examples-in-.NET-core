using LanguageExt;
using MyMusic.Application.Queries.Errors;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Read.Ports;

namespace MyMusic.Application.Queries {
    public class GetPlayListQuery {
        
        private readonly PlayListQueryPort playListQueryPort;
        
        public GetPlayListQuery(PlayListQueryPort playListQueryPort) {
            this.playListQueryPort = playListQueryPort;
        }

        public Either<QueryError, PlayList> Get(string playlistId) {
            return playListQueryPort.GetPlayList(playlistId);
        }
    }
}