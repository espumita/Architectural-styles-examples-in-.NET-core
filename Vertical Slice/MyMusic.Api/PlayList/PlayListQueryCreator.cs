using MyMusic.PlayList.Features;
using MyMusic.PlayList.Features.GetAllPlayListQuery;
using MyMusic.PlayList.Features.GetPlayListQuery;

namespace MyMusic.PlayList {

    public class PlayListQueryCreator {
        
        public GetAllPlayListQuery CreateGetAllPlayListQuery() {
            var playListDatabaseAdapter = new PLayListPostgreSQLQueryAdapter();
            return new GetAllPlayListQuery(playListDatabaseAdapter);
        }
        
        public GetPlayListQuery CreateGetPlayListQuery() {
            var playListDatabaseAdapter = new PLayListPostgreSQLQueryAdapter();
            return new GetPlayListQuery(playListDatabaseAdapter);
        }
    }
}