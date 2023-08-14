using MyMusic.Application.Read.Queries;
using MyMusic.Infrastructure.Queries;

namespace MyMusic.QueryCreators {
    public class TracksQueryCreator {
        
        public GetTracksQuery CreateGetTrackQuery() {
            var tracksDatabaseAdapter = new TracksPostgreSQLQueriesAdapter();
            return new GetTracksQuery(tracksDatabaseAdapter);
        }
    }
}