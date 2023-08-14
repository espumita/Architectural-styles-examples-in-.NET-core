using MyMusic.Application.Read.Queries;
using MyMusic.Infrastructure.Queries;

namespace MyMusic.QueryCreators {

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