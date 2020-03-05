using MyMusic.Application.Services;
using MyMusic.Infrastructure.Adapters.Http;
using MyMusic.Infrastructure.Adapters.Persistence;

namespace MyMusic.ServiceCreators {

    public class TracksServiceCreator {
        public GetTracksService CreateGetTrackService() {
            var tracksDatabaseAdapter = new TracksPostgreSQLAdapter();
            return new GetTracksService(tracksDatabaseAdapter);
        }
        
        public AddTrackToPlayListService CreateAddTrackToPlayListService() {
            var pLayListPostgreSqlAdapter = new PLayListPostgreSQLAdapter();
            var tracksNotifierAdapter = new TraksSpotifyApiAdapter();
            return new AddTrackToPlayListService(pLayListPostgreSqlAdapter, tracksNotifierAdapter);
        }
        
        public DeleteTrackFromPLayListService CreateDeleteTrackFromPLayListService() {
            var pLayListPostgreSqlAdapter = new PLayListPostgreSQLAdapter();
            var tracksNotifierAdapter = new TraksSpotifyApiAdapter();
            return new DeleteTrackFromPLayListService(pLayListPostgreSqlAdapter, tracksNotifierAdapter);
        }
    }
}