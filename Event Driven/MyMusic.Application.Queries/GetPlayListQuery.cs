using LanguageExt;
using MyMusic.Application.Queries.Errors;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Read.Ports;

namespace MyMusic.Application.Queries {
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