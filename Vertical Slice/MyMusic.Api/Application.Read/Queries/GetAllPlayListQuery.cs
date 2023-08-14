using System.Linq;
using LanguageExt;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Read.Ports;
using MyMusic.Application.Read.Queries.Errors;

namespace MyMusic.Application.Read.Queries {

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