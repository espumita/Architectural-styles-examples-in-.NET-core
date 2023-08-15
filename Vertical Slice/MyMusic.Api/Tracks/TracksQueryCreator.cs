using MyMusic.Tracks.Features;
using MyMusic.Tracks.Features.GetTrack;

namespace MyMusic.Tracks {
    public class TracksQueryCreator {
        
        public GetTrackQuery CreateGetTrackQuery() {
            var tracksDatabaseAdapter = new TracksPostgreSQLQueriesAdapter();
            return new GetTrackQuery(tracksDatabaseAdapter);
        }
    }
}