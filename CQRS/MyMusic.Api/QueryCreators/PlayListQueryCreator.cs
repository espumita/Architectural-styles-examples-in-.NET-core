using MyMusic.Application.Queries;
using MyMusic.Infrastructure.Adapters.Queries;

namespace MyMusic.QueryCreators {

    public class PlayListQueryCreator {
        
        public GetAllPlayListService CreateGetAllPlayListService() {
            var playListDatabaseAdapter = new PLayListPostgreSQLAdapter();
            return new GetAllPlayListService(playListDatabaseAdapter);
        }
        
        public GetPlayListService CreateGetPlayListService() {
            var playListDatabaseAdapter = new PLayListPostgreSQLAdapter();
            return new GetPlayListService(playListDatabaseAdapter);
        }
    }
}