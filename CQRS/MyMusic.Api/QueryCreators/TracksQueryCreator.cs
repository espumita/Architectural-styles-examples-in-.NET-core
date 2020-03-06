using MyMusic.Application.Queries;
using MyMusic.Infrastructure.Adapters.Queries;

namespace MyMusic.QueryCreators {
    public class TracksQueryCreator {
        
        public GetTracksQuery CreateGetTrackQuery() {
            var tracksDatabaseAdapter = new TracksPostgreSQLAdapter();
            return new GetTracksQuery(tracksDatabaseAdapter);
        }
    }
}