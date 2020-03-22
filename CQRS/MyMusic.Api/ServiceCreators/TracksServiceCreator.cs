using MyMusic.Application.Services;
using MyMusic.Infrastructure.Adapters.Http;
using MyMusic.Infrastructure.Adapters.Persistence;

namespace MyMusic.ServiceCreators {

    public class TracksServiceCreator {
        
        public AddTrackToPlayListService CreateAddTrackToPlayListService() {
            var pLayListPostgreSqlAdapter = new PLayListPostgreSQLPersistenceAdapter();
            var tracksNotifierAdapter = new TraksSpotifyApiAdapter();
            return new AddTrackToPlayListService(pLayListPostgreSqlAdapter, tracksNotifierAdapter);
        }
        
        public RemoveTrackFromPLayListService CreateRemoveTrackFromPLayListService() {
            var pLayListPostgreSqlAdapter = new PLayListPostgreSQLPersistenceAdapter();
            var tracksNotifierAdapter = new TraksSpotifyApiAdapter();
            return new RemoveTrackFromPLayListService(pLayListPostgreSqlAdapter, tracksNotifierAdapter);
        }
    }
}