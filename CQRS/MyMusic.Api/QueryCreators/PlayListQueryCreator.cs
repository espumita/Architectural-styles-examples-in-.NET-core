using MyMusic.Application.Queries;
using MyMusic.Infrastructure.Adapters.Queries;

namespace MyMusic.QueryCreators {

    public class PlayListQueryCreator {
        
        public GetAllPlayListQuery CreateGetAllPlayListQuery() {
            var playListDatabaseAdapter = new PLayListPostgreSQLAdapter();
            return new GetAllPlayListQuery(playListDatabaseAdapter);
        }
        
        public GetPlayListService CreateGetPlayListQuery() {
            var playListDatabaseAdapter = new PLayListPostgreSQLAdapter();
            return new GetPlayListService(playListDatabaseAdapter);
        }
    }
}