using LanguageExt;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Read.Ports;
using MyMusic.Application.Read.Queries.Errors;

namespace MyMusic.Application.Read.Queries {
    public class GetPlayListQuery {
        
        private readonly PlayListQueryPort playListQuery;
        
        public GetPlayListQuery(PlayListQueryPort playListQuery) {
            this.playListQuery = playListQuery;
        }

        public Either<QueryError, PlayList> Get(string playlistId) {
            return playListQuery.GetPlayList(playlistId);
        }
    }
}