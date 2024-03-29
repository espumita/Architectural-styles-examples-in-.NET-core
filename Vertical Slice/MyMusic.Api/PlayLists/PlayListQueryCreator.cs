using MyMusic.PlayLists.Features;
using MyMusic.PlayLists.Features.GetAllPlayListQuery;
using MyMusic.PlayLists.Features.GetPlayListQuery;

namespace MyMusic.PlayLists {

    public class PlayListQueryCreator {
        
        public GetAllPlayListQuery CreateGetAllPlayListQuery() {
            var playListDatabaseAdapter = new PLayListPostgreSQLQuery();
            return new GetAllPlayListQuery(playListDatabaseAdapter);
        }
        
        public GetPlayListQuery CreateGetPlayListQuery() {
            var playListDatabaseAdapter = new PLayListPostgreSQLQuery();
            return new GetPlayListQuery(playListDatabaseAdapter);
        }
    }
}