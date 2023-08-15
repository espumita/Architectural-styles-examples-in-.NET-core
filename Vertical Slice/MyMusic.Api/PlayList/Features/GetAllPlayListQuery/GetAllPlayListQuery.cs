using System.Linq;
using LanguageExt;
using MyMusic.PlayList.Features.GetPlayListQuery;
using MyMusic.Shared.Queries.Errors;

namespace MyMusic.PlayList.Features.GetAllPlayListQuery {

    public class GetAllPlayListQuery {
        private readonly PlayListQueryPort playListQuery;
        
        public GetAllPlayListQuery(PlayListQueryPort playListQuery) {
            this.playListQuery = playListQuery;
        }

        public Either<QueryError, ListOfPlayLists> Execute() {
            var playLists = playListQuery.GetAllPlayList();
            var activePlayLists = playLists
                    .Where(playList => playList.Status == PlayListStatus.Active)
                    .ToList();
            return new ListOfPlayLists(activePlayLists);
        }
    }
}