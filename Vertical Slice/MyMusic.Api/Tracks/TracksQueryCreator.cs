using MyMusic.Tracks.Features;
using MyMusic.Tracks.Features.GetTrack;

namespace MyMusic.Tracks {
    public class TracksQueryCreator {
        
        public GetTrackQuery CreateGetTrackQuery() {
            var tracksDatabaseAdapter = new TracksPostgreSQLQueries();
            return new GetTrackQuery(tracksDatabaseAdapter);
        }
    }
}