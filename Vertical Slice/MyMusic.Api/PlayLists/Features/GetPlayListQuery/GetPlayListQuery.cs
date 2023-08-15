using LanguageExt;
using MyMusic.Shared;

namespace MyMusic.PlayLists.Features.GetPlayListQuery {
    public class GetPlayListQuery {
        
        private readonly PlayListQuery playListQuery;
        
        public GetPlayListQuery(PlayListQuery playListQuery) {
            this.playListQuery = playListQuery;
        }

        public Either<QueryError, PlayList> Get(string playlistId) {
            return playListQuery.GetPlayList(playlistId);
        }
    }
}