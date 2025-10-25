using MyMusic.Application.Queries;
using MyMusic.Infrastructure.Adapters.Queries;

namespace MyMusic.QueryCreators {

    public class PlayListQueryCreator {
        
        public GetAllPlayListQuery CreateGetAllPlayListQuery() {
            var playListDatabaseAdapter = new PlayListPostgreSQLQueryAdapter();
            return new GetAllPlayListQuery(playListDatabaseAdapter);
        }
        
        public GetPlayListQuery CreateGetPlayListQuery() {
            var playListDatabaseAdapter = new PlayListPostgreSQLQueryAdapter();
            return new GetPlayListQuery(playListDatabaseAdapter);
        }
    }
}