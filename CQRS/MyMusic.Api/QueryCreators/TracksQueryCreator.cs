using MyMusic.Application.Queries;
using MyMusic.Infrastructure.Adapters.Queries;

namespace MyMusic.QueryCreators {
    public class TracksQueryCreator {
        
        public GetTracksService CreateGetTrackService() {
            var tracksDatabaseAdapter = new TracksPostgreSQLAdapter();
            return new GetTracksService(tracksDatabaseAdapter);
        }
    }
}