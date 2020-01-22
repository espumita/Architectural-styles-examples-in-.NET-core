using MyMusic.Application.Services;
using MyMusic.Infrastructure.Http;
using MyMusic.Infrastructure.Persistence;

namespace MyMusic.ServiceCreators {

    public class TracksServiceCreator {
        public GetTracksService CreateGetTrackService() {
            var tracksDatabaseAdapter = new TracksPostgreSQLAdapter();
            return new GetTracksService(tracksDatabaseAdapter);
        }
        
        public AddTrackToPlayListService CreateAddTrackToPlayListService() {
            var tracksDatabaseAdapter = new TracksPostgreSQLAdapter();
            var tracksNotifierAdapter = new TraksSpotifyApiAdapter();
            return new AddTrackToPlayListService(tracksDatabaseAdapter, tracksNotifierAdapter);
        }
        
        public DeleteTrackFromPLayListService CreateDeleteTrackFromPLayListService() {
            var tracksDatabaseAdapter = new TracksPostgreSQLAdapter();
            var tracksNotifierAdapter = new TraksSpotifyApiAdapter();
            return new DeleteTrackFromPLayListService(tracksDatabaseAdapter, tracksNotifierAdapter);
        }
    }
}