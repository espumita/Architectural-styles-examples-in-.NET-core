using System.Linq;
using LanguageExt;
using MyMusic.PlayLists.Features.GetPlayListQuery;
using MyMusic.Shared;

namespace MyMusic.PlayLists.Features.GetAllPlayListQuery {

    public class GetAllPlayListQuery {
        private readonly PlayListQuery playListQuery;
        
        public GetAllPlayListQuery(PlayListQuery playListQuery) {
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