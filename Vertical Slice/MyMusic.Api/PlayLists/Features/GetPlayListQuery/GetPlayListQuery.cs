using LanguageExt;
using MyMusic.Shared.Queries.Errors;

namespace MyMusic.PlayLists.Features.GetPlayListQuery {
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